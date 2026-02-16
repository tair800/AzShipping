using AzShipping.Domain.Entities;

namespace AzShipping.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        // Seed Client Segments
        if (!context.ClientSegments.Any())
        {
            var segments = new List<ClientSegment>
        {
            new ClientSegment
            {
                Id = Guid.NewGuid(),
                SegmentName = "VIP",
                SegmentPriority = 1,
                IsActive = true,
                IsDefault = false,
                PrimaryColor = "#FFFFFF",
                SecondaryColor = "#000000",
                CreatedAt = DateTime.UtcNow
            },
            new ClientSegment
            {
                Id = Guid.NewGuid(),
                SegmentName = "Super Client",
                SegmentPriority = 2,
                IsActive = true,
                IsDefault = false,
                PrimaryColor = "#90EE90",
                SecondaryColor = "#000000",
                CreatedAt = DateTime.UtcNow
            },
            new ClientSegment
            {
                Id = Guid.NewGuid(),
                SegmentName = "General",
                SegmentPriority = 3,
                IsActive = true,
                IsDefault = true,
                PrimaryColor = "#FFFFFF",
                SecondaryColor = "#000000",
                CreatedAt = DateTime.UtcNow
            }
        };

            context.ClientSegments.AddRange(segments);
            context.SaveChanges();
        }

        // Seed Request Sources
        if (!context.RequestSources.Any())
        {
            var requestSources = new List<RequestSource>
        {
            new RequestSource
            {
                Id = Guid.NewGuid(),
                Name = "Email",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new RequestSource
            {
                Id = Guid.NewGuid(),
                Name = "Client zone",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new RequestSource
            {
                Id = Guid.NewGuid(),
                Name = "Telephone",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new RequestSource
            {
                Id = Guid.NewGuid(),
                Name = "Skype",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new RequestSource
            {
                Id = Guid.NewGuid(),
                Name = "Реклама",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new RequestSource
            {
                Id = Guid.NewGuid(),
                Name = "Сайт",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new RequestSource
            {
                Id = Guid.NewGuid(),
                Name = "Соц. сети",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        };

            context.RequestSources.AddRange(requestSources);
            context.SaveChanges();
        }

        // Seed Packagings
        if (!context.Packagings.Any())
        {
            var packagings = new List<Packaging>
        {
            CreatePackagingWithTranslations("Box", new Dictionary<string, string>
            {
                { "EN", "Box" },
                { "LT", "Dėžė" },
                { "RU", "Коробка" }
            }),
            CreatePackagingWithTranslations("Carton Box", new Dictionary<string, string>
            {
                { "EN", "Carton Box" },
                { "LT", "Kartoninė dėžė" },
                { "RU", "Картонная коробка" }
            }),
            CreatePackagingWithTranslations("EUR-паллет - 800х1200х145мм.", new Dictionary<string, string>
            {
                { "EN", "EUR-pallet - 800x1200x145mm" },
                { "LT", "EUR-paletė - 800x1200x145mm" },
                { "RU", "EUR-паллет - 800х1200х145мм" }
            }),
            CreatePackagingWithTranslations("FIN-паллет - 1000х1200х145мм.", new Dictionary<string, string>
            {
                { "EN", "FIN-pallet - 1000x1200x145mm" },
                { "LT", "FIN-paletė - 1000x1200x145mm" },
                { "RU", "FIN-паллет - 1000х1200х145мм" }
            }),
            CreatePackagingWithTranslations("Wooden box", new Dictionary<string, string>
            {
                { "EN", "Wooden box" },
                { "LT", "Medinė dėžė" },
                { "RU", "Деревянный ящик" }
            })
        };

            context.Packagings.AddRange(packagings);
            context.SaveChanges();
        }

        // Seed Users (fake users for now)
        if (!context.Users.Any())
        {
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), FirstName = "John", LastName = "Smith", Email = "john.smith@azshipping.com", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = Guid.NewGuid(), FirstName = "Sarah", LastName = "Johnson", Email = "sarah.johnson@azshipping.com", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = Guid.NewGuid(), FirstName = "Michael", LastName = "Brown", Email = "michael.brown@azshipping.com", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = Guid.NewGuid(), FirstName = "Emily", LastName = "Davis", Email = "emily.davis@azshipping.com", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = Guid.NewGuid(), FirstName = "David", LastName = "Wilson", Email = "david.wilson@azshipping.com", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = Guid.NewGuid(), FirstName = "Lisa", LastName = "Anderson", Email = "lisa.anderson@azshipping.com", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = Guid.NewGuid(), FirstName = "Robert", LastName = "Taylor", Email = "robert.taylor@azshipping.com", IsActive = true, CreatedAt = DateTime.UtcNow },
                new User { Id = Guid.NewGuid(), FirstName = "Maria", LastName = "Martinez", Email = "maria.martinez@azshipping.com", IsActive = true, CreatedAt = DateTime.UtcNow }
            };
            context.Users.AddRange(users);
            context.SaveChanges();
        }

        // Seed Sales Funnel Statuses
        if (!context.SalesFunnelStatuses.Any())
        {
            var statuses = new List<SalesFunnelStatus>
            {
                CreateSalesFunnelStatus("Cold call", 1, null, 3, false, true, true),
                CreateSalesFunnelStatus("Sent by CP after the call.", 2, null, 3, false, true, true),
                CreateSalesFunnelStatus("Meeting | Negotiations", 3, null, 7, false, true, true),
                CreateSalesFunnelStatus("An agreement has been concluded", 4, null, 5, false, false, true),
                CreateSalesFunnelStatus("Regular work", 5, null, 30, false, true, true),
                CreateSalesFunnelStatus("Regular customer", 6, null, 300, false, true, true)
            };
            context.SalesFunnelStatuses.AddRange(statuses);
            context.SaveChanges();
        }

        // Seed Transport Types
        if (!context.TransportTypes.Any())
        {
            var transportTypes = new List<TransportType>
            {
                CreateTransportType("Авто", false, false, true, false, true),
                CreateTransportType("Автовоз", false, false, true, false, true),
                CreateTransportType("Платформа", false, false, true, false, true),
                CreateTransportType("Стандартный тент", false, false, true, false, true),
                CreateTransportType("Тент, 30м²", false, false, true, false, true),
                CreateTransportType("Тент, 50м³", false, false, true, false, true),
                CreateTransportType("Тент, 86м²", false, false, true, false, true),
                CreateTransportType("40 м³ закрытый", false, false, true, false, true),
                CreateTransportType("ЖД", false, false, false, true, true),
                CreateTransportType("Морской", false, true, false, false, true),
                CreateTransportType("20' GP", false, true, false, false, true),
                CreateTransportType("40' GP", false, true, false, false, true),
                CreateTransportType("40' HC", false, true, false, false, true),
                CreateTransportType("45' HC", false, true, false, false, true),
                CreateTransportType("Авиа", true, false, false, false, true)
            };
            context.TransportTypes.AddRange(transportTypes);
            context.SaveChanges();
        }
    }

    private static TransportType CreateTransportType(string name, bool isAir, bool isSea, bool isRoad, bool isRail, bool isActive)
    {
        var transportTypeId = Guid.NewGuid();
        var transportType = new TransportType
        {
            Id = transportTypeId,
            Name = name,
            IsAir = isAir,
            IsSea = isSea,
            IsRoad = isRoad,
            IsRail = isRail,
            IsActive = isActive,
            CreatedAt = DateTime.UtcNow,
            Translations = new List<TransportTypeTranslation>
            {
                new TransportTypeTranslation
                {
                    Id = Guid.NewGuid(),
                    TransportTypeId = transportTypeId,
                    LanguageCode = "EN",
                    Name = name // Will be translated later if needed
                }
            }
        };
        return transportType;
    }

    private static SalesFunnelStatus CreateSalesFunnelStatus(string name, int position, Guid? managerId, int days, bool sendEmail, bool sendNotification, bool isActive)
    {
        var statusId = Guid.NewGuid();
        var status = new SalesFunnelStatus
        {
            Id = statusId,
            Name = name,
            StatusPosition = position,
            ResponsibleManagerId = managerId,
            NumberOfDays = days,
            SendToEmail = sendEmail,
            SendNotification = sendNotification,
            IsActive = isActive,
            CreatedAt = DateTime.UtcNow,
            Translations = new List<SalesFunnelStatusTranslation>
            {
                new SalesFunnelStatusTranslation
                {
                    Id = Guid.NewGuid(),
                    SalesFunnelStatusId = statusId,
                    LanguageCode = "EN",
                    Name = name
                }
            }
        };
        return status;
    }

    private static Packaging CreatePackagingWithTranslations(string defaultName, Dictionary<string, string> translations)
    {
        var packaging = new Packaging
        {
            Id = Guid.NewGuid(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            Translations = new List<PackagingTranslation>()
        };

        // Add EN as default if not in translations
        if (!translations.ContainsKey("EN"))
        {
            translations["EN"] = defaultName;
        }

        foreach (var translation in translations)
        {
            packaging.Translations.Add(new PackagingTranslation
            {
                Id = Guid.NewGuid(),
                PackagingId = packaging.Id,
                LanguageCode = translation.Key,
                Name = translation.Value
            });
        }

        return packaging;
    }
}

