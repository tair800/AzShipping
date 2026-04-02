# AzShipping

Microservices: **Settings.API** (all reference data), **User.API** (auth/users), **Frontend.Web** (UI + proxy).

## Quick run

From repo root, in **three separate terminals**:

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
Then open **http://localhost:5080**

- **Settings.API** – port 5064, one DB (`SettingsDb`), migrations + seed on startup. Swagger: http://localhost:5064/swagger  
- **Operation.API** – port 5071, DB `OperationDb`. In **Development**, `JWT:SecretKey` comes from `appsettings.Development.json` (use the **same** signing key in Identity for valid tokens). Other environments: set `JWT:SecretKey` (user secrets or env). Swagger: http://localhost:5071/swagger  
- **User.API** – auth and users  
- **Frontend.Web** – serves UI and proxies `/api/*` to the backends  

## Prerequisites

- .NET 10 SDK  
- PostgreSQL (create DBs: `SettingsDb`, `UserDb` — or names in each API’s `appsettings.json`)  
- Same JWT secret in Settings.API and User.API if using auth  

## More

- **SETUP.md** – PostgreSQL, connection strings, optional Identity  
- **docs/FRONTEND-INTEGRATION.md** – API docs for React/frontend developers  
- **AzShipping.Microservices.sln** – Settings.API, User.API, Frontend.Web  
- **services/Settings/Settings.sln** – Settings service only (Domain, Application, Infrastructure, API)  
