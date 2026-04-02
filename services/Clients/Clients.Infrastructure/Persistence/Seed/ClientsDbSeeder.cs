using Clients.Domain.AggregatesModel.ClientAggregate;
using Clients.Domain.AggregatesModel.CurrencyAggregate;
using Clients.Domain.AggregatesModel.DirectionAggregate;
using Microsoft.EntityFrameworkCore;

namespace Clients.Infrastructure.Persistence.Seed;

public static class ClientsDbSeeder
{
    public static async Task SeedAsync(ClientsDbContext context, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        if (!await context.Currencies.AnyAsync(cancellationToken))
        {
            var currencies = new[]
            {
                (Code: "USD", Name: "US Dollar"),
                (Code: "EUR", Name: "Euro"),
                (Code: "GEL", Name: "Georgian Lari"),
                (Code: "GBP", Name: "British Pound"),
                (Code: "RUB", Name: "Russian Ruble"),
                (Code: "TRY", Name: "Turkish Lira"),
                (Code: "UAH", Name: "Ukrainian Hryvnia"),
                (Code: "AZN", Name: "Azerbaijani Manat"),
            };
            foreach (var (code, name) in currencies)
                context.Currencies.Add(new Currency { Id = Guid.NewGuid(), Code = code, Name = name });
            await context.SaveChangesAsync(cancellationToken);
        }

        if (!await context.Clients.AnyAsync(cancellationToken))
        {
            var usdId = (await context.Currencies.FirstOrDefaultAsync(c => c.Code == "USD", cancellationToken))?.Id;
            var eurId = (await context.Currencies.FirstOrDefaultAsync(c => c.Code == "EUR", cancellationToken))?.Id;

            var clients = new[]
            {
                (Code: "CL-SEED1", Company: "Acme Logistics Ltd", Abbr: "Acme", Vat: "123456789", Phone: "+1-555-0100", Email: "contact@acme.example.com"),
                (Code: "CL-SEED2", Company: "Global Freight Solutions", Abbr: "GFS", Vat: "987654321", Phone: "+44-20-7123-4567", Email: "info@globalfreight.example.com"),
                (Code: "CL-SEED3", Company: "Caucasus Shipping Co", Abbr: "CSC", Vat: "112233445", Phone: "+995-32-2-123456", Email: "office@caucasusshipping.example.com"),
            };

            foreach (var (code, company, abbr, vat, phone, email) in clients)
            {
                var clientId = Guid.NewGuid();
                var client = new Client
                {
                    Id = clientId,
                    Code = code,
                    IsCustomer = true,
                    ShipperClientNotRequired = false,
                    CompanyName = company,
                    NameAbbreviated = abbr,
                    VatNumber = vat,
                    LegalStreet = "Sample Street 1",
                    LegalZipCode = "10001",
                    LegalCountryId = new Guid("11111111-2222-3333-4444-555555555501"),
                    PostalCountryId = new Guid("11111111-2222-3333-4444-555555555501"),
                    LegalEmail = email,
                    PostalPhone = phone,
                    PostalEmail = email,
                    PaymentDelay = 30,
                    EmailToSendDocuments = email,
                    Comment = "Seeded sample client",
                    CreatedAt = now
                };
                context.Clients.Add(client);

                context.ClientContactPersons.Add(new ClientContactPerson
                {
                    Id = Guid.NewGuid(),
                    ClientId = clientId,
                    EnglishName = "John Smith",
                    Phone = phone,
                    Email = email
                });

                context.ClientBankAccounts.Add(new ClientBankAccount
                {
                    Id = Guid.NewGuid(),
                    ClientId = clientId,
                    CurrencyId = usdId ?? eurId,
                    AccountNumberIban = "GE00AAAA0000000000000000"
                });
            }

            await context.SaveChangesAsync(cancellationToken);

            var firstClientId = (await context.Clients.FirstAsync(cancellationToken)).Id;
            context.Directions.Add(new Direction
            {
                Id = Guid.NewGuid(),
                ClientId = firstClientId,
                Note = "Tbilisi – Batumi route",
                Comments = "Regular weekly shipments"
            });
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
