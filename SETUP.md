# AzShipping Setup Guide

Complete setup guide for the AzShipping application.

## Prerequisites

1. **.NET 8.0 SDK** - Download from https://dotnet.microsoft.com/download
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

### 3. Configure Connection String

Update `Presentation/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=AzShippingDb;Username=postgres;Password=YOUR_PASSWORD"
  }
}
```

**Default settings:**
- Host: `localhost`
- Port: `5432`
- Database: `AzShippingDb`
- Username: `postgres`
- Password: (set during PostgreSQL installation)

**Alternative: Use Environment Variable**
```powershell
# Windows PowerShell
$env:DATABASE_URL="Host=localhost;Port=5432;Database=AzShippingDb;Username=postgres;Password=YOUR_PASSWORD"

# Linux/Mac
export DATABASE_URL="Host=localhost;Port=5432;Database=AzShippingDb;Username=postgres;Password=YOUR_PASSWORD"
```

The application will use `DATABASE_URL` if `ConnectionStrings:DefaultConnection` is not set.

## Running the Application

### 1. Start PostgreSQL Service

**Windows:**
- Check Services (services.msc) → PostgreSQL service should be running
- Or use: `pg_ctl start`

**Linux/Mac:**
```bash
sudo systemctl start postgresql  # Linux
brew services start postgresql    # Mac
```

### 2. Run the Application

```bash
cd Presentation
dotnet run
```

Or from the root directory:
```bash
dotnet run --project Presentation/AzShipping.Presentation.csproj
```

### 3. What Happens on First Run

- Database tables are automatically created (via `ExecuteSqlRaw` in `Program.cs`)
- Initial seed data is inserted (if tables are empty)
- Application starts on `http://localhost:5062` (or configured port)

## Verifying Setup

### Method 1: Check Application UI

1. Open browser: http://localhost:5062
2. Navigate through pages:
   - Client Segments - should show 3 segments
   - Request Sources - should show 7 sources
   - Packagings - should show 5 packagings
   - Users - should show 8 users
   - Sales Funnel Status - should show 6 statuses
   - Transport Types - should show 15 types

### Method 2: Use pgAdmin

1. Open pgAdmin
2. Connect to server (localhost)
3. Expand: Servers → PostgreSQL → Databases → AzShippingDb → Schemas → public → Tables
4. Right-click any table → View/Edit Data → All Rows
5. Verify seeded data exists

### Method 3: Use SQL Queries

**In pgAdmin Query Tool:**
```sql
-- Count records in each table
SELECT 'ClientSegments' as TableName, COUNT(*) as Count FROM "ClientSegments"
UNION ALL
SELECT 'RequestSources', COUNT(*) FROM "RequestSources"
UNION ALL
SELECT 'Packagings', COUNT(*) FROM "Packagings"
UNION ALL
SELECT 'Users', COUNT(*) FROM "Users"
UNION ALL
SELECT 'SalesFunnelStatuses', COUNT(*) FROM "SalesFunnelStatuses"
UNION ALL
SELECT 'TransportTypes', COUNT(*) FROM "TransportTypes";

-- View sample data
SELECT * FROM "ClientSegments";
SELECT "FirstName", "LastName", "Email" FROM "Users";
```

**Expected Results:**
- ✅ **3** Client Segments
- ✅ **7** Request Sources
- ✅ **5** Packagings (+ translations)
- ✅ **8** Users
- ✅ **6** Sales Funnel Statuses (+ translations)
- ✅ **15** Transport Types (+ translations)

## Database Schema

The following tables are automatically created:

- **ClientSegments** - Client segment data
- **RequestSources** - Request source data
- **Packagings** - Packaging types
- **PackagingTranslations** - Packaging translations
- **Users** - User data
- **SalesFunnelStatuses** - Sales funnel status data
- **SalesFunnelStatusTranslations** - Sales funnel status translations
- **TransportTypes** - Transport type data
- **TransportTypeTranslations** - Transport type translations
- **LoadingMethods** - Loading method data
- **LoadingMethodTranslations** - Loading method translations
- **DeferredPaymentConditions** - Deferred payment conditions
- **RequestPurposes** - Request purposes
- **DrivingLicenceCategories** - Driving licence categories
- **WorkerPosts** - Worker posts
- **WorkerPostTranslations** - Worker post translations
- **CarrierTypes** - Carrier types
- **Banks** - Bank information

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

2. Check connection string in `appsettings.json`
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

**Symptoms:** Application runs but tables don't exist

**Solutions:**
1. Check application logs for errors
2. Verify database connection is working
3. Check `Program.cs` - `ExecuteSqlRaw` commands should run
4. Manually verify database exists and is accessible

### Seed Data Missing

**Symptoms:** Tables exist but are empty

**Solutions:**
1. Seed only runs if tables are empty (`.Any()` check)
2. If you need to reseed, drop and recreate database:
   ```sql
   DROP DATABASE "AzShippingDb";
   CREATE DATABASE "AzShippingDb";
   ```
3. Restart application

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

## API Documentation

- **Swagger UI:** http://localhost:5062/swagger (when running in Development mode)
- See `API_DOCUMENTATION.md` for detailed API documentation
- See `API_QUICK_REFERENCE.md` for quick API reference

## Development Notes

- Tables are created automatically via `ExecuteSqlRaw` in `Program.cs`
- Seed data is inserted by `DatabaseSeeder.Seed()` method
- Application uses Entity Framework Core with PostgreSQL
- All tables use UUID for primary keys
- Translation tables support multi-language content

## Next Steps

1. ✅ Database is set up and running
2. ✅ Application is running
3. ✅ Seed data is loaded
4. 🚀 Start using the application!

For API usage, see `API_DOCUMENTATION.md` and `API_QUICK_REFERENCE.md`.

