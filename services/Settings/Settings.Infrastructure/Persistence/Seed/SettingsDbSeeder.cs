using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.BankAggregate;
using Settings.Domain.AggregatesModel.CarrierTypeAggregate;
using Settings.Domain.AggregatesModel.CityAggregate;
using Settings.Domain.AggregatesModel.ClientSegmentAggregate;
using Settings.Domain.AggregatesModel.CountryAggregate;
using Settings.Domain.AggregatesModel.DeferredPaymentConditionAggregate;
using Settings.Domain.AggregatesModel.DrivingLicenceCategoryAggregate;
using Settings.Domain.AggregatesModel.LoadingMethodAggregate;
using Settings.Domain.AggregatesModel.PackagingAggregate;
using Settings.Domain.AggregatesModel.RequestPurposeAggregate;
using Settings.Domain.AggregatesModel.RequestSourceAggregate;
using Settings.Domain.AggregatesModel.QuoteSourceAggregate;
using Settings.Domain.AggregatesModel.SalesFunnelStatusAggregate;
using Settings.Domain.AggregatesModel.StateAggregate;
using Settings.Domain.AggregatesModel.TransportTypeAggregate;
using Settings.Domain.AggregatesModel.WorkerPostAggregate;
using Settings.Domain.AggregatesModel.WayOfNegotiationAggregate;
using Settings.Domain.AggregatesModel.ResultTypeAggregate;
using Settings.Domain.AggregatesModel.FunnelResultAggregate;
using Settings.Domain.AggregatesModel.CompanyAggregate;
using Settings.Domain.AggregatesModel.ClientSourceAggregate;
using Settings.Domain.AggregatesModel.ExecutionPlaceAggregate;
using Settings.Domain.AggregatesModel.MeetingTypeAggregate;
using TaskStatusEntity = Settings.Domain.AggregatesModel.TaskStatusAggregate.TaskStatus;
using Settings.Domain.AggregatesModel.TaskPriorityAggregate;
using Settings.Domain.AggregatesModel.MeetingStatusAggregate;
using Settings.Domain.AggregatesModel.MeetingResultAggregate;
using Settings.Domain.AggregatesModel.MeetingPriorityAggregate;
using Settings.Domain.AggregatesModel.UomAggregate;
using Settings.Domain.AggregatesModel.GlobalZoneAggregate;
using Settings.Domain.AggregatesModel.PricingTypeAggregate;
using Settings.Domain.AggregatesModel.AddressTypeAggregate;
using Settings.Domain.AggregatesModel.SystemLogAggregate;
using Settings.Domain.AggregatesModel.TemplateAggregate;
using Settings.Domain.AggregatesModel.EmployeeGroupAggregate;

namespace Settings.Infrastructure.Persistence.Seed;

