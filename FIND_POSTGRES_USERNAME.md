# How to Find Your PostgreSQL Username

## Quick Check Methods

### Method 1: Try Common Usernames

The most common PostgreSQL username is **`postgres`**. Try this first in your connection string.

If that doesn't work, try:
- Your Windows username (the name you use to log into Windows)
- `postgres` (default superuser)

### Method 2: Check via Command Line

**Windows (PowerShell or Command Prompt):**
```powershell
# Try to connect with 'postgres' username
psql -U postgres -d AzShippingDb

# If that works, your username is 'postgres'
# If it asks for password, enter: 12345@Tt
```

**If you get "role does not exist" error:**
```powershell
# Try with your Windows username
psql -U $env:USERNAME -d AzShippingDb
```

### Method 3: Check PostgreSQL Configuration

1. Open **pgAdmin** (if installed)
2. Look at the server connection settings
3. The username is shown in the connection dialog

### Method 4: Check Windows Services

1. Open **Services** (services.msc)
2. Find **postgresql-x64-XX** service
3. Right-click → Properties → Log On tab
4. Check the account name (usually "postgres" or your Windows user)

### Method 5: Check PostgreSQL Data Directory

The username is often the same as the folder name in PostgreSQL data directory:
- Default location: `C:\Program Files\PostgreSQL\[version]\data\`
- Look for folders like `base`, `global`, etc.
- The owner of these files is usually your PostgreSQL username

## Most Likely Username

**99% of the time, it's `postgres`** - this is the default superuser account created during PostgreSQL installation.

## Test Connection

Once you update the connection string, test it:

```powershell
# Test connection
psql -U postgres -d AzShippingDb -h localhost
# Enter password when prompted: 12345@Tt
```

If this works, your username is **`postgres`**.

## Updated Connection String

I've already updated your `appsettings.json` with password `12345@Tt` and username `postgres`.

If `postgres` doesn't work, try your Windows username instead.

