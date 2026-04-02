using General.Domain.AggregatesModel.CurrencyAggregate;
using General.Domain.AggregatesModel.IncotermAggregate;
using General.Domain.AggregatesModel.ProjectAggregate;
using General.Domain.AggregatesModel.VasAggregate;
using General.Domain.AggregatesModel.VesselAggregate;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Persistence.Seed;

public static class GeneralDbSeeder
{
    public static async System.Threading.Tasks.Task SeedAsync(GeneralDbContext db)
    {
        var now = DateTime.UtcNow;

        Guid? usdId = null;
        if (!await db.Currencies.AnyAsync())
        {
            var usd = new Currency { Id = Guid.NewGuid(), Code = "USD", Name = "US Dollar", Symbol = "$", NumericCode = 840, CreatedAt = now };
            usdId = usd.Id;
            var currencies = new[]
            {
                usd,
                new Currency { Id = Guid.NewGuid(), Code = "EUR", Name = "Euro", Symbol = "€", NumericCode = 978, CreatedAt = now },
                new Currency { Id = Guid.NewGuid(), Code = "GBP", Name = "British Pound", Symbol = "£", NumericCode = 826, CreatedAt = now },
                new Currency { Id = Guid.NewGuid(), Code = "JPY", Name = "Japanese Yen", Symbol = "¥", NumericCode = 392, CreatedAt = now },
                new Currency { Id = Guid.NewGuid(), Code = "CHF", Name = "Swiss Franc", Symbol = "CHF", NumericCode = 756, CreatedAt = now },
                new Currency { Id = Guid.NewGuid(), Code = "CAD", Name = "Canadian Dollar", Symbol = "C$", NumericCode = 124, CreatedAt = now },
                new Currency { Id = Guid.NewGuid(), Code = "AUD", Name = "Australian Dollar", Symbol = "A$", NumericCode = 36, CreatedAt = now },
                new Currency { Id = Guid.NewGuid(), Code = "CNY", Name = "Chinese Yuan", Symbol = "¥", NumericCode = 156, CreatedAt = now },
                new Currency { Id = Guid.NewGuid(), Code = "AED", Name = "UAE Dirham", Symbol = "د.إ", NumericCode = 784, CreatedAt = now },
                new Currency { Id = Guid.NewGuid(), Code = "SAR", Name = "Saudi Riyal", Symbol = "﷼", NumericCode = 682, CreatedAt = now },
                new Currency { Id = Guid.NewGuid(), Code = "INR", Name = "Indian Rupee", Symbol = "₹", NumericCode = 356, CreatedAt = now },
                new Currency { Id = Guid.NewGuid(), Code = "AZN", Name = "Azerbaijani Manat", Symbol = "₼", NumericCode = 944, CreatedAt = now }
            };
            db.Currencies.AddRange(currencies);
        }

        if (!await db.Projects.AnyAsync())
        {
            var projects = new[]
            {
                new Project { Id = Guid.NewGuid(), Name = "Default", CreatedAt = now },
                new Project { Id = Guid.NewGuid(), Name = "Project Alpha", CreatedAt = now },
                new Project { Id = Guid.NewGuid(), Name = "Project Beta", CreatedAt = now }
            };
            db.Projects.AddRange(projects);
        }

        if (!await db.Incoterms.AnyAsync())
        {
            var incoterms = new[]
            {
                new Incoterm { Id = Guid.NewGuid(), Code = "EXW", Name = "Ex Works", LocalName = "Ex Works", Freight = "Collect", OtherCharges = "Prepaid", IsActive = true, IsDeleted = false, CreatedAt = now },
                new Incoterm { Id = Guid.NewGuid(), Code = "FCA", Name = "Free Carrier", LocalName = "Free Carrier", Freight = "Collect", OtherCharges = "Prepaid", IsActive = true, IsDeleted = false, CreatedAt = now },
                new Incoterm { Id = Guid.NewGuid(), Code = "CPT", Name = "Carriage Paid To", LocalName = "Carriage Paid To", Freight = "Prepaid", OtherCharges = "Collect", IsActive = true, IsDeleted = false, CreatedAt = now },
                new Incoterm { Id = Guid.NewGuid(), Code = "CIP", Name = "Carriage and Insurance Paid To", LocalName = "Carriage and Insurance Paid To", Freight = "Prepaid", OtherCharges = "Prepaid", IsActive = true, IsDeleted = false, CreatedAt = now },
                new Incoterm { Id = Guid.NewGuid(), Code = "DAP", Name = "Delivered at Place", LocalName = "Delivered at Place", Freight = "Prepaid", OtherCharges = "Prepaid", IsActive = true, IsDeleted = false, CreatedAt = now },
                new Incoterm { Id = Guid.NewGuid(), Code = "DPU", Name = "Delivered at Place Unloaded", LocalName = "Delivered at Place Unloaded", Freight = "Prepaid", OtherCharges = "Prepaid", IsActive = true, IsDeleted = false, CreatedAt = now },
                new Incoterm { Id = Guid.NewGuid(), Code = "DDP", Name = "Delivered Duty Paid", LocalName = "Delivered Duty Paid", Freight = "Prepaid", OtherCharges = "Prepaid", IsActive = true, IsDeleted = false, CreatedAt = now }
            };
            db.Incoterms.AddRange(incoterms);
        }

        if (!await db.Vas.AnyAsync())
        {
            var currencyId = usdId ?? (await db.Currencies.FirstAsync(c => c.Code == "USD", cancellationToken: default)).Id;
            var vas = new[]
            {
                new Vas { Id = Guid.NewGuid(), Code = "OVS", Name = "Oversized Cargo Handling", OverWidth = 2.5m, OverHeight = 3m, OverWeight = 20m, Amount = 150m, CurrencyId = currencyId, IsMandatory = false, ExecutionPlace = "Warehouse", Uom = "Shipment", IsAir = true, IsSea = true, IsRoad = true, IsRail = false, IsActive = true, IsDeleted = false, CreatedAt = now },
                new Vas { Id = Guid.NewGuid(), Code = "LCL", Name = "LCL Consolidation", OverWidth = null, OverHeight = null, OverWeight = null, Amount = 75m, CurrencyId = currencyId, IsMandatory = false, ExecutionPlace = "Port", Uom = "CBM", IsAir = false, IsSea = true, IsRoad = false, IsRail = false, IsActive = true, IsDeleted = false, CreatedAt = now },
                new Vas { Id = Guid.NewGuid(), Code = "CRATING", Name = "Export Crating", OverWidth = null, OverHeight = null, OverWeight = null, Amount = null, CurrencyId = null, IsMandatory = false, ExecutionPlace = "Warehouse", Uom = "Piece", IsAir = true, IsSea = true, IsRoad = true, IsRail = true, IsActive = true, IsDeleted = false, CreatedAt = now }
            };
            db.Vas.AddRange(vas);
        }

        if (!await db.Vessels.AnyAsync())
        {
            var vessels = new[]
            {
                new Vessel { Id = Guid.NewGuid(), Code = "MSC-001", Name = "MSC Oscar", ImoCode = "9674190", LocalName = "MSC Oscar", CountryId = null, IsActive = true, IsDeleted = false, CreatedAt = now },
                new Vessel { Id = Guid.NewGuid(), Code = "MAEU-001", Name = "Ever Given", ImoCode = "9811000", LocalName = "Ever Given", CountryId = null, IsActive = true, IsDeleted = false, CreatedAt = now },
                new Vessel { Id = Guid.NewGuid(), Code = "HLAG-001", Name = "HMM Algeciras", ImoCode = "9865188", LocalName = "HMM Algeciras", CountryId = null, IsActive = true, IsDeleted = false, CreatedAt = now }
            };
            db.Vessels.AddRange(vessels);
        }

        await db.SaveChangesAsync();
    }
}
