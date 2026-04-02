using Microsoft.EntityFrameworkCore;
using Quotes.Domain.AggregatesModel.QuoteAggregate;

namespace Quotes.Infrastructure.Persistence.Seed;

public static class QuotesDbSeeder
{
    public static async Task SeedAsync(QuotesDbContext db)
    {
        if (!await db.QuoteTypes.AnyAsync())
        {
            var types = new[]
            {
                new QuoteType { Id = Guid.NewGuid(), Code = "export-air-express", Name = "Export Air Express", Direction = "Export", Mode = "Air", SubType = "Express", QuoteNumberPrefix = "EXP-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 1, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "domestic-air-express", Name = "Domestic Air Express", Direction = "Domestic", Mode = "Air", SubType = "Express", QuoteNumberPrefix = "DOM-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 2, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "transit-air-express", Name = "Transit Air Express", Direction = "Transit", Mode = "Air", SubType = "Express", QuoteNumberPrefix = "TRN-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 3, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "export-air-charter", Name = "Export Air Charter", Direction = "Export", Mode = "Air", SubType = "Charter", QuoteNumberPrefix = "EXP-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 4, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "export-air-general", Name = "Export Air General", Direction = "Export", Mode = "Air", SubType = "General", QuoteNumberPrefix = "EXP-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 5, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "export-sea-fcl", Name = "Export Sea FCL", Direction = "Export", Mode = "Sea", SubType = "FCL", QuoteNumberPrefix = "EXP-SEA-", CarrierApiPath = "shipping-lines", CarrierLabel = "Shipping line", SortOrder = 6, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "export-sea-lcl", Name = "Export Sea LCL", Direction = "Export", Mode = "Sea", SubType = "LCL", QuoteNumberPrefix = "EXP-SEA-", CarrierApiPath = "shipping-lines", CarrierLabel = "Shipping line", SortOrder = 7, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "export-sea-break-bulk", Name = "Export Sea Break Bulk", Direction = "Export", Mode = "Sea", SubType = "Break Bulk", QuoteNumberPrefix = "EXP-SEA-", CarrierApiPath = "shipping-lines", CarrierLabel = "Shipping line", SortOrder = 8, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "import-sea-fcl", Name = "Import Sea FCL", Direction = "Import", Mode = "Sea", SubType = "FCL", QuoteNumberPrefix = "IMP-SEA-", CarrierApiPath = "shipping-lines", CarrierLabel = "Shipping line", SortOrder = 9, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "import-sea-lcl", Name = "Import Sea LCL", Direction = "Import", Mode = "Sea", SubType = "LCL", QuoteNumberPrefix = "IMP-SEA-", CarrierApiPath = "shipping-lines", CarrierLabel = "Shipping line", SortOrder = 10, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "import-sea-break-bulk", Name = "Import Sea Break Bulk", Direction = "Import", Mode = "Sea", SubType = "Break Bulk", QuoteNumberPrefix = "IMP-SEA-", CarrierApiPath = "shipping-lines", CarrierLabel = "Shipping line", SortOrder = 11, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "transit-sea-fcl", Name = "Transit Sea FCL", Direction = "Transit", Mode = "Sea", SubType = "FCL", QuoteNumberPrefix = "TRN-SEA-", CarrierApiPath = "shipping-lines", CarrierLabel = "Shipping line", SortOrder = 12, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "transit-sea-lcl", Name = "Transit Sea LCL", Direction = "Transit", Mode = "Sea", SubType = "LCL", QuoteNumberPrefix = "TRN-SEA-", CarrierApiPath = "shipping-lines", CarrierLabel = "Shipping line", SortOrder = 13, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "transit-sea-break-bulk", Name = "Transit Sea Break Bulk", Direction = "Transit", Mode = "Sea", SubType = "Break Bulk", QuoteNumberPrefix = "TRN-SEA-", CarrierApiPath = "shipping-lines", CarrierLabel = "Shipping line", SortOrder = 14, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "import-air-express", Name = "Import Air Express", Direction = "Import", Mode = "Air", SubType = "Express", QuoteNumberPrefix = "IMP-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 15, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "import-air-charter", Name = "Import Air Charter", Direction = "Import", Mode = "Air", SubType = "Charter", QuoteNumberPrefix = "IMP-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 16, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "import-air-general", Name = "Import Air General", Direction = "Import", Mode = "Air", SubType = "General", QuoteNumberPrefix = "IMP-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 17, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "export-road-ftl", Name = "Export Road FTL", Direction = "Export", Mode = "Road", SubType = "FTL", QuoteNumberPrefix = "EXP-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 18, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "export-road-ltl", Name = "Export Road LTL", Direction = "Export", Mode = "Road", SubType = "LTL", QuoteNumberPrefix = "EXP-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 19, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "export-road-break-bulk", Name = "Export Road Break Bulk", Direction = "Export", Mode = "Road", SubType = "Break Bulk", QuoteNumberPrefix = "EXP-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 20, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "export-road-oog", Name = "Export Road OOG", Direction = "Export", Mode = "Road", SubType = "OOG", QuoteNumberPrefix = "EXP-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 21, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "import-road-ftl", Name = "Import Road FTL", Direction = "Import", Mode = "Road", SubType = "FTL", QuoteNumberPrefix = "IMP-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 24, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "import-road-ltl", Name = "Import Road LTL", Direction = "Import", Mode = "Road", SubType = "LTL", QuoteNumberPrefix = "IMP-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 25, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "import-road-break-bulk", Name = "Import Road Break Bulk", Direction = "Import", Mode = "Road", SubType = "Break Bulk", QuoteNumberPrefix = "IMP-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 24, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "import-road-oog", Name = "Import Road OOG", Direction = "Import", Mode = "Road", SubType = "OOG", QuoteNumberPrefix = "IMP-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 25, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "transit-road-ftl", Name = "Transit Road FTL", Direction = "Transit", Mode = "Road", SubType = "FTL", QuoteNumberPrefix = "TRN-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 26, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "transit-road-ltl", Name = "Transit Road LTL", Direction = "Transit", Mode = "Road", SubType = "LTL", QuoteNumberPrefix = "TRN-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 27, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "transit-road-break-bulk", Name = "Transit Road Break Bulk", Direction = "Transit", Mode = "Road", SubType = "Break Bulk", QuoteNumberPrefix = "TRN-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 28, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "transit-road-oog", Name = "Transit Road OOG", Direction = "Transit", Mode = "Road", SubType = "OOG", QuoteNumberPrefix = "TRN-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 29, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "export-rail-fcl", Name = "Export Rail FCL", Direction = "Export", Mode = "Rail", SubType = "FCL", QuoteNumberPrefix = "EXP-RAIL-", CarrierApiPath = "rail-carriers", CarrierLabel = "Rail carrier", SortOrder = 24, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "export-rail-lcl", Name = "Export Rail LCL", Direction = "Export", Mode = "Rail", SubType = "LCL", QuoteNumberPrefix = "EXP-RAIL-", CarrierApiPath = "rail-carriers", CarrierLabel = "Rail carrier", SortOrder = 25, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "export-rail-break-bulk", Name = "Export Rail Break Bulk", Direction = "Export", Mode = "Rail", SubType = "Break Bulk", QuoteNumberPrefix = "EXP-RAIL-", CarrierApiPath = "rail-carriers", CarrierLabel = "Rail carrier", SortOrder = 26, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "import-rail-fcl", Name = "Import Rail FCL", Direction = "Import", Mode = "Rail", SubType = "FCL", QuoteNumberPrefix = "IMP-RAIL-", CarrierApiPath = "rail-carriers", CarrierLabel = "Rail carrier", SortOrder = 27, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "import-rail-lcl", Name = "Import Rail LCL", Direction = "Import", Mode = "Rail", SubType = "LCL", QuoteNumberPrefix = "IMP-RAIL-", CarrierApiPath = "rail-carriers", CarrierLabel = "Rail carrier", SortOrder = 28, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "import-rail-break-bulk", Name = "Import Rail Break Bulk", Direction = "Import", Mode = "Rail", SubType = "Break Bulk", QuoteNumberPrefix = "IMP-RAIL-", CarrierApiPath = "rail-carriers", CarrierLabel = "Rail carrier", SortOrder = 29, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "transit-rail-fcl", Name = "Transit Rail FCL", Direction = "Transit", Mode = "Rail", SubType = "FCL", QuoteNumberPrefix = "TRN-RAIL-", CarrierApiPath = "rail-carriers", CarrierLabel = "Shipping line", SortOrder = 30, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "transit-rail-lcl", Name = "Transit Rail LCL", Direction = "Transit", Mode = "Rail", SubType = "LCL", QuoteNumberPrefix = "TRN-RAIL-", CarrierApiPath = "rail-carriers", CarrierLabel = "Shipping line", SortOrder = 31, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "transit-rail-break-bulk", Name = "Transit Rail Break Bulk", Direction = "Transit", Mode = "Rail", SubType = "Break Bulk", QuoteNumberPrefix = "TRN-RAIL-", CarrierApiPath = "rail-carriers", CarrierLabel = "Shipping line", SortOrder = 32, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "domestic-sea-fcl", Name = "Domestic Sea FCL", Direction = "Domestic", Mode = "Sea", SubType = "FCL", QuoteNumberPrefix = "DOM-SEA-", CarrierApiPath = "shipping-lines", CarrierLabel = "Shipping line", SortOrder = 33, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "domestic-sea-lcl", Name = "Domestic Sea LCL", Direction = "Domestic", Mode = "Sea", SubType = "LCL", QuoteNumberPrefix = "DOM-SEA-", CarrierApiPath = "shipping-lines", CarrierLabel = "Shipping line", SortOrder = 34, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "domestic-sea-break-bulk", Name = "Domestic Sea Break Bulk", Direction = "Domestic", Mode = "Sea", SubType = "Break Bulk", QuoteNumberPrefix = "DOM-SEA-", CarrierApiPath = "shipping-lines", CarrierLabel = "Shipping line", SortOrder = 35, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "domestic-road-ftl", Name = "Domestic Road FTL", Direction = "Domestic", Mode = "Road", SubType = "FTL", QuoteNumberPrefix = "DOM-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 36, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "domestic-road-ltl", Name = "Domestic Road LTL", Direction = "Domestic", Mode = "Road", SubType = "LTL", QuoteNumberPrefix = "DOM-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 37, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "domestic-road-break-bulk", Name = "Domestic Road Break Bulk", Direction = "Domestic", Mode = "Road", SubType = "Break Bulk", QuoteNumberPrefix = "DOM-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 38, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "domestic-road-oog", Name = "Domestic Road OOG", Direction = "Domestic", Mode = "Road", SubType = "OOG", QuoteNumberPrefix = "DOM-ROAD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 39, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "domestic-rail-fcl", Name = "Domestic Rail FCL", Direction = "Domestic", Mode = "Rail", SubType = "FCL", QuoteNumberPrefix = "DOM-RAIL-", CarrierApiPath = "rail-carriers", CarrierLabel = "Shipping line", SortOrder = 40, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "domestic-rail-lcl", Name = "Domestic Rail LCL", Direction = "Domestic", Mode = "Rail", SubType = "LCL", QuoteNumberPrefix = "DOM-RAIL-", CarrierApiPath = "rail-carriers", CarrierLabel = "Shipping line", SortOrder = 41, IsActive = true },
                new QuoteType { Id = Guid.NewGuid(), Code = "domestic-rail-break-bulk", Name = "Domestic Rail Break Bulk", Direction = "Domestic", Mode = "Rail", SubType = "Break Bulk", QuoteNumberPrefix = "DOM-RAIL-", CarrierApiPath = "rail-carriers", CarrierLabel = "Shipping line", SortOrder = 42, IsActive = true },
            };
            db.QuoteTypes.AddRange(types);
            await db.SaveChangesAsync();
        }
        else
        {
            // Add missing Sea types (when DB was seeded earlier with Air only)
            var seaTypes = new (string Code, string Name, string Direction, string SubType, string Prefix, int SortOrder)[]
            {
                ("export-sea-fcl", "Export Sea FCL", "Export", "FCL", "EXP-SEA-", 6),
                ("export-sea-lcl", "Export Sea LCL", "Export", "LCL", "EXP-SEA-", 7),
                ("export-sea-break-bulk", "Export Sea Break Bulk", "Export", "Break Bulk", "EXP-SEA-", 8),
                ("import-sea-fcl", "Import Sea FCL", "Import", "FCL", "IMP-SEA-", 9),
                ("import-sea-lcl", "Import Sea LCL", "Import", "LCL", "IMP-SEA-", 10),
                ("import-sea-break-bulk", "Import Sea Break Bulk", "Import", "Break Bulk", "IMP-SEA-", 11),
                ("transit-sea-fcl", "Transit Sea FCL", "Transit", "FCL", "TRN-SEA-", 12),
                ("transit-sea-lcl", "Transit Sea LCL", "Transit", "LCL", "TRN-SEA-", 13),
                ("transit-sea-break-bulk", "Transit Sea Break Bulk", "Transit", "Break Bulk", "TRN-SEA-", 14),
                ("domestic-sea-fcl", "Domestic Sea FCL", "Domestic", "FCL", "DOM-SEA-", 30),
                ("domestic-sea-lcl", "Domestic Sea LCL", "Domestic", "LCL", "DOM-SEA-", 31),
                ("domestic-sea-break-bulk", "Domestic Sea Break Bulk", "Domestic", "Break Bulk", "DOM-SEA-", 32),
            };
            foreach (var (code, name, direction, subType, prefix, sortOrder) in seaTypes)
            {
                if (!await db.QuoteTypes.AnyAsync(x => x.Code == code))
                {
                    db.QuoteTypes.Add(new QuoteType { Id = Guid.NewGuid(), Code = code, Name = name, Direction = direction, Mode = "Sea", SubType = subType, QuoteNumberPrefix = prefix, CarrierApiPath = "shipping-lines", CarrierLabel = "Shipping line", SortOrder = sortOrder, IsActive = true });
                    await db.SaveChangesAsync();
                }
            }

            // Add missing Road types (FTL, LTL, Break Bulk, OOG only)
            var roadTypes = new (string Code, string Name, string Direction, string SubType, string Prefix, int SortOrder)[]
            {
                ("export-road-ftl", "Export Road FTL", "Export", "FTL", "EXP-ROAD-", 18),
                ("export-road-ltl", "Export Road LTL", "Export", "LTL", "EXP-ROAD-", 19),
                ("export-road-break-bulk", "Export Road Break Bulk", "Export", "Break Bulk", "EXP-ROAD-", 20),
                ("export-road-oog", "Export Road OOG", "Export", "OOG", "EXP-ROAD-", 21),
                ("import-road-ftl", "Import Road FTL", "Import", "FTL", "IMP-ROAD-", 22),
                ("import-road-ltl", "Import Road LTL", "Import", "LTL", "IMP-ROAD-", 23),
                ("import-road-break-bulk", "Import Road Break Bulk", "Import", "Break Bulk", "IMP-ROAD-", 24),
                ("import-road-oog", "Import Road OOG", "Import", "OOG", "IMP-ROAD-", 25),
                ("transit-road-ftl", "Transit Road FTL", "Transit", "FTL", "TRN-ROAD-", 26),
                ("transit-road-ltl", "Transit Road LTL", "Transit", "LTL", "TRN-ROAD-", 27),
                ("transit-road-break-bulk", "Transit Road Break Bulk", "Transit", "Break Bulk", "TRN-ROAD-", 28),
                ("transit-road-oog", "Transit Road OOG", "Transit", "OOG", "TRN-ROAD-", 29),
                ("domestic-road-ftl", "Domestic Road FTL", "Domestic", "FTL", "DOM-ROAD-", 30),
                ("domestic-road-ltl", "Domestic Road LTL", "Domestic", "LTL", "DOM-ROAD-", 31),
                ("domestic-road-break-bulk", "Domestic Road Break Bulk", "Domestic", "Break Bulk", "DOM-ROAD-", 32),
                ("domestic-road-oog", "Domestic Road OOG", "Domestic", "OOG", "DOM-ROAD-", 33),
            };
            foreach (var (code, name, direction, subType, prefix, sortOrder) in roadTypes)
            {
                if (!await db.QuoteTypes.AnyAsync(x => x.Code == code))
                {
                    var carrierLabel = "Trucker";
                    db.QuoteTypes.Add(new QuoteType { Id = Guid.NewGuid(), Code = code, Name = name, Direction = direction, Mode = "Road", SubType = subType, QuoteNumberPrefix = prefix, CarrierApiPath = "carriers", CarrierLabel = carrierLabel, SortOrder = sortOrder, IsActive = true });
                    await db.SaveChangesAsync();
                }
            }

            // Add missing Rail types
            var railTypes = new (string Code, string Name, string Direction, string SubType, string Prefix, int SortOrder)[]
            {
                ("export-rail-fcl", "Export Rail FCL", "Export", "FCL", "EXP-RAIL-", 24),
                ("export-rail-lcl", "Export Rail LCL", "Export", "LCL", "EXP-RAIL-", 25),
                ("export-rail-break-bulk", "Export Rail Break Bulk", "Export", "Break Bulk", "EXP-RAIL-", 26),
                ("import-rail-fcl", "Import Rail FCL", "Import", "FCL", "IMP-RAIL-", 27),
                ("import-rail-lcl", "Import Rail LCL", "Import", "LCL", "IMP-RAIL-", 28),
                ("import-rail-break-bulk", "Import Rail Break Bulk", "Import", "Break Bulk", "IMP-RAIL-", 29),
                ("transit-rail-fcl", "Transit Rail FCL", "Transit", "FCL", "TRN-RAIL-", 30),
                ("transit-rail-lcl", "Transit Rail LCL", "Transit", "LCL", "TRN-RAIL-", 31),
                ("transit-rail-break-bulk", "Transit Rail Break Bulk", "Transit", "Break Bulk", "TRN-RAIL-", 32),
                ("domestic-rail-fcl", "Domestic Rail FCL", "Domestic", "FCL", "DOM-RAIL-", 40),
                ("domestic-rail-lcl", "Domestic Rail LCL", "Domestic", "LCL", "DOM-RAIL-", 41),
                ("domestic-rail-break-bulk", "Domestic Rail Break Bulk", "Domestic", "Break Bulk", "DOM-RAIL-", 42),
            };
            foreach (var (code, name, direction, subType, prefix, sortOrder) in railTypes)
            {
                if (!await db.QuoteTypes.AnyAsync(x => x.Code == code))
                {
                    var carrierLabel = (code == "transit-rail-fcl" || code == "transit-rail-lcl" || code == "transit-rail-break-bulk" || code == "domestic-rail-fcl" || code == "domestic-rail-lcl" || code == "domestic-rail-break-bulk") ? "Shipping line" : "Rail carrier";
                    db.QuoteTypes.Add(new QuoteType { Id = Guid.NewGuid(), Code = code, Name = name, Direction = direction, Mode = "Rail", SubType = subType, QuoteNumberPrefix = prefix, CarrierApiPath = "rail-carriers", CarrierLabel = carrierLabel, SortOrder = sortOrder, IsActive = true });
                    await db.SaveChangesAsync();
                }
            }
        }

        if (!await db.Quotes.AnyAsync())
        {
            var types = await db.QuoteTypes.Where(x => x.IsActive).OrderBy(x => x.SortOrder).ToListAsync();
            var now = DateTime.UtcNow;
            var baseTime = now.AddDays(-60).Ticks;
            var sampleQuotes = new List<QuoteEntity>();

            foreach (var t in types)
            {
                var quote = new QuoteEntity
                {
                    Id = Guid.NewGuid(),
                    CreationDate = now.AddDays(-sampleQuotes.Count),
                    QuoteNumber = t.QuoteNumberPrefix + (baseTime + sampleQuotes.Count * 1000000),
                    QuoteTypeId = t.Id,
                    CompanyName = "Sample Co",
                    ShipperName = "Sample Shipper",
                    ConsigneeName = "Sample Consignee",
                    RateType = "Spot Rate",
                    StartDate = now.AddDays(7),
                    ExpirationDate = now.AddDays(37),
                    PriceStandard = 1500m,
                    CurrencyCode = "USD",
                    IncludePickup = false,
                    IncludeDelivery = false,
                    IsActive = true,
                    CreatedAt = now,
                };

                var isSea = string.Equals(t.Mode, "Sea", StringComparison.OrdinalIgnoreCase);
                var isTransitSea = isSea && string.Equals(t.Direction, "Transit", StringComparison.OrdinalIgnoreCase);
                var isDomesticSea = isSea && string.Equals(t.Direction, "Domestic", StringComparison.OrdinalIgnoreCase);
                var isFcl = string.Equals(t.SubType, "FCL", StringComparison.OrdinalIgnoreCase);
                var isBreakBulk = string.Equals(t.SubType, "Break Bulk", StringComparison.OrdinalIgnoreCase);
                var isFtl = string.Equals(t.SubType, "FTL", StringComparison.OrdinalIgnoreCase);
                var isFclOrBreakBulk = isFcl || isBreakBulk;
                var isRoad = string.Equals(t.Mode, "Road", StringComparison.OrdinalIgnoreCase);
                var isRoadFtlOrFcl = isRoad && (isFtl || isFcl); // includes Import Road FTL
                var isRoadLtl = isRoad && (string.Equals(t.SubType, "LTL", StringComparison.OrdinalIgnoreCase) || string.Equals(t.SubType, "LCL", StringComparison.OrdinalIgnoreCase));
                var isRoadBreakBulk = isRoad && isBreakBulk;
                var isRoadOog = isRoad && string.Equals(t.SubType, "OOG", StringComparison.OrdinalIgnoreCase);

                if (isSea)
                {
                    quote.GatewayName = "Port of Baku";
                    quote.ViaPortName = "Port of Istanbul";
                    quote.DestinationName = isDomesticSea ? null : "Port of Batumi";
                    quote.CarrierName = "MSC / Maersk Line";
                    quote.PortOfDeliveryName = "Batumi Warehouse";
                    if (isTransitSea)
                    {
                        quote.MyPortName = "Port of Poti";
                        quote.ViaPort2Name = "CMA CGM";
                        quote.MyPort2Name = "Port of Constanța";
                    }
                    else if (isDomesticSea)
                    {
                        if (isFcl)
                        {
                            quote.MyPortName = "Port of Poti";
                            quote.MyPort2Name = "Port of Constanța";
                        }
                        else
                        {
                            quote.DestinationName = "Port of Batumi";
                            quote.MyPort2Name = "Port of Constanța";
                        }
                    }

                    if (isFclOrBreakBulk)
                    {
                        quote.Quantity1 = 2;
                        quote.PackageType1 = string.Equals(t.SubType, "Break Bulk", StringComparison.OrdinalIgnoreCase) ? "Pallets" : "40' HC";
                        quote.Quantity2 = 1;
                        quote.PackageType2 = "20' DC";
                        quote.GrossWeightKg = 15000;
                        quote.VolumeCbm = 65;
                    }
                    else
                    {
                        quote.GrossWeightKg = 1250;
                        quote.VolumeCbm = 5.5m;
                        quote.ChargeableWeightKg = 916;
                        quote.NumberOfPackages = 50;
                    }
                }
                else if (isRoad)
                {
                    quote.GatewayName = "Berlin Warehouse";
                    quote.ViaPortName = "Hannover";
                    quote.DestinationName = "Rotterdam Port";
                    quote.MyPort2Name = "Duisburg";
                    quote.CarrierName = "DB Schenker";
                    quote.PortOfDeliveryName = "Rotterdam Terminal";
                    if (isRoadFtlOrFcl)
                    {
                        quote.Quantity1 = 2;
                        quote.PackageType1 = isFtl ? "Pallets" : "20' DC";
                        quote.Quantity2 = 1;
                        quote.PackageType2 = "Pallets";
                    }
                    else if (isRoadBreakBulk)
                    {
                        quote.IncludeVas = true;
                        quote.VasServiceName = "Loading";
                        quote.ExecutionPlace = "Origin";
                        quote.VasQuantity = 1;
                        quote.VasUom = "Hour";
                        quote.VasCurrencyCode = "USD";
                        quote.VasTotal = 150m;
                        quote.Quantity1 = 3;
                        quote.PackageType1 = "Pallets";
                        quote.Quantity2 = 1;
                        quote.PackageType2 = "Crate";
                    }
                    else if (isRoadLtl)
                    {
                        quote.GrossWeightKg = 850;
                        quote.VolumeCbm = 4.2m;
                        quote.ChargeableWeightKg = 850;
                        quote.NumberOfPackages = 24;
                    }
                    else if (isRoadOog)
                    {
                        quote.IncludeVas = true;
                        quote.VasServiceName = "Crane";
                        quote.ExecutionPlace = "Origin";
                        quote.VasQuantity = 1;
                        quote.VasUom = "Unit";
                        quote.VasCurrencyCode = "USD";
                        quote.VasTotal = 500m;
                        quote.GrossWeightKg = 25000;
                        quote.VolumeCbm = 120m;
                        quote.ChargeableWeightKg = 25000;
                        quote.NumberOfPackages = 1;
                    }
                }

                sampleQuotes.Add(quote);
            }

            db.Quotes.AddRange(sampleQuotes);
            await db.SaveChangesAsync();
        }
    }
}
