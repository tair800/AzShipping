# Settings Service

Separate microservice for application/module settings (e.g. AzShipping). Uses Clean Architecture and DDD; validates JWT from Identity.

## Projects

- **Settings.API** – Controllers, JWT auth, Swagger
- **Settings.Application** – MediatR CQRS, DTOs, validators
- **Settings.Domain** – Aggregates (e.g. `AzShippingSettings`), repository interfaces
- **Settings.Infrastructure** – EF Core, PostgreSQL, repository implementations

## Run

1. Create PostgreSQL database: `SettingsDb` (same server as AzShipping, or separate).
2. Set secrets via user-secrets (recommended for development):
   ```bash
   cd services/Settings/Settings.API
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=SettingsDb;Username=postgres;Password=YOUR_PASSWORD"
   dotnet user-secrets set "JWT:SecretKey" "YOUR_JWT_SECRET_SAME_AS_IDENTITY"
   ```
3. From repo root:
   ```bash
   dotnet run --project services/Settings/Settings.API/Settings.API.csproj
   ```
   Or from this folder (`services/Settings/`): `dotnet run --project Settings.API/Settings.API.csproj`
   Default URL: http://localhost:5064

## Employee groups → JWT (Identity)

`EmployeeGroup.PermissionsJson` is merged for all groups listed on the user (`Identity` user `employeeGroupIds`) at **login** and **refresh**.

- **Settings:** `POST /api/employee-groups/resolve-permissions` — `[AllowAnonymous]` but requires header `X-AzShipping-Employee-Groups-Resolve-Key` matching config `EmployeeGroupResolve:ApiKey`. Body: `{ "ids": ["guid", ...] }`. Response: `{ "claims": ["Request.viewRequest", "Orders.generalSettings.view", "Orders.accessToOrders=all", ...] }`.
- **Identity:** Set `Settings:EmployeeGroupResolveApiKey` to the **same** API key; set `Settings:BaseUrl` to this service. If the key is missing or Settings is down, users still authenticate but get **no** `erp_permission` claims (only Identity `permission` claims from roles).
- **JWT claims:** `erp_permission` (one claim per flattened flag / access path), and `erp_unlimited` = `1` when the Identity user has **Unlimited access** (bypass ERP matrix in your domain code).
- **Domain APIs:** Shared library `shared/AzShipping.ApiSecurity` registers middleware (`UseErpModuleAccess` after `UseAuthentication`). Each service’s `appsettings` → `ErpModuleAccess:ModulePrefixes` must match JSON top-level keys (`Request`, `Clients`, `Carriers`, `Orders`, `Reports`, `Task`, `Settings`, …). If the token has **no** `erp_permission` claims, the middleware does nothing (legacy users). If it has claims but **none** match that service’s prefix, the API returns **403** (`erp_module_forbidden`). `erp_unlimited` bypasses the check. **Settings.API** uses this middleware with prefixes such as `Orders`, `Settings` (see `appsettings.json`).

## API (examples)

- `GET|POST|PUT|DELETE /api/templates` – Templates (manual `name`, `isActive`, optional `translations` map with keys `az`, `en`, `ru`). Seeded: Paid, Free.
- `GET /api/settings/azshipping` – Get AzShipping settings (requires `Authorization: Bearer <token>` from Identity).
- `PUT /api/settings/azshipping` – Create or update AzShipping settings (body: `{ "apiUrl", "apiKey", "isEnabled" }`).

See **INTEGRATION.md** in the repo root for how other apps or a BFF call this service.
