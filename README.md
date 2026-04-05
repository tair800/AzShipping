# AzShipping

AzShipping is a **.NET microservices** backend for a logistics / ERP-style shipping product: requests, quotes, operations (orders), clients, carriers, accounting documents, tasks, employees, and central **Settings** reference data. Services expose **HTTP APIs** (JSON), authenticate callers with **JWT** issued by **Azshipping-Identity**, and enforce **ERP permissions** (employee-group claims merged at login).

---

## Repository layout

| Area | Purpose |
|------|--------|
| **`Azshipping-Identity/`** | **Identity.API** — user registration/login/refresh, JWT issuance, password flows. At login it calls **Settings.API** to resolve employee-group `PermissionsJson` into flat `erp_permission` claims. |
| **`services/`** | Bounded-context APIs (see table below). Each service typically has **Domain → Application → Infrastructure → API** layers and its own PostgreSQL database. |
| **`shared/AzShipping.ApiSecurity/`** | Shared library: JWT claim type names (`erp_permission`, `erp_unlimited`), **ErpModuleAccess** middleware (gate by module prefix), **ImportExport** / **ErpAccessScope** constants. Referenced by all secured APIs so behavior stays consistent. |
| **`AzShipping.Contracts/`** | Optional **local** folder for shared contracts (DTOs, API surface definitions, or a NuGet-style contracts package). Listed in **`.gitignore`** so teams can clone it next to the repo or generate it without committing generated output. When you add it, reference it from services that need cross-service type sharing. |
| **`frontend/Web/`** | **Frontend.Web** — Blazor/UI host on **http://localhost:5080** that proxies `/api/*` to the backends and forwards the **Bearer** token to Identity and downstream services. |

**`AzShipping.Microservices.sln`** includes the **services** listed there, **`shared/AzShipping.ApiSecurity`**, and **Frontend.Web**. **Azshipping-Identity** lives alongside the repo and is started with `dotnet run` (see Quick run); add it to the solution in Visual Studio if you want it in one build.

### Why **AzShipping.Contracts** (optional / often gitignored)

Microservices own their **internal** Domain and DTOs. You still need a **stable, shared picture** of what crosses the wire: request/response shapes, enums, or OpenAPI-generated clients so the UI and services do not drift.

**`AzShipping.Contracts/`** is the place for that shared layer (class library or generated package). It is **ignored in Git** in this repo so you can: pull contracts from another repository, generate them from Swagger in CI, or keep vendor-specific packages without polluting the main tree. **Without contracts**, each team can duplicate types and break at runtime; **with contracts**, you version one package and reference it from `Frontend`, `Identity` (if needed), and any service that calls another over HTTP.

### Why **Azshipping-Identity** (everyone logs in here)

There is **one** place responsible for **who the user is** and **what their token contains**: passwords, refresh tokens, lockout, and **JWT** issuance. Business APIs do **not** implement login; they only **validate** the same JWT.

Identity is critical because it **enriches** the token: after authentication it calls **Settings** (`resolve-permissions`) and attaches **`erp_permission`** (and optional **`erp_unlimited`**) so every downstream API can enforce ERP rules without talking to Settings on every request. **Deploy Identity with high availability** and correct URLs to Settings; if that call fails or the API key is wrong, users may get tokens **without** ERP claims and will be blocked by `ErpModuleAccess` or action-level filters.

### Why **AzShipping.ApiSecurity** (referenced by almost every API)

If each service hard-coded claim names (`"erp_permission"`) and copy-pasted middleware, one typo would let some APIs accept invalid tokens or use different rules.

**`shared/AzShipping.ApiSecurity`** is a **single** dependency that provides:

- **`ErpClaimTypes`** — canonical claim type strings used by Identity and all APIs.
- **`ErpModuleAccessMiddleware`** — optional “must have at least one claim under allowed module prefixes” check per service (`ErpModuleAccess:ModulePrefixes` in `appsettings`).
- Shared constants such as **ImportExport** and **ErpAccessScope** values so documentation and code stay aligned.

Every API project references this library so **security behavior stays consistent** across the fleet when you add a new module prefix or claim type.

---

## Services (why they exist)

