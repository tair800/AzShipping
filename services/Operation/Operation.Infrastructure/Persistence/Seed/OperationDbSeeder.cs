using Microsoft.EntityFrameworkCore;
using Operation.Domain.AggregatesModel.OperationAggregate;

namespace Operation.Infrastructure.Persistence.Seed;

public static class OperationDbSeeder
{
    public static async Task SeedAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        if (await context.OperationTypes.AnyAsync(cancellationToken))
            return;

        var types = new[]
        {
            new OperationType { Id = Guid.NewGuid(), Code = "export-unimodal-air-express", Name = "Export Unimodal Air Express", Direction = "Export", Mode = "Air", SubType = "Express", OperationNumberPrefix = "OP-EXP-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 1, IsActive = true },
            new OperationType { Id = Guid.NewGuid(), Code = "export-unimodal-air-charter", Name = "Export Unimodal Air Charter", Direction = "Export", Mode = "Air", SubType = "Charter", OperationNumberPrefix = "OP-EXP-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 2, IsActive = true },
            new OperationType { Id = Guid.NewGuid(), Code = "export-unimodal-air-general", Name = "Export Unimodal Air General", Direction = "Export", Mode = "Air", SubType = "General", OperationNumberPrefix = "OP-EXP-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 3, IsActive = true },
        };
        context.OperationTypes.AddRange(types);
        await context.SaveChangesAsync(cancellationToken);
    }

    public static async Task EnsureExportAirMultimodalTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, string subType, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Export",
                Mode = "Air",
                SubType = subType,
                OperationNumberPrefix = "OP-EXP-AIR-",
                CarrierApiPath = "airlines",
                CarrierLabel = "Airline",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("export-multimodal-air-express", "Export Multimodal Air Express", "Express", 4);
        await AddIfMissing("export-multimodal-air-charter", "Export Multimodal Air Charter", "Charter", 5);
        await AddIfMissing("export-multimodal-air-general", "Export Multimodal Air General", "General", 6);
    }

    /// <summary>Adds Import + Air + Express/Charter/General (unimodal and multimodal) if missing.</summary>
    public static async Task EnsureImportAirTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, string subType, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Import",
                Mode = "Air",
                SubType = subType,
                OperationNumberPrefix = "OP-IMP-AIR-",
                CarrierApiPath = "airlines",
                CarrierLabel = "Airline",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("import-unimodal-air-express", "Import Unimodal Air Express", "Express", 7);
        await AddIfMissing("import-unimodal-air-charter", "Import Unimodal Air Charter", "Charter", 8);
        await AddIfMissing("import-unimodal-air-general", "Import Unimodal Air General", "General", 9);
        await AddIfMissing("import-multimodal-air-express", "Import Multimodal Air Express", "Express", 10);
        await AddIfMissing("import-multimodal-air-charter", "Import Multimodal Air Charter", "Charter", 11);
        await AddIfMissing("import-multimodal-air-general", "Import Multimodal Air General", "General", 12);
    }

