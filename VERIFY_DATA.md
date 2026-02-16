# Verify Data in PostgreSQL Database

## What Gets Seeded

When you run `dotnet run`, the following data is automatically inserted:

### 1. Client Segments (3 items)
- VIP
- Super Client
- General

### 2. Request Sources (7 items)
- Email
- Client zone
- Telephone
- Skype
- Реклама
- Сайт
- Соц. сети

### 3. Packagings (5 items with translations)
- Box (EN/LT/RU)
- Carton Box (EN/LT/RU)
- EUR-pallet (EN/LT/RU)
- FIN-pallet (EN/LT/RU)
- Wooden box (EN/LT/RU)

### 4. Users (8 fake users)
- John Smith
- Sarah Johnson
- Michael Brown
- Emily Davis
- David Wilson
- Lisa Anderson
- Robert Taylor
- Maria Martinez

### 5. Sales Funnel Statuses (6 items with translations)
- Cold call
- Sent by CP after the call.
- Meeting | Negotiations
- An agreement has been concluded
- Regular work
- Regular customer

### 6. Transport Types (15 items with translations)
- Авто (Road)
- Автовоз (Road)
- Платформа (Road)
- Стандартный тент (Road)
- Тент, 30м² (Road)
- Тент, 50м³ (Road)
- Тент, 86м² (Road)
- 40 м³ закрытый (Road)
- ЖД (Rail)
- Морской (Sea)
- 20' GP (Sea)
- 40' GP (Sea)
- 40' HC (Sea)
- 45' HC (Sea)
- Авиа (Air)

## How to Verify Data

### Method 1: Use pgAdmin (Easiest)

1. Open **pgAdmin**
2. Connect to your server (localhost)
3. Expand: Servers → PostgreSQL → Databases → AzShippingDb → Schemas → public → Tables
4. Right-click any table → View/Edit Data → All Rows
5. You should see all the seeded data!

### Method 2: Use SQL Query Tool in pgAdmin

1. Open pgAdmin
2. Right-click "AzShippingDb" → Query Tool
3. Run these queries:

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

-- View Client Segments
SELECT * FROM "ClientSegments";

-- View Users
SELECT "FirstName", "LastName", "Email" FROM "Users";

-- View Transport Types
SELECT "Name", "IsAir", "IsSea", "IsRoad", "IsRail" FROM "TransportTypes";
```

### Method 3: Check via Your Application

1. Run your application: `dotnet run --project Presentation/AzShipping.Presentation.csproj`
2. Open browser: http://localhost:5062
3. Navigate through the pages:
   - Client Segments page - should show 3 segments
   - Request Sources page - should show 7 sources
   - Packagings page - should show 5 packagings
   - Sales Funnel Status page - should show 6 statuses
   - Transport Types page - should show 15 transport types

### Method 4: Use Full Path to psql

If you find PostgreSQL installation:

```powershell
# Find PostgreSQL (common locations)
Get-ChildItem "C:\Program Files\PostgreSQL" -Recurse -Filter "psql.exe" | Select-Object FullName

# Then use full path:
& "C:\Program Files\PostgreSQL\16\bin\psql.exe" -U postgres -d AzShippingDb -h localhost
# Enter password: 12345@Tt

# Then run:
SELECT COUNT(*) FROM "ClientSegments";
SELECT COUNT(*) FROM "Users";
```

## Expected Results

After running `dotnet run`, you should have:
- ✅ **3** Client Segments
- ✅ **7** Request Sources
- ✅ **5** Packagings (+ translations)
- ✅ **8** Users
- ✅ **6** Sales Funnel Statuses (+ translations)
- ✅ **15** Transport Types (+ translations)

## If Data is Missing

If tables are empty, check:
1. Application logs for errors during startup
2. Database connection is working
3. Seed method is being called (check Program.cs line 65)

The seeding only runs if tables are empty (`.Any()` check), so if you already have data, it won't duplicate.