| Service | Default URL (dev) | Role |
|--------|-------------------|------|
| **Identity.API** | `https://localhost:5001` (run profile / `--urls`) | Auth source of truth; issues JWTs with ERP claims after Settings resolve. |
| **Settings.API** | http://localhost:5064 | Reference data (classifiers, companies, departments, employee groups, templates, logs, email config, etc.). **Resolve-permissions** endpoint (API key) for Identity. |
| **Clients.API** | http://localhost:5065 | Client CRM data. |
| **Carrier.API** | http://localhost:5066 | Carrier data. |
| **General.API** | http://localhost:5068 | Tasks, employees, salary-calculation stub; **Task** / **Calculation** ERP filters. |
| **Request.API** | http://localhost:5069 | Freight requests, price proposals, commercial offers, negotiations. |
| **Quotes.API** | http://localhost:5070 | Quotes, Excel export, funnel; **Reports** + **ImportExport** gates. |
| **Operation.API** | http://localhost:5071 | Logistics **operations** (orders); **Orders** + **Warehouse** ERP claims on shared routes. |
| **Accounting.API** | http://localhost:5072 | Operation invoices, acts, payments, VAT, invoice lookups; **Documents** ERP filter. |
| **Frontend.Web** | http://localhost:5080 | Single entry for browsers; proxies to services. |

This split keeps **bounded contexts** independent (separate deploy, schema, and scaling), while the UI talks to one origin and relies on **HTTP + JWT** for every API call.

---

## How services connect: HTTP and JWT

1. **Browser → Frontend.Web** (cookie/session or token as your UI implements it).
2. **Frontend → Identity** over HTTPS (login/register/refresh) to obtain a **JWT**.
3. **Frontend → each microservice** over HTTP(S) with header:  
   `Authorization: Bearer <access_token>`.
4. Each API:
   - Validates the JWT (**Issuer**, **Audience**, signing key) via `AddJwtAuthentication`.
   - Runs **ErpModuleAccess** middleware: if enabled, the user must have at least one `erp_permission` claim whose prefix matches **`ErpModuleAccess:ModulePrefixes`** for that service (e.g. `Request.`, `Orders.`, `Settings.`).
   - Runs optional **per-controller ERP filters** that require specific claim strings (e.g. `Documents.issuedInvoices.view`).

**Server-to-server (no user JWT):**

- **Identity → Settings**: `POST .../api/employee-groups/resolve-permissions` with header **`X-AzShipping-Employee-Groups-Resolve-Key`** matching **`EmployeeGroupResolve:ApiKey`** in Settings.
- **Identity / others → email**: Settings **system send** uses its own API key header (see `EmailSettingsController` and config).

Align **`JWT:Issuer`**, **`JWT:Audience`**, and the **signing secret** across Identity and every API that accepts user tokens (often via user secrets or environment variables in production).

---

## ERP permissions (high level)

- Employee groups store **`PermissionsJson`** in Settings (nested booleans/strings).
- **Settings** merges multiple groups and **flattens** to strings like `Orders.view`, `Settings.system.editing`, `ImportExport.reports.exportToExcel`.
- Identity puts those into the token as multiple **`erp_permission`** claims (and optional **`erp_unlimited`**).
- **Access to …** dropdown-style fields use string ranks merged by permissiveness: `none` < `own` < `ownDepartment` < `all` (see `EmployeeGroupPermissionMerger` and `ErpAccessScopeValues` in shared code).

---

## Prerequisites

- **.NET 10** SDK  
- **PostgreSQL** — create databases named in each API’s `appsettings.json` / user secrets (e.g. `SettingsDb`, `OperationDb`, `RequestDb`, …).  
- Same **JWT** validation parameters and secret between **Identity** and all secured APIs.  
- Configure **Settings** `EmployeeGroupResolve:ApiKey` and Identity’s URL to Settings for login to receive ERP claims.

---

## Quick run

From repo root, use **separate terminals** (order can matter: Identity + Settings before UI-heavy flows).