public static class SettingsDbSeeder
{
    public static async Task SeedAsync(SettingsDbContext context, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        if (!await context.Packagings.AnyAsync(cancellationToken))
        {
            var packagings = new[]
            {
                new { Name = "Box", EN = "Box", LT = "Dėžė", RU = "Коробка" },
                new { Name = "Pallet", EN = "Pallet", LT = "Palečių", RU = "Паллета" },
                new { Name = "Container", EN = "Container", LT = "Konteineris", RU = "Контейнер" },
                new { Name = "Crate", EN = "Crate", LT = "Lentynė", RU = "Ящик" },
                new { Name = "Bag", EN = "Bag", LT = "Maišas", RU = "Мешок" },
            };
            foreach (var p in packagings)
            {
                var id = Guid.NewGuid();
                context.Packagings.Add(new Packaging { Id = id, Name = p.Name, IsActive = true, CreatedAt = now });
                context.PackagingTranslations.Add(new PackagingTranslation { Id = Guid.NewGuid(), PackagingId = id, LanguageCode = "EN", Name = p.EN });
                context.PackagingTranslations.Add(new PackagingTranslation { Id = Guid.NewGuid(), PackagingId = id, LanguageCode = "LT", Name = p.LT });
                context.PackagingTranslations.Add(new PackagingTranslation { Id = Guid.NewGuid(), PackagingId = id, LanguageCode = "RU", Name = p.RU });
            }
        }

        if (!await context.CarrierTypes.AnyAsync(cancellationToken))
        {
            foreach (var name in new[] { "Standard", "Express", "Economy" })
                context.CarrierTypes.Add(new CarrierType { Id = Guid.NewGuid(), Name = name, IsActive = true, CreatedAt = now });
        }

        if (!await context.ExecutionPlaces.AnyAsync(cancellationToken))
        {
            foreach (var name in new[] { "Baku", "Tbilisi", "Istanbul", "Vilnius" })
                context.ExecutionPlaces.Add(new ExecutionPlace { Id = Guid.NewGuid(), Name = name, IsActive = true, CreatedAt = now });
        }

        if (!await context.MeetingTypes.AnyAsync(cancellationToken))
        {
            foreach (var name in new[] { "Online", "In Person", "Phone" })
                context.MeetingTypes.Add(new MeetingType { Id = Guid.NewGuid(), Name = name, IsActive = true, CreatedAt = now });
        }

        if (!await context.TaskStatuses.AnyAsync(cancellationToken))
        {
            context.TaskStatuses.Add(new TaskStatusEntity { Id = Guid.NewGuid(), Name = "Open", PrimaryColor = "#3498DB", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
            context.TaskStatuses.Add(new TaskStatusEntity { Id = Guid.NewGuid(), Name = "In Progress", PrimaryColor = "#f39c12", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
            context.TaskStatuses.Add(new TaskStatusEntity { Id = Guid.NewGuid(), Name = "Done", PrimaryColor = "#2ecc71", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
        }

        if (!await context.TaskPriorities.AnyAsync(cancellationToken))
        {
            context.TaskPriorities.Add(new TaskPriority { Id = Guid.NewGuid(), Name = "Low", PrimaryColor = "#95a5a6", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
            context.TaskPriorities.Add(new TaskPriority { Id = Guid.NewGuid(), Name = "Medium", PrimaryColor = "#f39c12", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
            context.TaskPriorities.Add(new TaskPriority { Id = Guid.NewGuid(), Name = "High", PrimaryColor = "#e74c3c", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
        }

        if (!await context.MeetingStatuses.AnyAsync(cancellationToken))
        {
            context.MeetingStatuses.Add(new MeetingStatus { Id = Guid.NewGuid(), Name = "Pending", PrimaryColor = "#f39c12", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
            context.MeetingStatuses.Add(new MeetingStatus { Id = Guid.NewGuid(), Name = "Scheduled", PrimaryColor = "#3498DB", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
            context.MeetingStatuses.Add(new MeetingStatus { Id = Guid.NewGuid(), Name = "Completed", PrimaryColor = "#2ecc71", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
            context.MeetingStatuses.Add(new MeetingStatus { Id = Guid.NewGuid(), Name = "Expired", PrimaryColor = "#95a5a6", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
            context.MeetingStatuses.Add(new MeetingStatus { Id = Guid.NewGuid(), Name = "Cancelled", PrimaryColor = "#e74c3c", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
        }
        else
        {
            // Ensure Pending and Expired exist (for DBs seeded before these were added)
            var names = await context.MeetingStatuses.Select(s => s.Name).ToListAsync(cancellationToken);
            if (!names.Contains("Pending"))
                context.MeetingStatuses.Add(new MeetingStatus { Id = Guid.NewGuid(), Name = "Pending", PrimaryColor = "#f39c12", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
            if (!names.Contains("Expired"))
                context.MeetingStatuses.Add(new MeetingStatus { Id = Guid.NewGuid(), Name = "Expired", PrimaryColor = "#95a5a6", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
        }

        if (!await context.MeetingResults.AnyAsync(cancellationToken))
        {
            context.MeetingResults.Add(new MeetingResult { Id = Guid.NewGuid(), Name = "Win", PrimaryColor = "#2ecc71", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
            context.MeetingResults.Add(new MeetingResult { Id = Guid.NewGuid(), Name = "Lost", PrimaryColor = "#e74c3c", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
        }

        if (!await context.MeetingPriorities.AnyAsync(cancellationToken))
        {
            context.MeetingPriorities.Add(new MeetingPriority { Id = Guid.NewGuid(), Name = "Low", PrimaryColor = "#95a5a6", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
            context.MeetingPriorities.Add(new MeetingPriority { Id = Guid.NewGuid(), Name = "Medium", PrimaryColor = "#f39c12", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
            context.MeetingPriorities.Add(new MeetingPriority { Id = Guid.NewGuid(), Name = "High", PrimaryColor = "#e74c3c", SecondaryColor = "#fff", IsActive = true, CreatedAt = now });
        }

        if (!await context.Uoms.AnyAsync(cancellationToken))
        {
            foreach (var name in new[] { "kg", "m", "L", "pcs", "container" })
                context.Uoms.Add(new Uom { Id = Guid.NewGuid(), Name = name, IsActive = true, CreatedAt = now });
        }

        if (!await context.PricingTypes.AnyAsync(cancellationToken))
        {
            foreach (var name in new[] { "Standard", "Volume", "Contract", "Spot" })
                context.PricingTypes.Add(new PricingType { Id = Guid.NewGuid(), Name = name, IsActive = true, CreatedAt = now });
        }

        if (!await context.TransportTypes.AnyAsync(cancellationToken))
        {
            context.TransportTypes.Add(new TransportType { Id = Guid.NewGuid(), Name = "Air", IsAir = true, IsSea = false, IsRoad = false, IsRail = false, IsActive = true, CreatedAt = now });
            context.TransportTypes.Add(new TransportType { Id = Guid.NewGuid(), Name = "Sea", IsAir = false, IsSea = true, IsRoad = false, IsRail = false, IsActive = true, CreatedAt = now });
            context.TransportTypes.Add(new TransportType { Id = Guid.NewGuid(), Name = "Road", IsAir = false, IsSea = false, IsRoad = true, IsRail = false, IsActive = true, CreatedAt = now });
            context.TransportTypes.Add(new TransportType { Id = Guid.NewGuid(), Name = "Rail", IsAir = false, IsSea = false, IsRoad = false, IsRail = true, IsActive = true, CreatedAt = now });
        }

        if (!await context.LoadingMethods.AnyAsync(cancellationToken))
        {
            var methods = new[] { new { Name = "Full Load", EN = "Full Load", LT = "Pilna apkrova", RU = "Полная загрузка" }, new { Name = "LTL", EN = "LTL", LT = "Dalinai", RU = "Частичная" }, new { Name = "FCL", EN = "FCL", LT = "FCL", RU = "FCL" } };
            foreach (var m in methods)
            {
                var id = Guid.NewGuid();
                context.LoadingMethods.Add(new LoadingMethod { Id = id, Name = m.Name, IsActive = true, CreatedAt = now });
                context.LoadingMethodTranslations.Add(new LoadingMethodTranslation { Id = Guid.NewGuid(), LoadingMethodId = id, LanguageCode = "EN", Name = m.EN });
                context.LoadingMethodTranslations.Add(new LoadingMethodTranslation { Id = Guid.NewGuid(), LoadingMethodId = id, LanguageCode = "LT", Name = m.LT });
                context.LoadingMethodTranslations.Add(new LoadingMethodTranslation { Id = Guid.NewGuid(), LoadingMethodId = id, LanguageCode = "RU", Name = m.RU });
            }
        }

        if (!await context.WorkerPosts.AnyAsync(cancellationToken))
        {
            var posts = new[] { new { Name = "Director", EN = "Director", LT = "Direktorius", RU = "Директор" }, new { Name = "Driver", EN = "Driver", LT = "Vairuotojas", RU = "Водитель" }, new { Name = "Loader", EN = "Loader", LT = "Kraunantysis", RU = "Грузчик" }, new { Name = "Manager", EN = "Manager", LT = "Vadybininkas", RU = "Менеджер" } };
            foreach (var p in posts)
            {
                var id = Guid.NewGuid();
                context.WorkerPosts.Add(new WorkerPost { Id = id, Name = p.Name, IsActive = true, CreatedAt = now });
                context.WorkerPostTranslations.Add(new WorkerPostTranslation { Id = Guid.NewGuid(), WorkerPostId = id, LanguageCode = "EN", Name = p.EN });
                context.WorkerPostTranslations.Add(new WorkerPostTranslation { Id = Guid.NewGuid(), WorkerPostId = id, LanguageCode = "LT", Name = p.LT });
                context.WorkerPostTranslations.Add(new WorkerPostTranslation { Id = Guid.NewGuid(), WorkerPostId = id, LanguageCode = "RU", Name = p.RU });
            }
        }
        else
        {
            var hasDirector = await context.WorkerPosts.AnyAsync(w => w.Name == "Director", cancellationToken);
            if (!hasDirector)
            {
                var id = Guid.NewGuid();
                context.WorkerPosts.Add(new WorkerPost { Id = id, Name = "Director", IsActive = true, CreatedAt = now });
                context.WorkerPostTranslations.Add(new WorkerPostTranslation { Id = Guid.NewGuid(), WorkerPostId = id, LanguageCode = "EN", Name = "Director" });
                context.WorkerPostTranslations.Add(new WorkerPostTranslation { Id = Guid.NewGuid(), WorkerPostId = id, LanguageCode = "LT", Name = "Direktorius" });
                context.WorkerPostTranslations.Add(new WorkerPostTranslation { Id = Guid.NewGuid(), WorkerPostId = id, LanguageCode = "RU", Name = "Директор" });
            }
        }

        if (!await context.DrivingLicenceCategories.AnyAsync(cancellationToken))
        {
            foreach (var (name, code) in new[] { ("B", "B"), ("C", "C"), ("CE", "CE"), ("C1", "C1"), ("D", "D") })
                context.DrivingLicenceCategories.Add(new DrivingLicenceCategory { Id = Guid.NewGuid(), Name = name, Code = code, IsActive = true, CreatedAt = now });
        }

        if (!await context.RequestPurposes.AnyAsync(cancellationToken))
        {
            foreach (var name in new[] { "Quote", "Order", "Inquiry", "Complaint" })
                context.RequestPurposes.Add(new RequestPurpose { Id = Guid.NewGuid(), Name = name, IsActive = true, CreatedAt = now });
        }

        if (!await context.DeferredPaymentConditions.AnyAsync(cancellationToken))
        {
            context.DeferredPaymentConditions.Add(new DeferredPaymentCondition { Id = Guid.NewGuid(), Name = "Net 30", FullText = "Payment within 30 days", IsActive = true, CreatedAt = now });
            context.DeferredPaymentConditions.Add(new DeferredPaymentCondition { Id = Guid.NewGuid(), Name = "Cash", FullText = "Payment on delivery", IsActive = true, CreatedAt = now });
            context.DeferredPaymentConditions.Add(new DeferredPaymentCondition { Id = Guid.NewGuid(), Name = "Prepaid", FullText = "Prepaid", IsActive = true, CreatedAt = now });
        }

        if (!await context.ClientSegments.AnyAsync(cancellationToken))
        {
            context.ClientSegments.Add(new ClientSegment { Id = Guid.NewGuid(), SegmentName = "VIP", SegmentPriority = 1, IsActive = true, IsDefault = false, PrimaryColor = "#FFD700", SecondaryColor = "#000000", CreatedAt = now });
            context.ClientSegments.Add(new ClientSegment { Id = Guid.NewGuid(), SegmentName = "Standard", SegmentPriority = 2, IsActive = true, IsDefault = true, PrimaryColor = "#3498DB", SecondaryColor = "#FFFFFF", CreatedAt = now });
            context.ClientSegments.Add(new ClientSegment { Id = Guid.NewGuid(), SegmentName = "Premium", SegmentPriority = 3, IsActive = true, IsDefault = false, PrimaryColor = "#9B59B6", SecondaryColor = "#FFFFFF", CreatedAt = now });
        }

        if (!await context.RequestSources.AnyAsync(cancellationToken))
        {
            foreach (var name in new[] { "Website", "Phone", "Email", "Referral", "Social" })
                context.RequestSources.Add(new RequestSource { Id = Guid.NewGuid(), Name = name, IsActive = true, CreatedAt = now });
        }

        if (!await context.QuoteSources.AnyAsync(cancellationToken))
        {
            var stages = new (string Name, int Order)[]
            {
                ("Created", 10),
                ("Planning", 20),
                ("Sent to Customer", 30),
                ("Accepted", 40),
                ("Declined", 50),
                ("Expired", 60),
                ("Cancelled", 70)
            };
            foreach (var (name, order) in stages)
                context.QuoteSources.Add(new QuoteSource { Id = Guid.NewGuid(), Name = name, DisplayOrder = order, IsActive = true, CreatedAt = now });
        }

        if (!await context.Templates.AnyAsync(cancellationToken))
        {
            var paidId = Guid.NewGuid();
            var freeId = Guid.NewGuid();
            context.Templates.Add(new Template { Id = paidId, Name = "Paid", IsActive = true, CreatedAt = now });
            context.TemplateTranslations.Add(new TemplateTranslation { Id = Guid.NewGuid(), TemplateId = paidId, LanguageCode = "az", Name = "Ödənişli" });
            context.TemplateTranslations.Add(new TemplateTranslation { Id = Guid.NewGuid(), TemplateId = paidId, LanguageCode = "en", Name = "Paid" });
            context.TemplateTranslations.Add(new TemplateTranslation { Id = Guid.NewGuid(), TemplateId = paidId, LanguageCode = "ru", Name = "Платный" });
            context.Templates.Add(new Template { Id = freeId, Name = "Free", IsActive = true, CreatedAt = now });
            context.TemplateTranslations.Add(new TemplateTranslation { Id = Guid.NewGuid(), TemplateId = freeId, LanguageCode = "az", Name = "Pulsuz" });
            context.TemplateTranslations.Add(new TemplateTranslation { Id = Guid.NewGuid(), TemplateId = freeId, LanguageCode = "en", Name = "Free" });
            context.TemplateTranslations.Add(new TemplateTranslation { Id = Guid.NewGuid(), TemplateId = freeId, LanguageCode = "ru", Name = "Бесплатный" });
        }

        if (!await context.AddressTypes.AnyAsync(cancellationToken))
        {
            context.AddressTypes.Add(new AddressType { Id = Guid.NewGuid(), Code = "MAIN", Name = "Main Address", Description = "Primary business address", IsActive = true, CreatedAt = now });
            context.AddressTypes.Add(new AddressType { Id = Guid.NewGuid(), Code = "WAREHOUSE", Name = "Warehouse", Description = "Warehouse or storage address", IsActive = true, CreatedAt = now });
            context.AddressTypes.Add(new AddressType { Id = Guid.NewGuid(), Code = "PICKUP", Name = "Pickup", Description = "Pickup location", IsActive = true, CreatedAt = now });
            context.AddressTypes.Add(new AddressType { Id = Guid.NewGuid(), Code = "DELIVERY", Name = "Delivery", Description = "Delivery destination", IsActive = true, CreatedAt = now });
            context.AddressTypes.Add(new AddressType { Id = Guid.NewGuid(), Code = "BILLING", Name = "Billing", Description = "Billing address", IsActive = true, CreatedAt = now });
        }

        if (!await context.Countries.AnyAsync(cancellationToken))
        {
            var azId = Guid.NewGuid();
            var geId = new Guid("11111111-2222-3333-4444-555555555501");
            var trId = Guid.NewGuid();
            var ltId = Guid.NewGuid();
            var deId = Guid.NewGuid();
            context.Countries.Add(new Country { Id = azId, IsoCode = "AZ", Name = "Azerbaijan", IsStateRequired = false, HasCities = true, Status = EntityStatus.Active, CreatedAt = now });
            context.Countries.Add(new Country { Id = geId, IsoCode = "GE", Name = "Georgia", IsStateRequired = false, HasCities = true, Status = EntityStatus.Active, CreatedAt = now });
            context.Countries.Add(new Country { Id = trId, IsoCode = "TR", Name = "Turkey", IsStateRequired = true, HasCities = true, Status = EntityStatus.Active, CreatedAt = now });
            context.Countries.Add(new Country { Id = ltId, IsoCode = "LT", Name = "Lithuania", IsStateRequired = false, HasCities = true, Status = EntityStatus.Active, CreatedAt = now });
            context.Countries.Add(new Country { Id = deId, IsoCode = "DE", Name = "Germany", IsStateRequired = true, HasCities = true, Status = EntityStatus.Active, CreatedAt = now });

            var azStateId = Guid.NewGuid();
            var geStateId = Guid.NewGuid();
            var trIstanbulId = Guid.NewGuid();
            var trAnkaraId = Guid.NewGuid();
            var trIzmirId = Guid.NewGuid();
            var ltVilniusId = Guid.NewGuid();
            var ltKaunasId = Guid.NewGuid();
            var ltKlaipedaId = Guid.NewGuid();
            var deBerlinId = Guid.NewGuid();
            var deBavariaId = Guid.NewGuid();
            context.States.Add(new State { Id = azStateId, Code = "BAK", Name = "Baku", CountryId = azId, Status = EntityStatus.Active, CreatedAt = now });
            context.States.Add(new State { Id = geStateId, Code = "TB", Name = "Tbilisi", CountryId = geId, Status = EntityStatus.Active, CreatedAt = now });
            context.States.Add(new State { Id = trIstanbulId, Code = "34", Name = "Istanbul", CountryId = trId, Status = EntityStatus.Active, CreatedAt = now });
            context.States.Add(new State { Id = trAnkaraId, Code = "06", Name = "Ankara", CountryId = trId, Status = EntityStatus.Active, CreatedAt = now });
            context.States.Add(new State { Id = trIzmirId, Code = "35", Name = "Izmir", CountryId = trId, Status = EntityStatus.Active, CreatedAt = now });
            context.States.Add(new State { Id = ltVilniusId, Code = "VL", Name = "Vilnius County", CountryId = ltId, Status = EntityStatus.Active, CreatedAt = now });
            context.States.Add(new State { Id = ltKaunasId, Code = "KU", Name = "Kaunas County", CountryId = ltId, Status = EntityStatus.Active, CreatedAt = now });
            context.States.Add(new State { Id = ltKlaipedaId, Code = "KL", Name = "Klaipeda County", CountryId = ltId, Status = EntityStatus.Active, CreatedAt = now });
            var deHamburgId = Guid.NewGuid();
            context.States.Add(new State { Id = deBerlinId, Code = "BE", Name = "Berlin", CountryId = deId, Status = EntityStatus.Active, CreatedAt = now });
            context.States.Add(new State { Id = deBavariaId, Code = "BY", Name = "Bavaria", CountryId = deId, Status = EntityStatus.Active, CreatedAt = now });
            context.States.Add(new State { Id = deHamburgId, Code = "HH", Name = "Hamburg", CountryId = deId, Status = EntityStatus.Active, CreatedAt = now });

            context.Cities.Add(new City { Id = Guid.NewGuid(), Code = "BAK", Name = "Baku", StateId = azStateId, ZipCode = "AZ1000", Status = EntityStatus.Active, CreatedAt = now });
            context.Cities.Add(new City { Id = Guid.NewGuid(), Code = "GAN", Name = "Ganja", StateId = azStateId, ZipCode = "AZ2000", Status = EntityStatus.Active, CreatedAt = now });
            context.Cities.Add(new City { Id = Guid.NewGuid(), Code = "SUM", Name = "Sumgayit", StateId = azStateId, ZipCode = "AZ5000", Status = EntityStatus.Active, CreatedAt = now });
            context.Cities.Add(new City { Id = Guid.NewGuid(), Code = "TBS", Name = "Tbilisi", StateId = geStateId, ZipCode = "0100", Status = EntityStatus.Active, CreatedAt = now });
            context.Cities.Add(new City { Id = Guid.NewGuid(), Code = "BAT", Name = "Batumi", StateId = geStateId, ZipCode = "6010", Status = EntityStatus.Active, CreatedAt = now });
            context.Cities.Add(new City { Id = Guid.NewGuid(), Code = "KUT", Name = "Kutaisi", StateId = geStateId, ZipCode = "4600", Status = EntityStatus.Active, CreatedAt = now });
            context.Cities.Add(new City { Id = Guid.NewGuid(), Code = "IST", Name = "Istanbul", StateId = trIstanbulId, ZipCode = "34000", Status = EntityStatus.Active, CreatedAt = now });
            context.Cities.Add(new City { Id = Guid.NewGuid(), Code = "ANK", Name = "Ankara", StateId = trAnkaraId, ZipCode = "06000", Status = EntityStatus.Active, CreatedAt = now });
            context.Cities.Add(new City { Id = Guid.NewGuid(), Code = "IZM", Name = "Izmir", StateId = trIzmirId, ZipCode = "35000", Status = EntityStatus.Active, CreatedAt = now });
            context.Cities.Add(new City { Id = Guid.NewGuid(), Code = "VNO", Name = "Vilnius", StateId = ltVilniusId, ZipCode = "01001", Status = EntityStatus.Active, CreatedAt = now });
            context.Cities.Add(new City { Id = Guid.NewGuid(), Code = "KUN", Name = "Kaunas", StateId = ltKaunasId, ZipCode = "44001", Status = EntityStatus.Active, CreatedAt = now });
            context.Cities.Add(new City { Id = Guid.NewGuid(), Code = "KLJ", Name = "Klaipeda", StateId = ltKlaipedaId, ZipCode = "91001", Status = EntityStatus.Active, CreatedAt = now });
            context.Cities.Add(new City { Id = Guid.NewGuid(), Code = "BER", Name = "Berlin", StateId = deBerlinId, ZipCode = "10115", Status = EntityStatus.Active, CreatedAt = now });
            context.Cities.Add(new City { Id = Guid.NewGuid(), Code = "MUC", Name = "Munich", StateId = deBavariaId, ZipCode = "80331", Status = EntityStatus.Active, CreatedAt = now });
            context.Cities.Add(new City { Id = Guid.NewGuid(), Code = "HAM", Name = "Hamburg", StateId = deHamburgId, ZipCode = "20095", Status = EntityStatus.Active, CreatedAt = now });
        }

        if (!await context.Banks.AnyAsync(cancellationToken))
        {
            context.Banks.Add(new Bank { Id = Guid.NewGuid(), Name = "Sample Bank", Code = "SB001", CreatedAt = now });
        }

        if (!await context.Companies.AnyAsync(cancellationToken))
        {
            var azCountry = await context.Countries.FirstOrDefaultAsync(c => c.IsoCode == "AZ", cancellationToken);
            var geCountry = await context.Countries.FirstOrDefaultAsync(c => c.IsoCode == "GE", cancellationToken);
            var bakuCity = await context.Cities.FirstOrDefaultAsync(c => c.Code == "BAK", cancellationToken);
            var tbilisiCity = await context.Cities.FirstOrDefaultAsync(c => c.Code == "TBS", cancellationToken);
            var sampleBank = await context.Banks.FirstOrDefaultAsync(cancellationToken);
            if (sampleBank == null)
            {
                sampleBank = new Bank { Id = Guid.NewGuid(), Name = "Sample Bank", Code = "SB001", CreatedAt = now };
                context.Banks.Add(sampleBank);
            }

            var c1 = new Company
            {
                Id = Guid.NewGuid(),
                Name = "AZ SHIPPING LLC",
                NameFull = "AZ SHIPPING AZERBAIJAN",
                DirectorsFullName = "Tabriz Aghahuseynov",
                Post = "Director",
                VatRate = "Without VAT",
                CompanyPrefix = "AZ",
                CompanyCodeType = "INN",
                CompanyCode = "1005381151",
                VatCode = "1005381151",
                CountryId = azCountry?.Id,
                CityId = bakuCity?.Id,
                Address = "Hamza Babashov str., Building 5, apt 12, Baku",
                PostCode = "AZ1114",
                Telephone = "+994502092558",
                Email = "info@azshipping.az",
                Website = "azshipping.az",
                IsMainCompany = true,
                CorrespondentAddress = "Hamza Babashov str., Building 5, apt 12, Baku",
                CorrespondentPostCode = "AZ1114",
                IsActive = true,
                CreatedAt = now
            };
            context.Companies.Add(c1);
            context.CompanyBankAccounts.Add(new CompanyBankAccount { Id = Guid.NewGuid(), CompanyId = c1.Id, BankId = sampleBank.Id, CurrencyCode = "AZN", AccountNumberIban = "AZ21NABZ00000000137010001944" });

            var c2 = new Company
            {
                Id = Guid.NewGuid(),
                Name = "AZ SHIPPING UZBEKISTAN",
                NameFull = "AZSHIPPING LLC",
                CompanyCodeType = "INN",
                CompanyCode = "311726403",
                CountryId = null,
                Address = "Babur str. 77 Z. Yakkasaray district, Tashkent, Uzbekistan",
                IsActive = true,
                CreatedAt = now
            };
            context.Companies.Add(c2);

            var c3 = new Company
            {
                Id = Guid.NewGuid(),
                Name = "AZSHIPPING GEORGIA",
                NameFull = "AZSHIPPING LLC",
                CompanyCodeType = "INN",
                CompanyCode = "405482277",
                VatCode = "405482277",
                CountryId = geCountry?.Id,
                CityId = tbilisiCity?.Id,
                Address = "Vake District, Besarion Zhgenti St. N 49, floor 1, apartment N20, Tbilisi, Georgia",
                CorrespondentAddress = "Vake District, Besarion Zhgenti St. N 49, floor 1, apartment N20, Tbilisi, Georgia",
                IsActive = true,
                CreatedAt = now
            };
            context.Companies.Add(c3);
            context.CompanyBankAccounts.Add(new CompanyBankAccount { Id = Guid.NewGuid(), CompanyId = c3.Id, BankId = sampleBank.Id, CurrencyCode = "GEL", AccountNumberIban = "GE29NB0000000101904917" });

            context.Companies.Add(new Company
            {
                Id = Guid.NewGuid(),
                Name = "AzShipping Ltd",
                NameFull = "AzShipping Logistics Ltd",
                DirectorsFullName = "John Smith",
                Post = "Director",
                VatRate = "Without VAT",
                CompanyCode = "123456789",
                Address = "123 Main Street",
                PostCode = "LV-1010",
                Telephone = "+371 12345678",
                Email = "office@azshipping.com",
                IsActive = true,
                CreatedAt = now
            });
        }

        if (!await context.SalesFunnelStatuses.AnyAsync(cancellationToken))
        {
            var statuses = new[] { ("New", 1), ("Contacted", 2), ("Qualified", 3), ("Proposal", 4), ("Negotiation", 5), ("Won", 6), ("Lost", 7) };
            foreach (var (name, pos) in statuses)
                context.SalesFunnelStatuses.Add(new SalesFunnelStatus { Id = Guid.NewGuid(), Name = name, StatusPosition = pos, NumberOfDays = 0, SendToEmail = false, SendNotification = false, IsActive = true, CreatedAt = now });
        }

        if (!await context.WayOfNegotiations.AnyAsync(cancellationToken))
        {
            var ways = new[]
            {
                new { Name = "Call", EN = "Call", LT = "Skambutis", RU = "Звонок" },
                new { Name = "Cold call", EN = "Cold call", LT = "Šaltas skambutis", RU = "Холодный звонок" },
                new { Name = "Commercial offer", EN = "Commercial offer", LT = "Komercinis pasiūlymas", RU = "Коммерческое предложение" },
                new { Name = "Meeting", EN = "Meeting", LT = "Susitikimas", RU = "Встреча" },
                new { Name = "Virtual meeting", EN = "Virtual meeting", LT = "Virtualus susitikimas", RU = "Виртуальная встреча" },
            };
            foreach (var w in ways)
            {
                var id = Guid.NewGuid();
                context.WayOfNegotiations.Add(new WayOfNegotiation { Id = id, Name = w.Name, IsActive = true, CreatedAt = now });
                context.WayOfNegotiationTranslations.Add(new WayOfNegotiationTranslation { Id = Guid.NewGuid(), WayOfNegotiationId = id, LanguageCode = "EN", Name = w.EN });
                context.WayOfNegotiationTranslations.Add(new WayOfNegotiationTranslation { Id = Guid.NewGuid(), WayOfNegotiationId = id, LanguageCode = "LT", Name = w.LT });
                context.WayOfNegotiationTranslations.Add(new WayOfNegotiationTranslation { Id = Guid.NewGuid(), WayOfNegotiationId = id, LanguageCode = "RU", Name = w.RU });
            }
        }

        if (!await context.ResultTypes.AnyAsync(cancellationToken))
        {
            var posId = Guid.NewGuid();
            var negId = Guid.NewGuid();
            context.ResultTypes.Add(new ResultType { Id = posId, Name = "Positive", Code = "POSITIVE", IsActive = true, CreatedAt = now });
            context.ResultTypes.Add(new ResultType { Id = negId, Name = "Negative", Code = "NEGATIVE", IsActive = true, CreatedAt = now });

            if (!await context.FunnelResults.AnyAsync(cancellationToken))
            {
                context.FunnelResults.Add(new FunnelResult { Id = Guid.NewGuid(), Name = "Success", ResultTypeId = posId, ToNextStep = true, IsActive = true, CreatedAt = now });
                context.FunnelResults.Add(new FunnelResult { Id = Guid.NewGuid(), Name = "Failure", ResultTypeId = negId, ToNextStep = false, IsActive = true, CreatedAt = now });
            }
        }

        if (!await context.ClientSources.AnyAsync(cancellationToken))
        {
            foreach (var name in new[] { "Website", "Phone", "Email", "Referral", "Social Media", "Exhibition", "Cold Call" })
                context.ClientSources.Add(new ClientSource { Id = Guid.NewGuid(), Name = name, IsActive = true, CreatedAt = now });
        }

        if (!await context.GlobalZones.AnyAsync(cancellationToken))
        {
            var zones = new[]
            {
                (Code: "EU", Name: "Europe", LocalName: "Европа"),
                (Code: "CAU", Name: "Caucasus", LocalName: "Кавказ"),
                (Code: "MEA", Name: "Middle East", LocalName: "Ближний Восток"),
                (Code: "CIS", Name: "CIS", LocalName: "СНГ"),
                (Code: "ASA", Name: "Asia", LocalName: "Азия"),
            };
            foreach (var (code, name, local) in zones)
                context.GlobalZones.Add(new GlobalZone { Id = Guid.NewGuid(), Code = code, Name = name, LocalName = local, Status = EntityStatus.Active, CreatedAt = now });
        }

        if (!await context.SystemLogs.AnyAsync(cancellationToken))
        {
            var logTime = DateTime.UtcNow.AddHours(-2);
            context.SystemLogs.Add(new SystemLog { CreatedAt = logTime, Name = "google api", Level = "Error", Body = "Empty app setting google_place.enabled" });
            context.SystemLogs.Add(new SystemLog { CreatedAt = logTime.AddMinutes(5), Name = "email", Level = "Error", Body = "Failed to authenticate on SMTP server with username 'user@azshipping.az'. Connection refused." });
            context.SystemLogs.Add(new SystemLog { CreatedAt = logTime.AddMinutes(10), Name = "email", Level = "Error", Body = "SMTP authentication failed. Check credentials and try again." });
            context.SystemLogs.Add(new SystemLog { CreatedAt = logTime.AddMinutes(15), Name = "Settings", Level = "Information", Body = "Application started successfully." });
            context.SystemLogs.Add(new SystemLog { CreatedAt = logTime.AddMinutes(20), Name = "database", Level = "Warning", Body = "Connection pool nearing capacity." });
        }

        if (!await context.EmployeeGroups.AnyAsync(cancellationToken))
        {
            var companyRows = await context.Companies.AsNoTracking().OrderBy(c => c.Name).ToListAsync(cancellationToken);
            foreach (var co in companyRows)
            {
                context.EmployeeGroups.Add(new EmployeeGroup
                {
                    Id = Guid.NewGuid(),
                    Name = co.Name + " — Operations",
                    CompanyId = co.Id,
                    PermissionsJson = "{}",
                    CreatedAtUtc = now
                });
                context.EmployeeGroups.Add(new EmployeeGroup
                {
                    Id = Guid.NewGuid(),
                    Name = co.Name + " — Sales",
                    CompanyId = co.Id,
                    PermissionsJson = "{}",
                    CreatedAtUtc = now
                });
            }

            context.EmployeeGroups.Add(new EmployeeGroup
            {
                Id = Guid.NewGuid(),
                Name = "Cross-company (no company filter)",
                CompanyId = null,
                PermissionsJson = "{}",
                CreatedAtUtc = now
            });
        }

        await EnsureErpTestEmployeeGroupAsync(context, now, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>Stable GUID so Identity seed can assign the same group. Multi-module read-only smoke test.</summary>
    private static async Task EnsureErpTestEmployeeGroupAsync(SettingsDbContext context, DateTime now, CancellationToken cancellationToken)
    {
        var id = Guid.Parse("a0000001-0001-4001-8001-000000000001");
        if (await context.EmployeeGroups.AnyAsync(g => g.Id == id, cancellationToken))
            return;

        const string permissionsJson = """
            {
              "Request": {
                "viewRequest": true,
                "commentsView": true,
                "priceProposalsView": true
              },
              "Orders": {
                "view": true
              },
              "Clients": {
                "viewClients": true
              },
              "Carriers": {
                "viewCarriers": true
              },
              "Reports": {
                "individualReports": true,
                "purchaseFunnel": true
              },
              "Task": {
                "viewTasks": true
              },
              "Documents": {
                "issuedInvoices": { "view": true, "editing": true, "delete": true, "editingPaidInvoices": true },
                "receivedInvoices": { "view": true, "editing": true, "delete": true, "editingPaidInvoices": true },
                "act": { "view": true, "editing": true, "delete": true },
                "incomingPayments": { "view": true, "editing": true, "delete": true },
                "effectedIncomingPayments": { "view": true, "editing": true, "delete": true },
                "otherDocuments": { "view": true, "editing": true, "delete": true },
                "documentsForRequest": { "view": true, "editing": true, "delete": true }
              },
              "Warehouse": {
                "stockView": true,
                "warehouseEditing": true,
                "useWarehouseMobileApplications": true,
                "documentsActivation": true,
                "requestForDeliveryFromCustomers": { "view": true, "editing": true, "delete": true },
                "act": { "view": true, "editing": true, "delete": true },
                "requestForDeliveryToCarrier": { "view": true, "editing": true, "delete": true },
                "waybill": { "view": true, "editing": true, "delete": true, "roleConfirm": true }
              },
              "Calculation": {
                "accessToSalaryCalculation": "all",
                "viewSalaryCalculation": true,
                "editingSalaryCalculation": true
              },
              "Settings": {
                "system": { "view": true, "editing": true },
                "organization": { "view": true, "editing": true },
                "classifiers": { "view": true, "editing": true },
                "templates": { "view": true, "editing": true },
                "dataTransferViaApi": { "roleActivate": true }
              },
              "ImportExport": {
                "request": { "exportToExcel": true, "importFromExcel": true },
                "orders": {
                  "exportToExcel": true,
                  "exportFlightsToExcel": true,
                  "exportToXml": true,
                  "importFromExcel": true,
                  "exportPayrollCalculationToExcel": true
                },
                "cargos": { "exportToExcel": true, "exportCargoStatusesToExcel": true, "importFromExcel": true },
                "documents": {
                  "exportIssuedInvoicesToExcel": true,
                  "exportIssuedInvoicesToXml": true,
                  "exportReceivedInvoicesToExcel": true,
                  "exportReceivedInvoicesToXml": true,
                  "exportActsToExcel": true,
                  "exportIncomingPaymentsToExcel": true
                },
                "clients": { "exportToExcel": true, "exportToXml": true, "importFromExcel": true },
                "carriers": {
                  "exportToExcel": true,
                  "exportToXml": true,
                  "importFromExcel": true,
                  "importTerminalsToExcel": true
                },
                "transport": { "importFromExcel": true },
                "drivers": { "importFromExcel": true },
                "reports": { "exportToExcel": true }
              }
            }
            """;

        context.EmployeeGroups.Add(new EmployeeGroup
        {
            Id = id,
            Name = "Local dev — ERP test (multi-module incl. Settings + ImportExport)",
            CompanyId = null,
            PermissionsJson = permissionsJson,
            CreatedAtUtc = now
        });
    }
}
