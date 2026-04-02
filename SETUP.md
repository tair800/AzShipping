# AzShipping Setup Guide

> **Note:** The legacy monolith (root `Application`, `Domain`, `Infrastructure`, `Presentation`) was removed. Run **microservices** under `services/`; see **README.md** (`AzShipping.Microservices.sln`, e.g. Settings on **5064**, frontend **5080**).

Complete setup guide for PostgreSQL and local development. Reference data is served by **Settings.API** (`SettingsDb`).

## Prerequisites

1. **.NET SDK** (see **README.md** for version, e.g. .NET 10) - https://dotnet.microsoft.com/download
2. **PostgreSQL** - Download from https://www.postgresql.org/download/

## PostgreSQL Installation

### Windows
- Download from: https://www.postgresql.org/download/windows/
- Or use: `winget install PostgreSQL.PostgreSQL`
- Default installation includes PostgreSQL server, pgAdmin, and command-line tools

### macOS
```bash
brew install postgresql
brew services start postgresql
```

### Linux (Ubuntu/Debian)
```bash
sudo apt update
sudo apt install postgresql postgresql-contrib
sudo systemctl start postgresql
```

## Database Setup

### 1. Create Database

After installing PostgreSQL, create the database:

**Using psql:**
```sql
-- Connect to PostgreSQL
psql -U postgres

-- Create database
CREATE DATABASE "AzShippingDb";

-- Exit
\q
```

**Using command line:**
```bash
createdb -U postgres AzShippingDb
```

**Using pgAdmin:**
1. Open pgAdmin
2. Right-click "Databases" → Create → Database
3. Name: `AzShippingDb`
4. Click "Save"

### 2. Find Your PostgreSQL Username

The most common username is **`postgres`** (default superuser).

**If `postgres` doesn't work, try:**
- Your Windows username
- Check pgAdmin connection settings
- Check Windows Services → PostgreSQL service → Log On tab

**Test connection:**
```powershell
# Windows PowerShell
psql -U postgres -d AzShippingDb -h localhost
# Enter password when prompted
```

### 3. Create service databases

Create databases your services expect (see **README.md**). At minimum for reference data:

```sql
CREATE DATABASE "SettingsDb";
```

You can still use **`AzShippingDb`** for local experiments; **Settings.API** defaults to **`SettingsDb`** in its `appsettings.json`.

### 4. Configure connection strings (microservices)

Each API has `appsettings.json` under `services/<Area>/<Name>.API/`. For **Settings.API**, set PostgreSQL to match `SettingsDb`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=SettingsDb;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

**Alternative: environment variable** (if the service supports it):

```powershell
$env:DATABASE_URL="Host=localhost;Port=5432;Database=SettingsDb;Username=postgres;Password=YOUR_PASSWORD"
```

## Running the application

### 1. Start PostgreSQL

Same as above (Windows Services, `pg_ctl`, `systemctl`, or `brew services`).

### 2. Run services

From the repo root, see **README.md** for the full list. For reference data only:

```bash
dotnet run --project services/Settings/Settings.API/Settings.API.csproj
```

- Swagger (Development): http://localhost:5064/swagger  

### 3. First run (Settings.API)

- EF migrations apply to the configured database  
- Seed runs according to that service’s startup logic  

## Verifying setup

1. Open http://localhost:5064/swagger and call list endpoints (e.g. client segments, packagings).  
2. In pgAdmin, inspect **SettingsDb** → **public** tables after the API has started.  

For frontend and proxy behavior, see **docs/FRONTEND-INTEGRATION.md**.

## Troubleshooting

### Connection Error

**Symptoms:** Application fails to start with database connection error

**Solutions:**
1. Verify PostgreSQL is running:
   ```powershell
   # Windows
   Get-Service postgresql*
   
   # Or check Services (services.msc)
   ```

2. Check connection string in the failing API’s `appsettings.json` (or user secrets / env)
3. Verify database exists:
   ```sql
   psql -U postgres -l
   ```
4. Test connection manually:
   ```powershell
   psql -U postgres -d AzShippingDb -h localhost
   ```

### Permission Error

**Symptoms:** Access denied or permission errors

**Solution:**
```sql
-- Grant privileges
GRANT ALL PRIVILEGES ON DATABASE "AzShippingDb" TO postgres;
```

### Port Already in Use

**Symptoms:** Port 5432 is already in use

**Solutions:**
1. Stop conflicting service
2. Change port in PostgreSQL config and update connection string
3. Use different port in connection string: `Port=5433`

### Tables Not Created

**Symptoms:** API runs but tables don't exist

**Solutions:**
1. Check API logs for migration errors
2. Verify database connection and database name match `appsettings.json`
3. Ensure the service runs migrations on startup (see that API’s `Program.cs`)
4. Manually verify the database exists and is reachable with `psql`

### Seed Data Missing

**Symptoms:** Tables exist but are empty

**Solutions:**
1. Seed only runs if tables are empty (`.Any()` check)
2. If you need to reseed, drop and recreate database:
   ```sql
   DROP DATABASE "AzShippingDb";
   CREATE DATABASE "AzShippingDb";
   ```
3. Restart the API

### psql Command Not Found

**Symptoms:** `psql` is not recognized

**Solutions:**
1. Use full path:
   ```powershell
   "C:\Program Files\PostgreSQL\16\bin\psql.exe" -U postgres
   ```
2. Add PostgreSQL bin to PATH:
   - System Properties → Environment Variables → Path
   - Add: `C:\Program Files\PostgreSQL\16\bin`
   - Restart terminal
3. Use pgAdmin GUI instead

## API documentation

- **Settings.API Swagger:** http://localhost:5064/swagger (Development)  
- **`API_DOCUMENTATION.md` / `API_QUICK_REFERENCE.md`** describe the **removed** monolith on port 5062; use Swagger and **docs/FRONTEND-INTEGRATION.md** for current contracts.

## Development notes

- Each service owns its DbContext, migrations, and seeding  
- EF Core + PostgreSQL; schemas differ per service database  

## Next steps

1. PostgreSQL running and databases created  
2. Run the services you need (see **README.md**)  
3. Use Swagger per service for exploration  

