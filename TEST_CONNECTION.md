# Testing PostgreSQL Connection

Since `psql` is not in your PATH, here are alternative ways to test the connection:

## Method 1: Use Full Path to psql

Find PostgreSQL installation and use full path:

```powershell
# Common locations:
"C:\Program Files\PostgreSQL\16\bin\psql.exe" -U postgres -d AzShippingDb -h localhost
"C:\Program Files\PostgreSQL\15\bin\psql.exe" -U postgres -d AzShippingDb -h localhost
"C:\Program Files\PostgreSQL\14\bin\psql.exe" -U postgres -d AzShippingDb -h localhost
```

## Method 2: Use pgAdmin (GUI)

1. Open **pgAdmin** (usually in Start Menu)
2. Right-click "Servers" → Create → Server
3. General tab: Name = "AzShipping"
4. Connection tab:
   - Host: `localhost`
   - Port: `5432`
   - Database: `AzShippingDb`
   - Username: `postgres`
   - Password: `12345@Tt`
5. Click "Save"
6. If it connects, your credentials are correct!

## Method 3: Just Run Your Application

The easiest way is to just **run your .NET application**:

```powershell
dotnet run --project Presentation/AzShipping.Presentation.csproj
```

If it connects successfully, you'll see the application start without database errors.

If there's a connection error, it will tell you what's wrong (wrong username, password, etc.).

## Method 4: Add PostgreSQL to PATH (Optional)

If you want to use `psql` from anywhere:

1. Find PostgreSQL bin folder (usually `C:\Program Files\PostgreSQL\16\bin`)
2. Add to PATH:
   - Right-click "This PC" → Properties
   - Advanced System Settings → Environment Variables
   - Edit "Path" → Add PostgreSQL bin folder
   - Restart terminal

## Current Configuration

Your connection string is set to:
- **Host**: localhost
- **Port**: 5432
- **Database**: AzShippingDb
- **Username**: postgres
- **Password**: 12345@Tt

## Quick Test

Just run your application - if it starts without errors, the connection works!

```powershell
cd C:\Users\taira\OneDrive\Desktop\AzShipping
dotnet run --project Presentation/AzShipping.Presentation.csproj
```

