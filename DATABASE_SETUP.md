# PostgreSQL Database Setup

## Current Configuration

The application is configured to use **PostgreSQL** database.

### Connection String

The connection string is configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=AzShippingDb;Username=postgres;Password=postgres"
  }
}
```

### Default Settings
- **Host**: localhost
- **Port**: 5432
- **Database**: AzShippingDb
- **Username**: postgres
- **Password**: postgres

## Setup Instructions

### 1. Install PostgreSQL

If you don't have PostgreSQL installed:

**Windows:**
- Download from: https://www.postgresql.org/download/windows/
- Or use: `winget install PostgreSQL.PostgreSQL`

**macOS:**
```bash
brew install postgresql
brew services start postgresql
```

**Linux (Ubuntu/Debian):**
```bash
sudo apt update
sudo apt install postgresql postgresql-contrib
sudo systemctl start postgresql
```

### 2. Create Database

After installing PostgreSQL, create the database:

```sql
-- Connect to PostgreSQL
psql -U postgres

-- Create database
CREATE DATABASE "AzShippingDb";

-- Exit
\q
```

Or using command line:
```bash
createdb -U postgres AzShippingDb
```

### 3. Update Connection String (if needed)

If your PostgreSQL credentials are different, update `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=AzShippingDb;Username=YOUR_USERNAME;Password=YOUR_PASSWORD"
  }
}
```

### 4. Run the Application

When you run the application:
- The database will be automatically created if it doesn't exist (`EnsureCreated()`)
- All tables will be created based on your entities
- Seed data will be automatically inserted

### 5. Verify Database

You can verify the database was created:

```bash
psql -U postgres -d AzShippingDb

-- List tables
\dt

-- View data
SELECT * FROM "ClientSegments";
SELECT * FROM "RequestSources";
SELECT * FROM "Packagings";
SELECT * FROM "Users";
SELECT * FROM "SalesFunnelStatuses";
SELECT * FROM "TransportTypes";
```

## Database Schema

The following tables will be created:

- **ClientSegments** - Client segment data
- **RequestSources** - Request source data
- **Packagings** - Packaging types
- **PackagingTranslations** - Packaging translations
- **Users** - User data (fake users for now)
- **SalesFunnelStatuses** - Sales funnel status data
- **SalesFunnelStatusTranslations** - Sales funnel status translations
- **TransportTypes** - Transport type data
- **TransportTypeTranslations** - Transport type translations

## Migration (Optional)

If you want to use EF Core Migrations instead of `EnsureCreated()`:

```bash
# Install EF Core tools (if not already installed)
dotnet tool install --global dotnet-ef

# Create initial migration
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Presentation

# Apply migration
dotnet ef database update --project Infrastructure --startup-project Presentation
```

Then update `Program.cs` to use migrations instead of `EnsureCreated()`.

## Troubleshooting

### Connection Error
If you get a connection error:
1. Verify PostgreSQL is running: `pg_isready` or check services
2. Check connection string in `appsettings.json`
3. Verify database exists: `psql -U postgres -l`
4. Check firewall settings if connecting remotely

### Permission Error
If you get permission errors:
```sql
-- Grant privileges
GRANT ALL PRIVILEGES ON DATABASE "AzShippingDb" TO postgres;
```

### Port Already in Use
If port 5432 is in use, change the port in connection string or stop the conflicting service.

