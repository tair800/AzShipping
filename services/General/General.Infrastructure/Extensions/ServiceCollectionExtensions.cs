using General.Application.Services;
using General.Domain.AggregatesModel.CurrencyAggregate;
using General.Domain.AggregatesModel.IncotermAggregate;
using General.Domain.AggregatesModel.MeetingAggregate;
using General.Domain.AggregatesModel.MeetingHistoryAggregate;
using General.Domain.AggregatesModel.VasAggregate;
using General.Domain.AggregatesModel.VesselAggregate;
using General.Domain.AggregatesModel.ProjectAggregate;
using General.Domain.AggregatesModel.TaskAggregate;
using General.Domain.AggregatesModel.EmployeeAggregate;
using General.Infrastructure.Persistence;
using General.Infrastructure.Persistence.Repositories;
using General.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace General.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ActionLogClientOptions>(o =>
            o.SettingsBaseUrl = configuration["Services:Settings"] ?? "http://localhost:5064");
        services.Configure<TaskDocumentStorageOptions>(configuration.GetSection("TaskDocuments"));
        services.AddSingleton<ITaskDocumentStorage, TaskDocumentFileStorage>();
        services.AddHttpClient();
        services.AddScoped<IActionLogClient, ActionLogClient>();
        var conn = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is required.");
        services.AddDbContext<GeneralDbContext>(o => o.UseNpgsql(conn));
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskDocumentRepository, TaskDocumentRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IIncotermRepository, IncotermRepository>();
        services.AddScoped<IVasRepository, VasRepository>();
        services.AddScoped<IVesselRepository, VesselRepository>();
        services.AddScoped<IMeetingRepository, MeetingRepository>();
        services.AddScoped<IMeetingHistoryRepository, MeetingHistoryRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeNoteRepository, EmployeeNoteRepository>();
        services.AddScoped<ISettingsCatalogLookup, SettingsCatalogLookup>();
        return services;
    }
}