taskkill /F /IM settings.API.exe
```bash
dotnet run --project services/Carrier/Carrier.API/Carrier.API.csproj
dotnet run --project services/clients/clients.api/clients.api.csproj
dotnet run --project services/Settings/Settings.API/Settings.API.csproj
dotnet run --project services/Accounting/Accounting.API/Accounting.API.csproj
dotnet run --project services/Quotes/Quotes.API/Quotes.API.csproj
dotnet run --project services/General/General.API/General.API.csproj
dotnet run --project services/Request/request.API/request.API.csproj
dotnet run --project services/Operation/Operation.API/Operation.API.csproj
dotnet run --project frontend/Web/Frontend.Web.csproj
dotnet run --project "Azshipping-Identity\Identity.API\Identity.API.csproj" --urls "https://localhost:5001"
```
 taskkill /IM "Request.API.exe" /F   

Then open **http://localhost:5080**.

**Swagger (dev):** each API serves `/swagger` on its port (e.g. Settings http://localhost:5064/swagger, Operation http://localhost:5071/swagger).

---

## Deployment: important information

### Topology and startup order

- **Settings.API** should be reachable **before** you rely on full login flows: Identity calls it to resolve employee groups.  
- **Identity** must be up for user JWTs; business APIs only validate tokens.  
- **PostgreSQL**: typically **one database per service** (isolated schema migrations, blast radius, backups). Set **`ConnectionStrings:DefaultConnection`** per deployment (env vars override `appsettings`).

### JWT (must match everywhere)

- **Same signing key** (or certificate configuration) and **Issuer** / **Audience** on **Identity** and on **every API** that uses `AddJwtAuthentication`. A mismatch yields `401` everywhere.  
- Prefer **environment variables** or a secret store for `JWT:SecretKey` (or equivalent); never ship production keys in Git.  
- Tokens carry **`erp_permission`**; if `ModulePrefixes` on a service does not include any prefix present on the user’s claims, **`erp_module_forbidden`** is returned after authentication.

### Server-to-server secrets

- **`EmployeeGroupResolve:ApiKey`** in **Settings** must match **`X-AzShipping-Employee-Groups-Resolve-Key`** used by **Identity** when calling `resolve-permissions`. Wrong key ⇒ login may succeed without ERP claims ⇒ users blocked on business APIs.  
- **Email system send** and similar integrations use their own API keys; configure in Settings and callers.

### Networking and the UI

- **CORS**: allow the real **frontend origin** on each API that the browser calls directly, or keep a **single BFF/proxy** (e.g. Frontend.Web) so browsers only talk to one host.  
- **Reverse proxy**: terminate TLS at the edge; forward **`Authorization`**, **`X-Forwarded-Proto`**, and **`X-Forwarded-For`** when applicable.  
- **Frontend.Web**: point proxy / gateway config at **internal** service URLs in production (not `localhost`).

### Configuration discipline

- **`ErpModuleAccess:ModulePrefixes`** per service must reflect what you store in employee-group JSON (`Request`, `Orders`, `Settings`, `Documents`, `Warehouse`, `ImportExport`, …).  
- **Clock skew**: NTP on all nodes; large skew breaks JWT `exp` / `nbf` validation.  
- **Health checks**: use `/health` (and Swagger only in non-production or locked down).

### Contracts in deployment / CI

- If you use **AzShipping.Contracts** as a NuGet package or submodule, **pin versions** in CI/CD so all services deploy against the same contract build.  
- Regenerate OpenAPI clients when APIs change to avoid runtime JSON mismatches.

### Quick deployment checklist (summary)

1. PostgreSQL per service + connection strings.  
2. JWT issuer/audience/secret aligned: Identity + all APIs.  
3. Settings resolve API key + Identity base URL to Settings.  
4. CORS / proxy / public URLs for UI and redirects.  
5. `ModulePrefixes` and ERP JSON in Settings aligned with product.  
6. Secrets via env or vault; no keys in repo.  
7. TLS, forwarding headers, health probes.  
8. Contracts package versioned if used.

---

## Further reading

- **`docs/FRONTEND-INTEGRATION.md`** — API integration notes for UI clients (if present in your clone).  
- **`.gitignore`** — excludes `frontend/`, `docs/`, `AzShipping.Contracts/`, and local appsettings variants per team policy.
