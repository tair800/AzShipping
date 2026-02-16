# PostgreSQL Setup Guide

## Prerequisites

1. **Install PostgreSQL**
   - Download and install PostgreSQL from: https://www.postgresql.org/download/
   - Default installation includes:
     - PostgreSQL server
     - pgAdmin (database management tool)
     - Command-line tools

2. **Create Database**
   - Open pgAdmin or use command line
   - Create a new database named `AzShippingDb`

## Configuration

### Connection String

The connection string is configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=AzShippingDb;Username=postgres;Password=postgres"
  }
}
```

### Update Connection String

If your PostgreSQL setup differs, update the connection string with:
- **Host**: Your PostgreSQL server address (default: `localhost`)
- **Port**: PostgreSQL port (default: `5432`)
- **Database**: Database name (default: `AzShippingDb`)
- **Username**: Your PostgreSQL username (default: `postgres`)
- **Password**: Your PostgreSQL password (default: `postgres`)

### Example Connection Strings

**Local PostgreSQL:**
```
Host=localhost;Port=5432;Database=AzShippingDb;Username=postgres;Password=yourpassword
```

**Remote PostgreSQL:**
```
Host=your-server.com;Port=5432;Database=AzShippingDb;Username=youruser;Password=yourpassword
```

**With SSL:**
```
Host=localhost;Port=5432;Database=AzShippingDb;Username=postgres;Password=yourpassword;SSL Mode=Require
```

## Database Creation

The application will automatically create the database schema on first run using `EnsureCreated()`. All tables will be created automatically based on your entity models.

## Initial Data

The application seeds initial data on startup:
- Client Segments (VIP, Super Client, General)
- Request Sources (Email, Client zone, Telephone, etc.)
- Packagings (Box, Carton Box, EUR-pallet, etc.)
- Users (8 fake users)
- Sales Funnel Statuses (6 statuses)
- Transport Types (15 types)

## Running the Application

1. **Start PostgreSQL Server**
   - Make sure PostgreSQL service is running
   - On Windows: Check Services or use `pg_ctl start`

2. **Run the Application**
   ```bash
   dotnet run --project Presentation/AzShipping.Presentation.csproj
   ```

3. **Verify Database**
   - Connect to PostgreSQL using pgAdmin or psql
   - Check that `AzShippingDb` database exists
   - Verify tables are created

## Troubleshooting

### Connection Error
- Verify PostgreSQL service is running
- Check connection string credentials
- Ensure database `AzShippingDb` exists
- Verify firewall allows connections on port 5432

### Migration Issues
- If tables already exist, the seeder will skip seeding
- To reset: Drop and recreate the database

### Port Conflicts
- Default PostgreSQL port is 5432
- If using a different port, update connection string

## Environment Variables

You can also set the connection string via environment variable:

```bash
# Windows PowerShell
$env:DATABASE_URL="Host=localhost;Port=5432;Database=AzShippingDb;Username=postgres;Password=postgres"

# Linux/Mac
export DATABASE_URL="Host=localhost;Port=5432;Database=AzShippingDb;Username=postgres;Password=postgres"
```

The application will use `DATABASE_URL` environment variable if `ConnectionStrings:DefaultConnection` is not set.

