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

        // Seed Loading Methods
        if (!context.LoadingMethods.Any())
        {
            var loadingMethods = new List<LoadingMethod>
            {
                CreateLoadingMethodWithTranslations("Top", new Dictionary<string, string>
                {
                    { "EN", "Top" },
                    { "LT", "Viršus" },
                    { "RU", "Сверху" }
                }),
                CreateLoadingMethodWithTranslations("Side", new Dictionary<string, string>
                {
                    { "EN", "Side" },
                    { "LT", "Šonas" },
                    { "RU", "Сбоку" }
                }),
                CreateLoadingMethodWithTranslations("Back", new Dictionary<string, string>
                {
                    { "EN", "Back" },
                    { "LT", "Galinė" },
                    { "RU", "Сзади" }
                })
            };

            context.LoadingMethods.AddRange(loadingMethods);
            context.SaveChanges();
        }

        // Seed Deferred Payment Conditions
        if (!context.DeferredPaymentConditions.Any())
        {
            var conditions = new List<DeferredPaymentCondition>
            {
                new DeferredPaymentCondition
                {
                    Id = Guid.NewGuid(),
                    Name = "Bit 10 calendar days.",
                    FullText = "Bank transfer within 10 calendar days after receipt of the original invoice, act, CMR with the recipient's mark.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new DeferredPaymentCondition
                {
                    Id = Guid.NewGuid(),
                    Name = "Bit 3 calendar days.",
                    FullText = "Bank transfer within 3 calendar days after receiving the originals of the invoice, act, CMR with the recipient's mark.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new DeferredPaymentCondition
                {
                    Id = Guid.NewGuid(),
                    Name = "Bit 30 calendar days.",
                    FullText = "Bank transfer within 30 calendar days after receiving the originals of the invoice, act, CMR with the recipient's mark.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new DeferredPaymentCondition
                {
                    Id = Guid.NewGuid(),
                    Name = "Bit 5 calendar days.",
                    FullText = "Bank transfer within 5 calendar days after receipt of the original invoice, act, CMR with the recipient's mark.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new DeferredPaymentCondition
                {
                    Id = Guid.NewGuid(),
                    Name = "Bit 7 calendar days.",
                    FullText = "Bank transfer within 7 calendar days after receipt of the original invoice, act, CMR with the recipient's mark.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.DeferredPaymentConditions.AddRange(conditions);
            context.SaveChanges();
        }

        // Seed Request Purposes
        if (!context.RequestPurposes.Any())
        {
            var purposes = new List<RequestPurpose>
            {
                new RequestPurpose
                {
                    Id = Guid.NewGuid(),
                    Name = "Price calculation only",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new RequestPurpose
                {
                    Id = Guid.NewGuid(),
                    Name = "Real + price calculation",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new RequestPurpose
                {
                    Id = Guid.NewGuid(),
                    Name = "Real cargo",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.RequestPurposes.AddRange(purposes);
            context.SaveChanges();
        }

        // Seed Driving Licence Categories
        if (!context.DrivingLicenceCategories.Any())
        {
            var categories = new List<DrivingLicenceCategory>
            {
                new DrivingLicenceCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "A",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new DrivingLicenceCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "B",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new DrivingLicenceCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "C",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new DrivingLicenceCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "D",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                },
                new DrivingLicenceCategory
                {
                    Id = Guid.NewGuid(),
                    Name = "E",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.DrivingLicenceCategories.AddRange(categories);
            context.SaveChanges();
        }

        // Seed Worker Posts
        if (!context.WorkerPosts.Any())
        {
            var posts = new List<WorkerPost>
            {
                CreateWorkerPostWithTranslations("Accountant", new Dictionary<string, string>
                {
                    { "EN", "Accountant" },
                    { "RU", "Бухгалтер" },
                    { "PL", "Księgowy" },
                    { "LT", "Buhalteris" }
                }),
                CreateWorkerPostWithTranslations("Commercial Director", new Dictionary<string, string>
                {
                    { "EN", "Commercial Director" },
                    { "RU", "Коммерческий директор" },
                    { "PL", "Dyrektor handlowy" },
                    { "LT", "Komercinis direktorius" }
                }),
                CreateWorkerPostWithTranslations("Deputy Director", new Dictionary<string, string>
                {
                    { "EN", "Deputy Director" },
                    { "RU", "Заместитель директора" },
                    { "PL", "Zastępca dyrektora" },
                    { "LT", "Direktoriaus pavaduotojas" }
                }),
                CreateWorkerPostWithTranslations("Director", new Dictionary<string, string>
                {
                    { "EN", "Director" },
                    { "RU", "Директор" },
                    { "PL", "Dyrektor" },
                    { "LT", "Direktorius" }
                }),
                CreateWorkerPostWithTranslations("General Director", new Dictionary<string, string>
                {
                    { "EN", "General Director" },
                    { "RU", "Генеральный директор" },
                    { "PL", "Dyrektor generalny" },
                    { "LT", "Generalinis direktorius" }
                }),
                CreateWorkerPostWithTranslations("Head of Sales Department", new Dictionary<string, string>
                {
                    { "EN", "Head of Sales Department" },
                    { "RU", "Начальник отдела продаж" },
                    { "PL", "Kierownik działu sprzedaży" },
                    { "LT", "Pardavimų skyriaus vadovas" }
                }),
                CreateWorkerPostWithTranslations("Head of Transport Department", new Dictionary<string, string>
                {
                    { "EN", "Head of Transport Department" },
                    { "RU", "Начальник транспортного отдела" },
                    { "PL", "Kierownik działu transportu" },
                    { "LT", "Transporto skyriaus vadovas" }
                }),
                CreateWorkerPostWithTranslations("Logist", new Dictionary<string, string>
                {
                    { "EN", "Logist" },
                    { "RU", "Логист" },
                    { "PL", "Logistyk" },
                    { "LT", "Logistikas" }
                }),
                CreateWorkerPostWithTranslations("Manager", new Dictionary<string, string>
                {
                    { "EN", "Manager" },
                    { "RU", "Менеджер" },
                    { "PL", "Menedżer" },
                    { "LT", "Vadybininkas" }
                })
            };

            context.WorkerPosts.AddRange(posts);
            context.SaveChanges();
        }

        SeedCarrierTypes(context);
        SeedBanks(context);
    }

    private static void SeedCarrierTypes(ApplicationDbContext context)
    {
        if (!context.CarrierTypes.Any())
        {
            var carrierTypes = new List<CarrierType>
            {
                new CarrierType
                {
                    Id = Guid.NewGuid(),
                    Name = "New",
                    IsActive = true,
                    Colour = "#000000",
                    IsDefault = false,
                    CreatedAt = DateTime.UtcNow
                },
                new CarrierType
                {
                    Id = Guid.NewGuid(),
                    Name = "Reliable",
                    IsActive = true,
                    Colour = "#000000",
                    IsDefault = false,
                    CreatedAt = DateTime.UtcNow
                },
                new CarrierType
                {
                    Id = Guid.NewGuid(),
                    Name = "Unreliable",
                    IsActive = true,
                    Colour = "#000000",
                    IsDefault = false,
                    CreatedAt = DateTime.UtcNow
                },
                new CarrierType
                {
                    Id = Guid.NewGuid(),
                    Name = "Unverified",
                    IsActive = true,
                    Colour = "#000000",
                    IsDefault = false,
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.CarrierTypes.AddRange(carrierTypes);
            context.SaveChanges();
        }
    }

    private static WorkerPost CreateWorkerPostWithTranslations(string defaultName, Dictionary<string, string> translations)
    {
        var post = new WorkerPost
        {
            Id = Guid.NewGuid(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            Translations = new List<WorkerPostTranslation>()
        };

        // Add EN as default if not in translations
        if (!translations.ContainsKey("EN"))
        {
            translations["EN"] = defaultName;
        }

        foreach (var translation in translations)
        {
            post.Translations.Add(new WorkerPostTranslation
            {
                Id = Guid.NewGuid(),
                WorkerPostId = post.Id,
                LanguageCode = translation.Key,
                Name = translation.Value
            });
        }

        return post;
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

    private static LoadingMethod CreateLoadingMethodWithTranslations(string defaultName, Dictionary<string, string> translations)
    {
        var loadingMethod = new LoadingMethod
        {
            Id = Guid.NewGuid(),
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            Translations = new List<LoadingMethodTranslation>()
        };

        // Add EN as default if not in translations
        if (!translations.ContainsKey("EN"))
        {
            translations["EN"] = defaultName;
        }

        foreach (var translation in translations)
        {
            loadingMethod.Translations.Add(new LoadingMethodTranslation
            {
                Id = Guid.NewGuid(),
                LoadingMethodId = loadingMethod.Id,
                LanguageCode = translation.Key,
                Name = translation.Value
            });
        }

        return loadingMethod;
    }

    private static void SeedBanks(ApplicationDbContext context)
    {
        if (!context.Banks.Any())
        {
            var banks = new List<Bank>
            {
                new Bank
                {
                    Id = Guid.NewGuid(),
                    Name = "ASC XALQ Bankı",
                    Branch = "Gusar branch",
                    Code = "510233",
                    Swift = "HAJCAZ22XXX",
                    Country = "Azerbaijan",
                    City = "Gusar",
                    Address = "Азербайджан, Gusar, F.Musayev srt., 68",
                    PostCode = "AZ 3800",
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.Banks.AddRange(banks);
            context.SaveChanges();
        }
    }
}

