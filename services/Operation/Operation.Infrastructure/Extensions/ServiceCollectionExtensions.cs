using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Operation.Domain.AggregatesModel.OperationAggregate;
using Operation.Infrastructure.Persistence;
using Operation.Infrastructure.Persistence.Repositories;

namespace Operation.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is required.");
        services.AddDbContext<OperationDbContext>(o => o.UseNpgsql(conn));
        services.AddScoped<IOperationTypeRepository, OperationTypeRepository>();
        services.AddScoped<IOperationRepository, OperationRepository>();
        services.AddScoped<IOperationDimensionRepository, OperationDimensionRepository>();
        services.AddScoped<IOperationPackageLineRepository, OperationPackageLineRepository>();
        services.AddScoped<IOperationVasRepository, OperationVasRepository>();
        return services;
    }
}
