using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quotes.Application.Services;
using Quotes.Domain.AggregatesModel.AddressAggregate;
using Quotes.Domain.AggregatesModel.QuoteAggregate;
using Quotes.Infrastructure.Persistence;
using Quotes.Infrastructure.Persistence.Repositories;
using Quotes.Infrastructure.Services;

namespace Quotes.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ActionLogClientOptions>(o =>
            o.SettingsBaseUrl = configuration["Services:Settings"] ?? "http://localhost:5064");
        services.AddHttpClient();
        services.AddScoped<IActionLogClient, ActionLogClient>();
        var conn = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is required.");
        services.AddDbContext<QuotesDbContext>(o => o.UseNpgsql(conn));
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IQuoteTypeRepository, QuoteTypeRepository>();
        services.AddScoped<IQuoteRepository, QuoteRepository>();
        return services;
    }
}
