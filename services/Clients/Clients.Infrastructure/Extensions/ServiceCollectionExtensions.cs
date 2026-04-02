using Clients.Application.Services;
using Clients.Infrastructure.Services;
using Clients.Domain.AggregatesModel.ClientAggregate;
using Clients.Domain.AggregatesModel.CurrencyAggregate;
using Clients.Domain.AggregatesModel.DirectionAggregate;
using Clients.Domain.AggregatesModel.DocumentAggregate;
using Clients.Domain.AggregatesModel.NegotiationAggregate;
using Clients.Infrastructure.Persistence;
using Clients.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clients.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var settingsUrl = configuration["Services:Settings"] ?? "http://localhost:5064";
        services.Configure<ActionLogClientOptions>(o => o.SettingsBaseUrl = settingsUrl);
        services.AddHttpClient();
        services.AddHttpClient<ISettingsReferenceDataClient, SettingsReferenceDataClient>(client =>
            client.BaseAddress = new Uri(settingsUrl.TrimEnd('/') + "/"));
        services.AddScoped<IActionLogClient, ActionLogClient>();
        var conn = configuration.GetConnectionString("DefaultConnection")
            ?? "Host=localhost;Port=5432;Database=ClientsDb;Username=postgres;Password=postgres";
        services.AddDbContext<ClientsDbContext>(o => o.UseNpgsql(conn));
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IDirectionRepository, DirectionRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<INegotiationRepository, NegotiationRepository>();
        services.AddScoped<INegotiationResultRepository, NegotiationResultRepository>();
        return services;
    }
}
