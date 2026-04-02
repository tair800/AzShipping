using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.AirlineAggregate;
using Carrier.Domain.AggregatesModel.RailwayStationAggregate;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using Carrier.Domain.AggregatesModel.ShippingAgentAggregate;
using Carrier.Domain.AggregatesModel.DriverAggregate;
using Carrier.Domain.AggregatesModel.ShippingLineAggregate;
using Carrier.Domain.AggregatesModel.TerminalAggregate;
using Carrier.Domain.AggregatesModel.VehicleAggregate;
using Carrier.Infrastructure.Persistence;
using Carrier.Infrastructure.Persistence.Repositories;
using Carrier.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Carrier.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ActionLogClientOptions>(o =>
            o.SettingsBaseUrl = configuration["Services:Settings"] ?? "http://localhost:5064");
        services.AddHttpClient();
        services.AddScoped<IActionLogClient, ActionLogClient>();
        var conn = configuration.GetConnectionString("DefaultConnection")
            ?? "Host=localhost;Port=5432;Database=CarrierDb;Username=postgres;Password=12345@Tt";
        services.AddDbContext<CarrierDbContext>(o => o.UseNpgsql(conn));
        services.AddScoped<ICarrierRepository, CarrierRepository>();
        services.AddScoped<ITerminalRepository, TerminalRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        services.AddScoped<ICarrierDirectionRepository, CarrierDirectionRepository>();
        services.AddScoped<ICarrierDocumentRepository, CarrierDocumentRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IShippingLineRepository, ShippingLineRepository>();
        services.AddScoped<IAirlineRepository, AirlineRepository>();
        services.AddScoped<IShippingAgentRepository, ShippingAgentRepository>();
        services.AddScoped<IRailwayStationRepository, RailwayStationRepository>();
        return services;
    }
}
