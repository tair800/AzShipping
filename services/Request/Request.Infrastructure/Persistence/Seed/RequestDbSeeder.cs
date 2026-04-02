using Microsoft.EntityFrameworkCore;
using Request.Domain.AggregatesModel.RequestAggregate;
using Request.Domain.AggregatesModel.SaleStatusAggregate;

namespace Request.Infrastructure.Persistence.Seed;

public static class RequestDbSeeder
{
    public static async Task SeedAsync(RequestDbContext db)
    {
        if (!await db.SaleStatuses.AnyAsync())
        {
            var statuses = new[]
            {
                new SaleStatus { Id = Guid.NewGuid(), Name = "Offer sent", SortOrder = 1, IsActive = true, CreatedAt = DateTime.UtcNow },
                new SaleStatus { Id = Guid.NewGuid(), Name = "Negotiations", SortOrder = 2, IsActive = true, CreatedAt = DateTime.UtcNow },
                new SaleStatus { Id = Guid.NewGuid(), Name = "Deal close(Win)", SortOrder = 3, IsActive = true, CreatedAt = DateTime.UtcNow },
                new SaleStatus { Id = Guid.NewGuid(), Name = "Deal close(Lost)", SortOrder = 4, IsActive = true, CreatedAt = DateTime.UtcNow },
            };
            db.SaleStatuses.AddRange(statuses);
            await db.SaveChangesAsync();
        }

        if (!await db.RequestTypes.AnyAsync())
        {
            var types = new[]
            {
                new RequestType { Id = Guid.NewGuid(), Code = "export-air-express", Name = "Export Air Express", Direction = "Export", Mode = "Air", SubType = "Express", RequestNumberPrefix = "EXP-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 1, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "export-air-charter", Name = "Export Air Charter", Direction = "Export", Mode = "Air", SubType = "Charter", RequestNumberPrefix = "EXP-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 2, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "export-air-general", Name = "Export Air General", Direction = "Export", Mode = "Air", SubType = "General", RequestNumberPrefix = "EXP-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 3, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "import-air-express", Name = "Import Air Express", Direction = "Import", Mode = "Air", SubType = "Express", RequestNumberPrefix = "IMP-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 4, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "import-air-charter", Name = "Import Air Charter", Direction = "Import", Mode = "Air", SubType = "Charter", RequestNumberPrefix = "IMP-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 5, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "import-air-general", Name = "Import Air General", Direction = "Import", Mode = "Air", SubType = "General", RequestNumberPrefix = "IMP-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 6, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "transit-air-express", Name = "Transit Air Express", Direction = "Transit", Mode = "Air", SubType = "Express", RequestNumberPrefix = "TRN-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 7, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "transit-air-charter", Name = "Transit Air Charter", Direction = "Transit", Mode = "Air", SubType = "Charter", RequestNumberPrefix = "TRN-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 8, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "transit-air-general", Name = "Transit Air General", Direction = "Transit", Mode = "Air", SubType = "General", RequestNumberPrefix = "TRN-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 9, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "domestic-air-express", Name = "Domestic Air Express", Direction = "Domestic", Mode = "Air", SubType = "Express", RequestNumberPrefix = "DOM-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 10, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "domestic-air-charter", Name = "Domestic Air Charter", Direction = "Domestic", Mode = "Air", SubType = "Charter", RequestNumberPrefix = "DOM-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 11, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "domestic-air-general", Name = "Domestic Air General", Direction = "Domestic", Mode = "Air", SubType = "General", RequestNumberPrefix = "DOM-AIR-", CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = 12, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "export-sea-fcl", Name = "Export Sea FCL", Direction = "Export", Mode = "Sea", SubType = "FCL", RequestNumberPrefix = "EXP-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 13, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "export-sea-lcl", Name = "Export Sea LCL", Direction = "Export", Mode = "Sea", SubType = "LCL", RequestNumberPrefix = "EXP-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 14, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "export-sea-breakbulk", Name = "Export Sea Breakbulk", Direction = "Export", Mode = "Sea", SubType = "Breakbulk", RequestNumberPrefix = "EXP-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 15, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "import-sea-fcl", Name = "Import Sea FCL", Direction = "Import", Mode = "Sea", SubType = "FCL", RequestNumberPrefix = "IMP-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 16, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "import-sea-lcl", Name = "Import Sea LCL", Direction = "Import", Mode = "Sea", SubType = "LCL", RequestNumberPrefix = "IMP-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 17, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "import-sea-breakbulk", Name = "Import Sea Breakbulk", Direction = "Import", Mode = "Sea", SubType = "Breakbulk", RequestNumberPrefix = "IMP-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 18, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "transit-sea-fcl", Name = "Transit Sea FCL", Direction = "Transit", Mode = "Sea", SubType = "FCL", RequestNumberPrefix = "TRN-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 19, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "transit-sea-lcl", Name = "Transit Sea LCL", Direction = "Transit", Mode = "Sea", SubType = "LCL", RequestNumberPrefix = "TRN-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 20, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "transit-sea-breakbulk", Name = "Transit Sea Breakbulk", Direction = "Transit", Mode = "Sea", SubType = "Breakbulk", RequestNumberPrefix = "TRN-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 21, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "domestic-sea-fcl", Name = "Domestic Sea FCL", Direction = "Domestic", Mode = "Sea", SubType = "FCL", RequestNumberPrefix = "DOM-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 22, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "domestic-sea-lcl", Name = "Domestic Sea LCL", Direction = "Domestic", Mode = "Sea", SubType = "LCL", RequestNumberPrefix = "DOM-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 23, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "domestic-sea-breakbulk", Name = "Domestic Sea Breakbulk", Direction = "Domestic", Mode = "Sea", SubType = "Breakbulk", RequestNumberPrefix = "DOM-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 24, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "export-road-ftl", Name = "Export Road FTL", Direction = "Export", Mode = "Road", SubType = "FTL", RequestNumberPrefix = "EXP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 25, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "export-road-breakbulk", Name = "Export Road Breakbulk", Direction = "Export", Mode = "Road", SubType = "Breakbulk", RequestNumberPrefix = "EXP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 26, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "export-road-oog", Name = "Export Road OOG", Direction = "Export", Mode = "Road", SubType = "OOG", RequestNumberPrefix = "EXP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 27, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "import-road-ftl", Name = "Import Road FTL", Direction = "Import", Mode = "Road", SubType = "FTL", RequestNumberPrefix = "IMP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 28, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "import-road-oog", Name = "Import Road OOG", Direction = "Import", Mode = "Road", SubType = "OOG", RequestNumberPrefix = "IMP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 29, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "import-road-breakbulk", Name = "Import Road Breakbulk", Direction = "Import", Mode = "Road", SubType = "Breakbulk", RequestNumberPrefix = "IMP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 30, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "transit-road-ftl", Name = "Transit Road FTL", Direction = "Transit", Mode = "Road", SubType = "FTL", RequestNumberPrefix = "TRN-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 31, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "transit-road-oog", Name = "Transit Road OOG", Direction = "Transit", Mode = "Road", SubType = "OOG", RequestNumberPrefix = "TRN-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 32, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "domestic-road-ftl", Name = "Domestic Road FTL", Direction = "Domestic", Mode = "Road", SubType = "FTL", RequestNumberPrefix = "DOM-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 33, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "domestic-road-breakbulk", Name = "Domestic Road Breakbulk", Direction = "Domestic", Mode = "Road", SubType = "Breakbulk", RequestNumberPrefix = "DOM-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 34, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "export-road-ltl", Name = "Export Road LTL", Direction = "Export", Mode = "Road", SubType = "LTL", RequestNumberPrefix = "EXP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 35, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "import-road-ltl", Name = "Import Road LTL", Direction = "Import", Mode = "Road", SubType = "LTL", RequestNumberPrefix = "IMP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 36, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "transit-road-ltl", Name = "Transit Road LTL", Direction = "Transit", Mode = "Road", SubType = "LTL", RequestNumberPrefix = "TRN-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 37, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "domestic-road-ltl", Name = "Domestic Road LTL", Direction = "Domestic", Mode = "Road", SubType = "LTL", RequestNumberPrefix = "DOM-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 38, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "domestic-road-oog", Name = "Domestic Road OOG", Direction = "Domestic", Mode = "Road", SubType = "OOG", RequestNumberPrefix = "DOM-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 39, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "export-rail-fcl", Name = "Export Rail FCL", Direction = "Export", Mode = "Rail", SubType = "FCL", RequestNumberPrefix = "EXP-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 40, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "export-rail-lcl", Name = "Export Rail LCL", Direction = "Export", Mode = "Rail", SubType = "LCL", RequestNumberPrefix = "EXP-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 41, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "export-rail-oog", Name = "Export Rail OOG", Direction = "Export", Mode = "Rail", SubType = "OOG", RequestNumberPrefix = "EXP-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 42, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "import-rail-fcl", Name = "Import Rail FCL", Direction = "Import", Mode = "Rail", SubType = "FCL", RequestNumberPrefix = "IMP-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 43, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "import-rail-lcl", Name = "Import Rail LCL", Direction = "Import", Mode = "Rail", SubType = "LCL", RequestNumberPrefix = "IMP-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 44, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "import-rail-oog", Name = "Import Rail OOG", Direction = "Import", Mode = "Rail", SubType = "OOG", RequestNumberPrefix = "IMP-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 45, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "transit-rail-fcl", Name = "Transit Rail FCL", Direction = "Transit", Mode = "Rail", SubType = "FCL", RequestNumberPrefix = "TRN-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 46, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "transit-rail-lcl", Name = "Transit Rail LCL", Direction = "Transit", Mode = "Rail", SubType = "LCL", RequestNumberPrefix = "TRN-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 47, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "transit-rail-oog", Name = "Transit Rail OOG", Direction = "Transit", Mode = "Rail", SubType = "OOG", RequestNumberPrefix = "TRN-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 48, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "domestic-rail-fcl", Name = "Domestic Rail FCL", Direction = "Domestic", Mode = "Rail", SubType = "FCL", RequestNumberPrefix = "DOM-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 49, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "domestic-rail-lcl", Name = "Domestic Rail LCL", Direction = "Domestic", Mode = "Rail", SubType = "LCL", RequestNumberPrefix = "DOM-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 50, IsActive = true },
                new RequestType { Id = Guid.NewGuid(), Code = "domestic-rail-oog", Name = "Domestic Rail OOG", Direction = "Domestic", Mode = "Rail", SubType = "OOG", RequestNumberPrefix = "DOM-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 51, IsActive = true },
            };
            db.RequestTypes.AddRange(types);
            await db.SaveChangesAsync();
        }
        else
        {
            foreach (var (code, name, direction, subType, prefix, sortOrder) in new[] {
                ("export-air-express", "Export Air Express", "Export", "Express", "EXP-AIR-", 1), ("export-air-charter", "Export Air Charter", "Export", "Charter", "EXP-AIR-", 2), ("export-air-general", "Export Air General", "Export", "General", "EXP-AIR-", 3),
                ("import-air-express", "Import Air Express", "Import", "Express", "IMP-AIR-", 4), ("import-air-charter", "Import Air Charter", "Import", "Charter", "IMP-AIR-", 5), ("import-air-general", "Import Air General", "Import", "General", "IMP-AIR-", 6),
                ("transit-air-express", "Transit Air Express", "Transit", "Express", "TRN-AIR-", 7), ("transit-air-charter", "Transit Air Charter", "Transit", "Charter", "TRN-AIR-", 8), ("transit-air-general", "Transit Air General", "Transit", "General", "TRN-AIR-", 9),
                ("domestic-air-express", "Domestic Air Express", "Domestic", "Express", "DOM-AIR-", 10), ("domestic-air-charter", "Domestic Air Charter", "Domestic", "Charter", "DOM-AIR-", 11), ("domestic-air-general", "Domestic Air General", "Domestic", "General", "DOM-AIR-", 12)
            })
            {
                if (!await db.RequestTypes.AnyAsync(x => x.Code == code))
                {
                    db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = code, Name = name, Direction = direction, Mode = "Air", SubType = subType, RequestNumberPrefix = prefix, CarrierApiPath = "airlines", CarrierLabel = "Airline", SortOrder = sortOrder, IsActive = true });
                    await db.SaveChangesAsync();
                }
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "export-sea-fcl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "export-sea-fcl", Name = "Export Sea FCL", Direction = "Export", Mode = "Sea", SubType = "FCL", RequestNumberPrefix = "EXP-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 5, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "export-sea-lcl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "export-sea-lcl", Name = "Export Sea LCL", Direction = "Export", Mode = "Sea", SubType = "LCL", RequestNumberPrefix = "EXP-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 6, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "export-sea-breakbulk"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "export-sea-breakbulk", Name = "Export Sea Breakbulk", Direction = "Export", Mode = "Sea", SubType = "Breakbulk", RequestNumberPrefix = "EXP-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 7, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "import-sea-fcl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "import-sea-fcl", Name = "Import Sea FCL", Direction = "Import", Mode = "Sea", SubType = "FCL", RequestNumberPrefix = "IMP-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 8, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "import-sea-lcl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "import-sea-lcl", Name = "Import Sea LCL", Direction = "Import", Mode = "Sea", SubType = "LCL", RequestNumberPrefix = "IMP-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 9, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "import-sea-breakbulk"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "import-sea-breakbulk", Name = "Import Sea Breakbulk", Direction = "Import", Mode = "Sea", SubType = "Breakbulk", RequestNumberPrefix = "IMP-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 10, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "transit-sea-fcl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "transit-sea-fcl", Name = "Transit Sea FCL", Direction = "Transit", Mode = "Sea", SubType = "FCL", RequestNumberPrefix = "TRN-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 11, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "transit-sea-lcl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "transit-sea-lcl", Name = "Transit Sea LCL", Direction = "Transit", Mode = "Sea", SubType = "LCL", RequestNumberPrefix = "TRN-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 12, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "transit-sea-breakbulk"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "transit-sea-breakbulk", Name = "Transit Sea Breakbulk", Direction = "Transit", Mode = "Sea", SubType = "Breakbulk", RequestNumberPrefix = "TRN-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 13, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "domestic-sea-fcl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "domestic-sea-fcl", Name = "Domestic Sea FCL", Direction = "Domestic", Mode = "Sea", SubType = "FCL", RequestNumberPrefix = "DOM-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 14, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "domestic-sea-lcl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "domestic-sea-lcl", Name = "Domestic Sea LCL", Direction = "Domestic", Mode = "Sea", SubType = "LCL", RequestNumberPrefix = "DOM-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 15, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "domestic-sea-breakbulk"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "domestic-sea-breakbulk", Name = "Domestic Sea Breakbulk", Direction = "Domestic", Mode = "Sea", SubType = "Breakbulk", RequestNumberPrefix = "DOM-SEA-", CarrierApiPath = "shippinglines", CarrierLabel = "Shipping line", SortOrder = 16, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "export-road-ftl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "export-road-ftl", Name = "Export Road FTL", Direction = "Export", Mode = "Road", SubType = "FTL", RequestNumberPrefix = "EXP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 17, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "export-road-breakbulk"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "export-road-breakbulk", Name = "Export Road Breakbulk", Direction = "Export", Mode = "Road", SubType = "Breakbulk", RequestNumberPrefix = "EXP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 18, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "export-road-oog"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "export-road-oog", Name = "Export Road OOG", Direction = "Export", Mode = "Road", SubType = "OOG", RequestNumberPrefix = "EXP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 19, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "import-road-ftl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "import-road-ftl", Name = "Import Road FTL", Direction = "Import", Mode = "Road", SubType = "FTL", RequestNumberPrefix = "IMP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 20, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "import-road-oog"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "import-road-oog", Name = "Import Road OOG", Direction = "Import", Mode = "Road", SubType = "OOG", RequestNumberPrefix = "IMP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 21, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "import-road-breakbulk"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "import-road-breakbulk", Name = "Import Road Breakbulk", Direction = "Import", Mode = "Road", SubType = "Breakbulk", RequestNumberPrefix = "IMP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 22, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "transit-road-ftl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "transit-road-ftl", Name = "Transit Road FTL", Direction = "Transit", Mode = "Road", SubType = "FTL", RequestNumberPrefix = "TRN-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 23, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "transit-road-oog"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "transit-road-oog", Name = "Transit Road OOG", Direction = "Transit", Mode = "Road", SubType = "OOG", RequestNumberPrefix = "TRN-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 24, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "domestic-road-ftl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "domestic-road-ftl", Name = "Domestic Road FTL", Direction = "Domestic", Mode = "Road", SubType = "FTL", RequestNumberPrefix = "DOM-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 25, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "domestic-road-breakbulk"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "domestic-road-breakbulk", Name = "Domestic Road Breakbulk", Direction = "Domestic", Mode = "Road", SubType = "Breakbulk", RequestNumberPrefix = "DOM-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 26, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "export-road-ltl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "export-road-ltl", Name = "Export Road LTL", Direction = "Export", Mode = "Road", SubType = "LTL", RequestNumberPrefix = "EXP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 27, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "import-road-ltl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "import-road-ltl", Name = "Import Road LTL", Direction = "Import", Mode = "Road", SubType = "LTL", RequestNumberPrefix = "IMP-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 28, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "transit-road-ltl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "transit-road-ltl", Name = "Transit Road LTL", Direction = "Transit", Mode = "Road", SubType = "LTL", RequestNumberPrefix = "TRN-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 29, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "domestic-road-ltl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "domestic-road-ltl", Name = "Domestic Road LTL", Direction = "Domestic", Mode = "Road", SubType = "LTL", RequestNumberPrefix = "DOM-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 30, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "domestic-road-oog"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "domestic-road-oog", Name = "Domestic Road OOG", Direction = "Domestic", Mode = "Road", SubType = "OOG", RequestNumberPrefix = "DOM-RD-", CarrierApiPath = "carriers", CarrierLabel = "Trucker", SortOrder = 31, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "export-rail-lcl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "export-rail-lcl", Name = "Export Rail LCL", Direction = "Export", Mode = "Rail", SubType = "LCL", RequestNumberPrefix = "EXP-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 32, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "import-rail-lcl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "import-rail-lcl", Name = "Import Rail LCL", Direction = "Import", Mode = "Rail", SubType = "LCL", RequestNumberPrefix = "IMP-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 33, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "transit-rail-lcl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "transit-rail-lcl", Name = "Transit Rail LCL", Direction = "Transit", Mode = "Rail", SubType = "LCL", RequestNumberPrefix = "TRN-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 34, IsActive = true });
                await db.SaveChangesAsync();
            }
            if (!await db.RequestTypes.AnyAsync(x => x.Code == "domestic-rail-lcl"))
            {
                db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = "domestic-rail-lcl", Name = "Domestic Rail LCL", Direction = "Domestic", Mode = "Rail", SubType = "LCL", RequestNumberPrefix = "DOM-RL-", CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = 35, IsActive = true });
                await db.SaveChangesAsync();
            }
            foreach (var (code, name, direction, subType, prefix, sortOrder) in new[] {
                ("export-rail-fcl", "Export Rail FCL", "Export", "FCL", "EXP-RL-", 40), ("export-rail-oog", "Export Rail OOG", "Export", "OOG", "EXP-RL-", 42),
                ("import-rail-fcl", "Import Rail FCL", "Import", "FCL", "IMP-RL-", 43), ("import-rail-oog", "Import Rail OOG", "Import", "OOG", "IMP-RL-", 45),
                ("transit-rail-fcl", "Transit Rail FCL", "Transit", "FCL", "TRN-RL-", 46), ("transit-rail-oog", "Transit Rail OOG", "Transit", "OOG", "TRN-RL-", 48),
                ("domestic-rail-fcl", "Domestic Rail FCL", "Domestic", "FCL", "DOM-RL-", 49), ("domestic-rail-oog", "Domestic Rail OOG", "Domestic", "OOG", "DOM-RL-", 51)
            })
            {
                if (!await db.RequestTypes.AnyAsync(x => x.Code == code))
                {
                    db.RequestTypes.Add(new RequestType { Id = Guid.NewGuid(), Code = code, Name = name, Direction = direction, Mode = "Rail", SubType = subType, RequestNumberPrefix = prefix, CarrierApiPath = "railwaystations", CarrierLabel = "Shipping line", SortOrder = sortOrder, IsActive = true });
                    await db.SaveChangesAsync();
                }
            }
        }

        if (!await db.Requests.AnyAsync())
        {
            var types = await db.RequestTypes.Where(x => x.IsActive).OrderBy(x => x.SortOrder).ToListAsync();
            var now = DateTime.UtcNow;
            var baseTime = now.AddDays(-30).Ticks;

            foreach (var rt in types)
            {
                var req = new RequestEntity
                {
                    Id = Guid.NewGuid(),
                    CreationDate = now.AddDays(-types.IndexOf(rt) * 2),
                    CreatedAt = now,
                    RequestNumber = rt.RequestNumberPrefix + (baseTime + types.IndexOf(rt) * 1000000),
                    RequestTypeId = rt.Id,
                    CompanyName = "Sample Company Ltd",
                    ManagerName = "John Manager",
                    LogisticianName = "Jane Logistician",
                    DepartmentName = "Logistics",
                    ShipperName = "ABC Shipper Inc",
                    ConsigneeName = "XYZ Consignee Corp",
                    DispatchDateFrom = now.AddDays(7),
                    UnloadingDateFrom = now.AddDays(14),
                    QuotationSent = true,
                    StatusName = "Initial contact",
                    ExtremelyUrgent = false,
                    PriceStandard = 1500,
                    CurrencyCode = "USD",
                    PriceWithVat = 1650,
                    VatRate = "10%",
                    SourceOfRequestName = "Website",
                    RequestPurposeName = "Quote",
                    GatewayName = "Port of Baku",
                    ViaPortName = "Port of Istanbul",
                    DestinationName = "Port of Batumi",
                    CarrierName = "MSC / Maersk Line",
                    DescriptionOfGoods = "General cargo",
                    Notes = "Sample request for " + rt.Name,
                    IsActive = true
                };

                var isFclOrBreakbulk = (string.Equals(rt.Mode, "Sea", StringComparison.OrdinalIgnoreCase)
                        && (string.Equals(rt.SubType, "FCL", StringComparison.OrdinalIgnoreCase) || string.Equals(rt.SubType, "Breakbulk", StringComparison.OrdinalIgnoreCase)))
                    || (string.Equals(rt.Mode, "Road", StringComparison.OrdinalIgnoreCase) && (string.Equals(rt.SubType, "FTL", StringComparison.OrdinalIgnoreCase) || string.Equals(rt.SubType, "Breakbulk", StringComparison.OrdinalIgnoreCase)))
                    || (string.Equals(rt.Mode, "Rail", StringComparison.OrdinalIgnoreCase) && string.Equals(rt.SubType, "FCL", StringComparison.OrdinalIgnoreCase));

                var isTransitBreakbulk = string.Equals(rt.Direction, "Transit", StringComparison.OrdinalIgnoreCase)
                    && string.Equals(rt.SubType, "Breakbulk", StringComparison.OrdinalIgnoreCase);
                var isTransitRoad = string.Equals(rt.Direction, "Transit", StringComparison.OrdinalIgnoreCase)
                    && string.Equals(rt.Mode, "Road", StringComparison.OrdinalIgnoreCase);
                var isTransitRail = string.Equals(rt.Direction, "Transit", StringComparison.OrdinalIgnoreCase)
                    && string.Equals(rt.Mode, "Rail", StringComparison.OrdinalIgnoreCase);
                if (isTransitBreakbulk)
                    req.TransitPortName = "Port of Poti";
                else if (isTransitRoad)
                    req.TransitPortName = "Tbilisi";
                else if (isTransitRail)
                    req.TransitPortName = "Tbilisi Central";

                if (isFclOrBreakbulk)
                {
                    var pkgType = string.Equals(rt.SubType, "Breakbulk", StringComparison.OrdinalIgnoreCase) ? "Pallets"
                        : string.Equals(rt.SubType, "FTL", StringComparison.OrdinalIgnoreCase) ? "13.6m" : "40' HC";
                    req.GrossWeightKg = 15000;
                    req.VolumeCbm = 65;
                    req.ChargeableWeightKg = 65;
                    req.NumberOfPackages = 2;
                    db.Requests.Add(req);
                    await db.SaveChangesAsync();
                    db.RequestDimensions.Add(new RequestDimension
                    {
                        Id = Guid.NewGuid(),
                        RequestId = req.Id,
                        Length = 0,
                        Width = 0,
                        Height = 0,
                        Quantity = 2,
                        PackageType = pkgType,
                        VolumeCbm = 65
                    });
                }
                else
                {
                    req.GrossWeightKg = 1250;
                    req.VolumeCbm = 5.5m;
                    req.ChargeableWeightKg = 916;
                    req.NumberOfPackages = 50;
                    db.Requests.Add(req);
                    await db.SaveChangesAsync();
                    db.RequestDimensions.Add(new RequestDimension
                    {
                        Id = Guid.NewGuid(),
                        RequestId = req.Id,
                        Length = 120,
                        Width = 80,
                        Height = 100,
                        Quantity = 50,
                        WeightKg = 25,
                        VolumeCbm = 4.8m
                    });
                }
            }
            await db.SaveChangesAsync();
        }
    }
}