    /// <summary>Adds Transit + Air + Express/Charter/General (unimodal and multimodal) if missing.</summary>
    public static async Task EnsureTransitAirTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, string subType, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Transit",
                Mode = "Air",
                SubType = subType,
                OperationNumberPrefix = "OP-TRN-AIR-",
                CarrierApiPath = "airlines",
                CarrierLabel = "Airline",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("transit-unimodal-air-express", "Transit Unimodal Air Express", "Express", 13);
        await AddIfMissing("transit-unimodal-air-charter", "Transit Unimodal Air Charter", "Charter", 14);
        await AddIfMissing("transit-unimodal-air-general", "Transit Unimodal Air General", "General", 15);
        await AddIfMissing("transit-multimodal-air-express", "Transit Multimodal Air Express", "Express", 16);
        await AddIfMissing("transit-multimodal-air-charter", "Transit Multimodal Air Charter", "Charter", 17);
        await AddIfMissing("transit-multimodal-air-general", "Transit Multimodal Air General", "General", 18);
    }

    /// <summary>Adds Domestic + Air + Express/Charter/General (unimodal and multimodal) if missing.</summary>
    public static async Task EnsureDomesticAirTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, string subType, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Domestic",
                Mode = "Air",
                SubType = subType,
                OperationNumberPrefix = "OP-DOM-AIR-",
                CarrierApiPath = "airlines",
                CarrierLabel = "Airline",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("domestic-unimodal-air-express", "Domestic Unimodal Air Express", "Express", 19);
        await AddIfMissing("domestic-unimodal-air-charter", "Domestic Unimodal Air Charter", "Charter", 20);
        await AddIfMissing("domestic-unimodal-air-general", "Domestic Unimodal Air General", "General", 21);
        await AddIfMissing("domestic-multimodal-air-express", "Domestic Multimodal Air Express", "Express", 22);
        await AddIfMissing("domestic-multimodal-air-charter", "Domestic Multimodal Air Charter", "Charter", 23);
        await AddIfMissing("domestic-multimodal-air-general", "Domestic Multimodal Air General", "General", 24);
    }

    /// <summary>Export + Sea + FCL (unimodal and multimodal).</summary>
    public static async Task EnsureExportSeaFclTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Export",
                Mode = "Sea",
                SubType = "FCL",
                OperationNumberPrefix = "OP-EXP-SEA-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("export-unimodal-sea-fcl", "Export Unimodal Sea FCL", 25);
        await AddIfMissing("export-multimodal-sea-fcl", "Export Multimodal Sea FCL", 26);
    }

    /// <summary>Export + Rail + FCL (unimodal and multimodal). Package lines like sea FCL; prefix <c>OP-EXP-RAIL-</c>.</summary>
    public static async Task EnsureExportRailFclTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Export",
                Mode = "Rail",
                SubType = "FCL",
                OperationNumberPrefix = "OP-EXP-RAIL-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("export-unimodal-rail-fcl", "Export Unimodal Rail FCL", 77);
        await AddIfMissing("export-multimodal-rail-fcl", "Export Multimodal Rail FCL", 78);
    }

    /// <summary>Export + Rail + LCL (unimodal and multimodal). Fill dimensions; 1000 kg/CBM; prefix <c>OP-EXP-RAIL-</c>.</summary>
    public static async Task EnsureExportRailLclTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Export",
                Mode = "Rail",
                SubType = "LCL",
                OperationNumberPrefix = "OP-EXP-RAIL-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("export-unimodal-rail-lcl", "Export Unimodal Rail LCL", 79);
        await AddIfMissing("export-multimodal-rail-lcl", "Export Multimodal Rail LCL", 80);
    }

    /// <summary>Export + Rail + Breakbulk (unimodal and multimodal). Package lines; optional VAS; prefix <c>OP-EXP-RAIL-</c>.</summary>
    public static async Task EnsureExportRailBreakbulkTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Export",
                Mode = "Rail",
                SubType = "Breakbulk",
                OperationNumberPrefix = "OP-EXP-RAIL-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("export-unimodal-rail-breakbulk", "Export Unimodal Rail Breakbulk", 81);
        await AddIfMissing("export-multimodal-rail-breakbulk", "Export Multimodal Rail Breakbulk", 82);
    }

    /// <summary>Import + Rail + FCL (unimodal and multimodal). Package lines; prefix <c>OP-IMP-RAIL-</c>.</summary>
    public static async Task EnsureImportRailFclTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Import",
                Mode = "Rail",
                SubType = "FCL",
                OperationNumberPrefix = "OP-IMP-RAIL-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("import-unimodal-rail-fcl", "Import Unimodal Rail FCL", 83);
        await AddIfMissing("import-multimodal-rail-fcl", "Import Multimodal Rail FCL", 84);
    }

    /// <summary>Import + Rail + LCL (unimodal and multimodal). Fill dimensions; 1000 kg/CBM; prefix <c>OP-IMP-RAIL-</c>.</summary>
    public static async Task EnsureImportRailLclTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Import",
                Mode = "Rail",
                SubType = "LCL",
                OperationNumberPrefix = "OP-IMP-RAIL-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("import-unimodal-rail-lcl", "Import Unimodal Rail LCL", 85);
        await AddIfMissing("import-multimodal-rail-lcl", "Import Multimodal Rail LCL", 86);
    }

    /// <summary>Import + Rail + Breakbulk (unimodal and multimodal). Package lines; optional VAS; prefix <c>OP-IMP-RAIL-</c>.</summary>
    public static async Task EnsureImportRailBreakbulkTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Import",
                Mode = "Rail",
                SubType = "Breakbulk",
                OperationNumberPrefix = "OP-IMP-RAIL-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("import-unimodal-rail-breakbulk", "Import Unimodal Rail Breakbulk", 87);
        await AddIfMissing("import-multimodal-rail-breakbulk", "Import Multimodal Rail Breakbulk", 88);
    }

    /// <summary>Transit + Rail + FCL (unimodal and multimodal). Package lines; prefix <c>OP-TRN-RAIL-</c>.</summary>
    public static async Task EnsureTransitRailFclTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Transit",
                Mode = "Rail",
                SubType = "FCL",
                OperationNumberPrefix = "OP-TRN-RAIL-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("transit-unimodal-rail-fcl", "Transit Unimodal Rail FCL", 89);
        await AddIfMissing("transit-multimodal-rail-fcl", "Transit Multimodal Rail FCL", 90);
    }

    /// <summary>Transit + Rail + LCL (unimodal and multimodal). Fill dimensions; 1000 kg/CBM; prefix <c>OP-TRN-RAIL-</c>.</summary>
    public static async Task EnsureTransitRailLclTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Transit",
                Mode = "Rail",
                SubType = "LCL",
                OperationNumberPrefix = "OP-TRN-RAIL-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("transit-unimodal-rail-lcl", "Transit Unimodal Rail LCL", 91);
        await AddIfMissing("transit-multimodal-rail-lcl", "Transit Multimodal Rail LCL", 92);
    }

    /// <summary>Transit + Rail + Breakbulk (unimodal and multimodal). Package lines; optional VAS; prefix <c>OP-TRN-RAIL-</c>.</summary>
    public static async Task EnsureTransitRailBreakbulkTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Transit",
                Mode = "Rail",
                SubType = "Breakbulk",
                OperationNumberPrefix = "OP-TRN-RAIL-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("transit-unimodal-rail-breakbulk", "Transit Unimodal Rail Breakbulk", 93);
        await AddIfMissing("transit-multimodal-rail-breakbulk", "Transit Multimodal Rail Breakbulk", 94);
    }

    /// <summary>Domestic + Rail + FCL (unimodal and multimodal). Package lines; prefix <c>OP-DOM-RAIL-</c>.</summary>
    public static async Task EnsureDomesticRailFclTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Domestic",
                Mode = "Rail",
                SubType = "FCL",
                OperationNumberPrefix = "OP-DOM-RAIL-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("domestic-unimodal-rail-fcl", "Domestic Unimodal Rail FCL", 95);
        await AddIfMissing("domestic-multimodal-rail-fcl", "Domestic Multimodal Rail FCL", 96);
    }

    /// <summary>Import + Sea + FCL (unimodal and multimodal).</summary>
    public static async Task EnsureImportSeaFclTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Import",
                Mode = "Sea",
                SubType = "FCL",
                OperationNumberPrefix = "OP-IMP-SEA-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("import-unimodal-sea-fcl", "Import Unimodal Sea FCL", 31);
        await AddIfMissing("import-multimodal-sea-fcl", "Import Multimodal Sea FCL", 32);
    }

    /// <summary>Import + Sea + LCL (unimodal and multimodal).</summary>
    public static async Task EnsureImportSeaLclTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Import",
                Mode = "Sea",
                SubType = "LCL",
                OperationNumberPrefix = "OP-IMP-SEA-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("import-unimodal-sea-lcl", "Import Unimodal Sea LCL", 33);
        await AddIfMissing("import-multimodal-sea-lcl", "Import Multimodal Sea LCL", 34);
    }

    /// <summary>Export + Sea + LCL (unimodal and multimodal).</summary>
    public static async Task EnsureExportSeaLclTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Export",
                Mode = "Sea",
                SubType = "LCL",
                OperationNumberPrefix = "OP-EXP-SEA-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("export-unimodal-sea-lcl", "Export Unimodal Sea LCL", 27);
        await AddIfMissing("export-multimodal-sea-lcl", "Export Multimodal Sea LCL", 28);
    }

    /// <summary>Export + Sea + Breakbulk (unimodal and multimodal).</summary>
    public static async Task EnsureExportSeaBreakbulkTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Export",
                Mode = "Sea",
                SubType = "Breakbulk",
                OperationNumberPrefix = "OP-EXP-SEA-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("export-unimodal-sea-breakbulk", "Export Unimodal Sea Breakbulk", 29);
        await AddIfMissing("export-multimodal-sea-breakbulk", "Export Multimodal Sea Breakbulk", 30);
    }

    /// <summary>Import + Sea + Breakbulk (unimodal and multimodal).</summary>
    public static async Task EnsureImportSeaBreakbulkTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Import",
                Mode = "Sea",
                SubType = "Breakbulk",
                OperationNumberPrefix = "OP-IMP-SEA-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("import-unimodal-sea-breakbulk", "Import Unimodal Sea Breakbulk", 35);
        await AddIfMissing("import-multimodal-sea-breakbulk", "Import Multimodal Sea Breakbulk", 36);
    }

    /// <summary>Transit + Sea + FCL (unimodal and multimodal).</summary>
    public static async Task EnsureTransitSeaFclTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Transit",
                Mode = "Sea",
                SubType = "FCL",
                OperationNumberPrefix = "OP-TRN-SEA-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("transit-unimodal-sea-fcl", "Transit Unimodal Sea FCL", 37);
        await AddIfMissing("transit-multimodal-sea-fcl", "Transit Multimodal Sea FCL", 38);
    }

    /// <summary>Transit + Sea + LCL (unimodal and multimodal).</summary>
    public static async Task EnsureTransitSeaLclTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Transit",
                Mode = "Sea",
                SubType = "LCL",
                OperationNumberPrefix = "OP-TRN-SEA-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("transit-unimodal-sea-lcl", "Transit Unimodal Sea LCL", 39);
        await AddIfMissing("transit-multimodal-sea-lcl", "Transit Multimodal Sea LCL", 40);
    }

    /// <summary>Transit + Sea + Breakbulk (unimodal and multimodal).</summary>
    public static async Task EnsureTransitSeaBreakbulkTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Transit",
                Mode = "Sea",
                SubType = "Breakbulk",
                OperationNumberPrefix = "OP-TRN-SEA-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("transit-unimodal-sea-breakbulk", "Transit Unimodal Sea Breakbulk", 41);
        await AddIfMissing("transit-multimodal-sea-breakbulk", "Transit Multimodal Sea Breakbulk", 42);
    }

    /// <summary>Domestic + Sea + FCL (unimodal and multimodal).</summary>
    public static async Task EnsureDomesticSeaFclTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Domestic",
                Mode = "Sea",
                SubType = "FCL",
                OperationNumberPrefix = "OP-DOM-SEA-",
                CarrierApiPath = "shippinglines",
                CarrierLabel = "Shipping line",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("domestic-unimodal-sea-fcl", "Domestic Unimodal Sea FCL", 43);
        await AddIfMissing("domestic-multimodal-sea-fcl", "Domestic Multimodal Sea FCL", 44);
    }

    /// <summary>Export + Road + FTL (unimodal and multimodal).</summary>
    public static async Task EnsureExportRoadFtlTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Export",
                Mode = "Road",
                SubType = "FTL",
                OperationNumberPrefix = "OP-EXP-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("export-unimodal-road-ftl", "Export Unimodal Road FTL", 45);
        await AddIfMissing("export-multimodal-road-ftl", "Export Multimodal Road FTL", 46);
    }

    /// <summary>Import + Road + FTL (unimodal and multimodal).</summary>
    public static async Task EnsureImportRoadFtlTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Import",
                Mode = "Road",
                SubType = "FTL",
                OperationNumberPrefix = "OP-IMP-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("import-unimodal-road-ftl", "Import Unimodal Road FTL", 53);
        await AddIfMissing("import-multimodal-road-ftl", "Import Multimodal Road FTL", 54);
    }

    /// <summary>Import + Road + LTL (unimodal and multimodal). Fill dimensions; 1000 kg/CBM W/M (same as export road LTL).</summary>
    public static async Task EnsureImportRoadLtlTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Import",
                Mode = "Road",
                SubType = "LTL",
                OperationNumberPrefix = "OP-IMP-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("import-unimodal-road-ltl", "Import Unimodal Road LTL", 55);
        await AddIfMissing("import-multimodal-road-ltl", "Import Multimodal Road LTL", 56);
    }

    /// <summary>Import + Road + Breakbulk (unimodal and multimodal). Package lines; optional VAS.</summary>
    public static async Task EnsureImportRoadBreakbulkTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Import",
                Mode = "Road",
                SubType = "Breakbulk",
                OperationNumberPrefix = "OP-IMP-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("import-unimodal-road-breakbulk", "Import Unimodal Road Breakbulk", 57);
        await AddIfMissing("import-multimodal-road-breakbulk", "Import Multimodal Road Breakbulk", 58);
    }

    /// <summary>Import + Road + OOG (unimodal and multimodal). Fill dimensions (W/M 1000 kg/CBM); optional VAS.</summary>
    public static async Task EnsureImportRoadOogTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Import",
                Mode = "Road",
                SubType = "OOG",
                OperationNumberPrefix = "OP-IMP-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("import-unimodal-road-oog", "Import Unimodal Road OOG", 59);
        await AddIfMissing("import-multimodal-road-oog", "Import Multimodal Road OOG", 60);
    }

    /// <summary>Export + Road + LTL (unimodal and multimodal).</summary>
    public static async Task EnsureExportRoadLtlTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Export",
                Mode = "Road",
                SubType = "LTL",
                OperationNumberPrefix = "OP-EXP-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("export-unimodal-road-ltl", "Export Unimodal Road LTL", 47);
        await AddIfMissing("export-multimodal-road-ltl", "Export Multimodal Road LTL", 48);
    }

    /// <summary>Export + Road + Breakbulk (unimodal and multimodal).</summary>
    public static async Task EnsureExportRoadBreakbulkTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Export",
                Mode = "Road",
                SubType = "Breakbulk",
                OperationNumberPrefix = "OP-EXP-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("export-unimodal-road-breakbulk", "Export Unimodal Road Breakbulk", 49);
        await AddIfMissing("export-multimodal-road-breakbulk", "Export Multimodal Road Breakbulk", 50);
    }

    /// <summary>Export + Road + OOG (unimodal and multimodal). Totals / Fill dimensions (W/M 1000 kg/CBM); optional VAS.</summary>
    public static async Task EnsureExportRoadOogTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Export",
                Mode = "Road",
                SubType = "OOG",
                OperationNumberPrefix = "OP-EXP-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("export-unimodal-road-oog", "Export Unimodal Road OOG", 51);
        await AddIfMissing("export-multimodal-road-oog", "Export Multimodal Road OOG", 52);
    }

    /// <summary>Transit + Road + FTL (unimodal and multimodal).</summary>
    public static async Task EnsureTransitRoadFtlTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Transit",
                Mode = "Road",
                SubType = "FTL",
                OperationNumberPrefix = "OP-TRN-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("transit-unimodal-road-ftl", "Transit Unimodal Road FTL", 61);
        await AddIfMissing("transit-multimodal-road-ftl", "Transit Multimodal Road FTL", 62);
    }

    /// <summary>Transit + Road + LTL (unimodal and multimodal). Fill dimensions; 1000 kg/CBM W/M.</summary>
    public static async Task EnsureTransitRoadLtlTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Transit",
                Mode = "Road",
                SubType = "LTL",
                OperationNumberPrefix = "OP-TRN-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("transit-unimodal-road-ltl", "Transit Unimodal Road LTL", 63);
        await AddIfMissing("transit-multimodal-road-ltl", "Transit Multimodal Road LTL", 64);
    }

    /// <summary>Transit + Road + Breakbulk (unimodal and multimodal). Package lines; optional VAS.</summary>
    public static async Task EnsureTransitRoadBreakbulkTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Transit",
                Mode = "Road",
                SubType = "Breakbulk",
                OperationNumberPrefix = "OP-TRN-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("transit-unimodal-road-breakbulk", "Transit Unimodal Road Breakbulk", 65);
        await AddIfMissing("transit-multimodal-road-breakbulk", "Transit Multimodal Road Breakbulk", 66);
    }

    /// <summary>Transit + Road + OOG (unimodal and multimodal). Fill dimensions (W/M 1000 kg/CBM); optional VAS.</summary>
    public static async Task EnsureTransitRoadOogTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Transit",
                Mode = "Road",
                SubType = "OOG",
                OperationNumberPrefix = "OP-TRN-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("transit-unimodal-road-oog", "Transit Unimodal Road OOG", 67);
        await AddIfMissing("transit-multimodal-road-oog", "Transit Multimodal Road OOG", 68);
    }

    /// <summary>Domestic + Road + FTL (unimodal and multimodal).</summary>
    public static async Task EnsureDomesticRoadFtlTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Domestic",
                Mode = "Road",
                SubType = "FTL",
                OperationNumberPrefix = "OP-DOM-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("domestic-unimodal-road-ftl", "Domestic Unimodal Road FTL", 69);
        await AddIfMissing("domestic-multimodal-road-ftl", "Domestic Multimodal Road FTL", 70);
    }

    /// <summary>Domestic + Road + LTL (unimodal and multimodal). Fill dimensions; 1000 kg/CBM W/M.</summary>
    public static async Task EnsureDomesticRoadLtlTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Domestic",
                Mode = "Road",
                SubType = "LTL",
                OperationNumberPrefix = "OP-DOM-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("domestic-unimodal-road-ltl", "Domestic Unimodal Road LTL", 71);
        await AddIfMissing("domestic-multimodal-road-ltl", "Domestic Multimodal Road LTL", 72);
    }

    /// <summary>Domestic + Road + Breakbulk (unimodal and multimodal). Package lines; optional VAS.</summary>
    public static async Task EnsureDomesticRoadBreakbulkTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Domestic",
                Mode = "Road",
                SubType = "Breakbulk",
                OperationNumberPrefix = "OP-DOM-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("domestic-unimodal-road-breakbulk", "Domestic Unimodal Road Breakbulk", 73);
        await AddIfMissing("domestic-multimodal-road-breakbulk", "Domestic Multimodal Road Breakbulk", 74);
    }

    /// <summary>Domestic + Road + OOG (unimodal and multimodal). Fill dimensions; 1000 kg/CBM; optional VAS.</summary>
    public static async Task EnsureDomesticRoadOogTypesAsync(OperationDbContext context, CancellationToken cancellationToken = default)
    {
        async Task AddIfMissing(string code, string name, int sortOrder)
        {
            if (await context.OperationTypes.AnyAsync(x => x.Code == code, cancellationToken)) return;
            context.OperationTypes.Add(new OperationType
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Direction = "Domestic",
                Mode = "Road",
                SubType = "OOG",
                OperationNumberPrefix = "OP-DOM-ROAD-",
                CarrierApiPath = "carriers",
                CarrierLabel = "Trucker",
                SortOrder = sortOrder,
                IsActive = true
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        await AddIfMissing("domestic-unimodal-road-oog", "Domestic Unimodal Road OOG", 75);
        await AddIfMissing("domestic-multimodal-road-oog", "Domestic Multimodal Road OOG", 76);
    }

    /// <summary>Stable GUIDs — must match Accounting sample invoice seed (<c>OperationInvoice.OperationId</c>).</summary>
    public static readonly Guid DemoLogisticsOperationId1 = Guid.Parse("a1111111-1111-4111-8111-111111111101");
    public static readonly Guid DemoLogisticsOperationId2 = Guid.Parse("a1111111-1111-4111-8111-111111111102");

    /// <summary>Two demo operations for invoice list / operation-detail smoke tests (idempotent).</summary>
    public static async Task SeedDemoLogisticsOperationsAsync(OperationDbContext context,
        CancellationToken cancellationToken = default)
    {
        var typeId = await context.OperationTypes.AsNoTracking()
            .Where(t => t.Code == "export-unimodal-air-express")
            .Select(t => t.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (typeId == Guid.Empty)
            return;

        var now = DateTime.UtcNow;
        if (!await context.Operations.AnyAsync(o => o.Id == DemoLogisticsOperationId1, cancellationToken))
        {
            context.Operations.Add(new LogisticsOperation
            {
                Id = DemoLogisticsOperationId1,
                CreationDate = now.Date,
                CreatedAt = now,
                OperationNumber = "OP-DEMO-1001",
                OperationTypeId = typeId,
                ModalType = "Unimodal",
                PricingMode = "RoutingRates",
                MyCustomerName = "Caspian Trading LLC",
                ShipperName = "Baku Export Co.",
                ConsigneeName = "Rotterdam Imports BV",
                CurrencyCode = "USD",
                DescriptionOfGoods = "Demo air cargo — electronics spares",
                OperationStageName = "Planning",
                IsActive = true
            });
        }

        if (!await context.Operations.AnyAsync(o => o.Id == DemoLogisticsOperationId2, cancellationToken))
        {
            context.Operations.Add(new LogisticsOperation
            {
                Id = DemoLogisticsOperationId2,
                CreationDate = now.Date,
                CreatedAt = now,
                OperationNumber = "OP-DEMO-1002",
                OperationTypeId = typeId,
                ModalType = "Unimodal",
                PricingMode = "RoutingRates",
                MyCustomerName = "Silk Route Freight",
                ShipperName = "Istanbul Hub Ltd",
                CurrencyCode = "EUR",
                DescriptionOfGoods = "Demo consignment — textile rolls",
                OperationStageName = "Planning",
                IsActive = true
            });
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
