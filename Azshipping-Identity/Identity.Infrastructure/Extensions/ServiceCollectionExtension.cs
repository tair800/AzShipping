using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.UserAggregate;
using Identity.Infrastructure.Options;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MrStyx.Domain.SeedWork.Persistence;
using MrStyx.Infrastructure;

namespace Identity.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            options.UseNpgsql(connectionString);
        });

        services.RegisterRepositories(typeof(AppDbContext).Assembly, typeof(IUserRepository).Assembly);
        services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();

        services.AddPasswordOptions(configuration);
        services.AddRefreshTokenOptions(configuration);
        services.AddEmailOptions(configuration);

        services.AddScoped<IPermissionReadService, PermissionReadService>();
        services.AddScoped<IEmployeeGroupPermissionClaimsService, EmployeeGroupPermissionClaimsService>();

        services.Configure<SettingsClientOptions>(configuration.GetSection(SettingsClientOptions.SectionName));
        services.Configure<GeneralClientOptions>(configuration.GetSection(GeneralClientOptions.SectionName));
        services.Configure<LicensingOptions>(configuration.GetSection(LicensingOptions.SectionName));
        services.AddHttpClient();
        services.AddHttpClient("SettingsEmailRelay", client => client.Timeout = TimeSpan.FromSeconds(90));
        services.AddScoped<SettingsCatalogHttpReader>();
        services.AddScoped<IGeneralEmployeeProvisioningService, GeneralEmployeeProvisioningService>();
        services.AddScoped<IUserDtoEnrichmentService, UserDtoEnrichmentService>();
        services.AddScoped<ILicensingService, LicensingService>();
        services.AddScoped<IUserSignatureStorageService, UserSignatureStorageService>();

        return services;
    }
}