using Carrier.Domain.AggregatesModel.RailwayStationAggregate;
using Carrier.Domain.AggregatesModel.ShippingAgentAggregate;
using Carrier.Domain.AggregatesModel.ShippingLineAggregate;
using Carrier.Domain.AggregatesModel.AirlineAggregate;
using Carrier.Domain.AggregatesModel.TerminalAggregate;
using Carrier.Domain.AggregatesModel.VehicleAggregate;
using Microsoft.EntityFrameworkCore;
using CarrierEntity = Carrier.Domain.AggregatesModel.CarrierAggregate.Carrier;

namespace Carrier.Infrastructure.Persistence.Seed;

public static class CarrierDbSeeder
{
    public static async Task SeedAsync(CarrierDbContext context, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        if (!await context.Carriers.AnyAsync(cancellationToken))
        {
            var jan2024 = new DateTime(2024, 1, 10, 0, 0, 0, DateTimeKind.Utc);
            var jun2024 = new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);
            var sep2024 = new DateTime(2024, 9, 1, 0, 0, 0, DateTimeKind.Utc);

            var carriers = new[]
            {
                new CarrierEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Azerbaijan Airlines Cargo",
                    LocalName = "Azərbaycan Hava Yolları",
                    CarrierTypeId = null,
                    TransportTypeId = null,
                    CarrierDirection = "AZE - TR",
                    DateOfCreation = jan2024,
                    LegalZipCode = "AZ1000",
                    LegalPhones = "+994 12 598 0000",
                    LegalEmails = "cargo@azal.az",
                    PostalZipCode = "AZ1000",
                    Comment = "Air cargo carrier. Heydar Aliyev Ave 1, Baku",
                    IsDeactive = false,
                    CreatedAt = now
                },
                new CarrierEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "BSC Baku Shipping",
                    CarrierTypeId = null,
                    TransportTypeId = null,
                    CarrierDirection = "AZE - GE",
                    DateOfCreation = jun2024,
                    LegalZipCode = "AZ0100",
                    LegalPhones = "+994 12 465 1234",
                    LegalEmails = "info@bakushipping.az",
                    PostalZipCode = "AZ0100",
                    Comment = "Sea freight. Port of Baku, Alat",
                    IsDeactive = false,
                    CreatedAt = now
                },
                new CarrierEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "TransCaucasus Logistics",
                    CarrierTypeId = null,
                    TransportTypeId = null,
                    CarrierDirection = "TR - AZE - GE",
                    DateOfCreation = sep2024,
                    LegalZipCode = "34000",
                    LegalPhones = "+90 212 555 0100",
                    LegalEmails = "contact@transcaucasus.com",
                    PostalZipCode = "34000",
                    Comment = "Road freight, intermodal. Esenler Logistics Park, Istanbul",
                    IsDeactive = false,
                    CreatedAt = now
                },
                new CarrierEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Georgian Rail Cargo",
                    LocalName = "საქართველოს რკინიგზა",
                    CarrierTypeId = null,
                    TransportTypeId = null,
                    CarrierDirection = "GE - AZE",
                    DateOfCreation = jun2024,
                    LegalZipCode = "0100",
                    LegalPhones = "+995 32 2 99 00 00",
                    LegalEmails = "cargo@railway.ge",
                    PostalZipCode = "0100",
                    Comment = "Rail freight. Tbilisi Central Station",
                    IsDeactive = false,
                    CreatedAt = now
                },
                new CarrierEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "DHL Express Caucasus",
                    CarrierTypeId = null,
                    TransportTypeId = null,
                    CarrierDirection = "International",
                    DateOfCreation = jan2024,
                    LegalZipCode = "AZ1044",
                    LegalPhones = "+994 12 404 0505",
                    LegalEmails = "azerbaijan@dhl.com",
                    PostalZipCode = "AZ1044",
                    Comment = "Express air and road. Airport Road 5, Baku",
                    IsDeactive = false,
                    CreatedAt = now
                }
            };

            foreach (var c in carriers)
                context.Carriers.Add(c);
        }

        if (!await context.Terminals.AnyAsync(cancellationToken))
        {
            var jan2025 = new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc);
            var feb2025 = new DateTime(2025, 2, 1, 0, 0, 0, DateTimeKind.Utc);
            var mar2025 = new DateTime(2025, 3, 10, 0, 0, 0, DateTimeKind.Utc);

            var terminals = new[]
        {
            new Terminal
            {
                Id = Guid.NewGuid(),
                Name = "Baku Port Terminal",
                CountryId = null,
                CityId = null,
                Address = "Heydar Aliyev Avenue 1, Baku",
                PostCode = "AZ1000",
                RailwayStation = "Baku Central",
                TransportTypeIds = null, // Air, Sea, Road, Rail - assign via UI
                Notes = "Main logistics hub in Baku",
                IsDeactive = false,
                DateOfCreation = jan2025,
                CreatedAt = now
            },
            new Terminal
            {
                Id = Guid.NewGuid(),
                Name = "Tbilisi Cargo Center",
                CountryId = null,
                CityId = null,
                Address = "Airport Road 15, Tbilisi",
                PostCode = "0198",
                RailwayStation = "Tbilisi Central Station",
                TransportTypeIds = null,
                Notes = "Handles air and road freight",
                IsDeactive = false,
                DateOfCreation = jan2025,
                CreatedAt = now
            },
            new Terminal
            {
                Id = Guid.NewGuid(),
                Name = "Batumi Seaport Terminal",
                CountryId = null,
                CityId = null,
                Address = "Rustaveli Avenue 1, Batumi",
                PostCode = "6010",
                RailwayStation = null,
                TransportTypeIds = null,
                Notes = "Sea freight terminal",
                IsDeactive = false,
                DateOfCreation = feb2025,
                CreatedAt = now
            },
            new Terminal
            {
                Id = Guid.NewGuid(),
                Name = "Istanbul Intermodal Hub",
                CountryId = null,
                CityId = null,
                Address = "Esenler District, Logistics Park 5",
                PostCode = "34000",
                RailwayStation = "Halkali Railway Station",
                TransportTypeIds = null,
                Notes = "Air, sea, road and rail",
                IsDeactive = false,
                DateOfCreation = feb2025,
                CreatedAt = now
            },
            new Terminal
            {
                Id = Guid.NewGuid(),
                Name = "Ganja Road Terminal",
                CountryId = null,
                CityId = null,
                Address = "Heydar Aliyev Highway, km 15",
                PostCode = "AZ2000",
                RailwayStation = "Ganja Station",
                TransportTypeIds = null,
                Notes = "Road and rail cargo",
                IsDeactive = false,
                DateOfCreation = mar2025,
                CreatedAt = now
            },
            new Terminal
            {
                Id = Guid.NewGuid(),
                Name = "Shipping Az Warehouse",
                CountryId = null,
                CityId = null,
                Address = "Place name",
                PostCode = null,
                RailwayStation = null,
                TransportTypeIds = null,
                Notes = null,
                IsDeactive = false,
                DateOfCreation = new DateTime(2026, 1, 12, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = now
            }
        };

            foreach (var t in terminals)
                context.Terminals.Add(t);
        }

        if (!await context.VehicleBrands.AnyAsync(cancellationToken))
        {
            foreach (var name in new[] { "Mercedes-Benz", "Scania", "Volvo", "MAN", "DAF", "Iveco", "Renault", "Kamaz" })
                context.VehicleBrands.Add(new VehicleBrand { Id = Guid.NewGuid(), Name = name });
        }

        if (!await context.VehicleModels.AnyAsync(cancellationToken))
        {
            foreach (var name in new[] { "Actros", "R-Series", "FH", "TGX", "XF", "Stralis", "T-Range", "5490" })
                context.VehicleModels.Add(new VehicleModel { Id = Guid.NewGuid(), Name = name });
        }

        if (!await context.EuroEmissionClasses.AnyAsync(cancellationToken))
        {
            foreach (var name in new[] { "Euro 3", "Euro 4", "Euro 5", "Euro 6" })
                context.EuroEmissionClasses.Add(new EuroEmissionClass { Id = Guid.NewGuid(), Name = name });
        }

        if (!await context.VehicleGroups.AnyAsync(cancellationToken))
        {
            foreach (var name in new[] { "Road", "Sea", "Rail", "Air", "Fleet A", "Fleet B" })
                context.VehicleGroups.Add(new VehicleGroup { Id = Guid.NewGuid(), Name = name });
        }

        if (!await context.ShippingLines.AnyAsync(cancellationToken))
        {
            var shippingLines = new[]
            {
                new ShippingLine { Id = Guid.NewGuid(), Code = "MSC", ScacCode = "MSCU", Name = "Mediterranean Shipping Company", LocalName = "MSC", Website = "https://www.msc.com", VatNo = null, IsActive = true, CreatedAt = now },
                new ShippingLine { Id = Guid.NewGuid(), Code = "MAEU", ScacCode = "MAEU", Name = "Maersk Line", LocalName = "Maersk", Website = "https://www.maersk.com", VatNo = null, IsActive = true, CreatedAt = now },
                new ShippingLine { Id = Guid.NewGuid(), Code = "CMDU", ScacCode = "CMDU", Name = "CMA CGM", LocalName = "CMA CGM", Website = "https://www.cma-cgm.com", VatNo = null, IsActive = true, CreatedAt = now },
                new ShippingLine { Id = Guid.NewGuid(), Code = "HLCU", ScacCode = "HLCU", Name = "Hapag-Lloyd", LocalName = "Hapag-Lloyd", Website = "https://www.hapag-lloyd.com", VatNo = null, IsActive = true, CreatedAt = now },
                new ShippingLine { Id = Guid.NewGuid(), Code = "COSU", ScacCode = "COSU", Name = "COSCO Shipping", LocalName = "中远海运", Website = "https://www.coscoshipping.com", VatNo = null, IsActive = true, CreatedAt = now }
            };
            foreach (var sl in shippingLines)
                context.ShippingLines.Add(sl);
        }

        if (!await context.Airlines.AnyAsync(cancellationToken))
        {
            var airlines = new[]
            {
                new Airline { Id = Guid.NewGuid(), Code = "J2", Icao = "AHY", Name = "Azerbaijan Airlines", LocalName = "Azərbaycan Hava Yolları", Prefix = "AZAL", Website = "https://www.azal.az", VatNo = null, IsActive = true, CreatedAt = now },
                new Airline { Id = Guid.NewGuid(), Code = "TK", Icao = "THY", Name = "Turkish Airlines", LocalName = "Türk Hava Yolları", Prefix = "TURKISH", Website = "https://www.turkishairlines.com", VatNo = null, IsActive = true, CreatedAt = now },
                new Airline { Id = Guid.NewGuid(), Code = "9U", Icao = "TAR", Name = "TAROM", LocalName = "Transporturile Aeriene Române", Prefix = "TAROM", Website = "https://www.tarom.ro", VatNo = null, IsActive = true, CreatedAt = now },
                new Airline { Id = Guid.NewGuid(), Code = "SU", Icao = "AFL", Name = "Aeroflot", LocalName = "Аэрофлот", Prefix = "AEROFLOT", Website = "https://www.aeroflot.ru", VatNo = null, IsActive = true, CreatedAt = now },
                new Airline { Id = Guid.NewGuid(), Code = "QR", Icao = "QTR", Name = "Qatar Airways", LocalName = "طيران قطر", Prefix = "QATARI", Website = "https://www.qatarairways.com", VatNo = null, IsActive = true, CreatedAt = now }
            };
            foreach (var a in airlines)
                context.Airlines.Add(a);
        }

        if (!await context.ShippingAgents.AnyAsync(cancellationToken))
        {
            var shippingAgents = new[]
            {
                new ShippingAgent { Id = Guid.NewGuid(), CompanyName = "Baku Maritime Agency", LocalName = "Bakı Dəniz Agentliyi", Address1 = "Port of Baku, Alat", ZipCode = "AZ0100", Email = "agency@bakumaritime.az", EnglishName = "John Williams", Position = "Operations Manager", BusinessPhone = "+994 12 465 1000", IsActive = true, CreatedAt = now },
                new ShippingAgent { Id = Guid.NewGuid(), CompanyName = "Caucasus Freight Services", LocalName = "Qafqaz Yük Agentliyi", Address1 = "Nizami St 45", CityId = null, ZipCode = "AZ1000", Email = "info@caucasusfreight.az", EnglishName = "Maria Garcia", Position = "Shipping Coordinator", Mobile = "+994 50 123 4567", IsActive = true, CreatedAt = now },
                new ShippingAgent { Id = Guid.NewGuid(), CompanyName = "Istanbul Port Agency", LocalName = "İstanbul Liman Ajansı", Address1 = "Karaköy District", ZipCode = "34425", Email = "contact@istanbulport.com", EnglishName = "Mehmet Yilmaz", Position = "Port Agent", BusinessPhone = "+90 212 555 0100", IsActive = true, CreatedAt = now },
                new ShippingAgent { Id = Guid.NewGuid(), CompanyName = "Batumi Shipping Co", LocalName = "ბათუმის ტრანსპორტირების კომპანია", Address1 = "Rustaveli Ave 1", ZipCode = "6010", Email = "office@batumishipping.ge", EnglishName = "Giorgi Kvirikashvili", Position = "Manager", BusinessPhone = "+995 422 27 00 00", IsActive = true, CreatedAt = now }
            };
            foreach (var sa in shippingAgents)
                context.ShippingAgents.Add(sa);
        }

        if (!await context.RailwayStations.AnyAsync(cancellationToken))
        {
            var railwayStations = new[]
            {
                new RailwayStation { Id = Guid.NewGuid(), Code = "BAK-C", Name = "Baku Central", Railway = "ADY", LocalName = "Bakı Mərkəzi", Prefix = "BAK", Website = null, VatNo = null, Notes = "Main railway hub", IsActive = true, CreatedAt = now },
                new RailwayStation { Id = Guid.NewGuid(), Code = "GAN-S", Name = "Ganja Station", Railway = "ADY", LocalName = "Gəncə Stansiyası", Prefix = "GAN", Website = null, VatNo = null, IsActive = true, CreatedAt = now },
                new RailwayStation { Id = Guid.NewGuid(), Code = "TBS-C", Name = "Tbilisi Central", Railway = "GR", LocalName = "თბილისის ცენტრალი", Prefix = "TBS", Website = null, VatNo = null, Notes = "Georgian Railway main station", IsActive = true, CreatedAt = now },
                new RailwayStation { Id = Guid.NewGuid(), Code = "BAT-S", Name = "Batumi Station", Railway = "GR", LocalName = "ბათუმის სადგური", Prefix = "BAT", Website = null, VatNo = null, IsActive = true, CreatedAt = now },
                new RailwayStation { Id = Guid.NewGuid(), Code = "IST-H", Name = "Halkali Railway Station", Railway = "TCDD", LocalName = "Halkalı İstasyonu", Prefix = "IST", Website = null, VatNo = null, Notes = "Istanbul freight hub", IsActive = true, CreatedAt = now },
                new RailwayStation { Id = Guid.NewGuid(), Code = "23546", Name = "Sumgayit Freight Terminal", Railway = "ADY", LocalName = "Sumqayıt Yük Terminalı", Prefix = "AZShipping", Website = "https://ady.az", VatNo = "23456", IsActive = true, CreatedAt = now }
            };
            foreach (var rs in railwayStations)
                context.RailwayStations.Add(rs);
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
