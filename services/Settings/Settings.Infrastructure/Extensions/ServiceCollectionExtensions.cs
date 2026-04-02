using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Settings.Domain.AggregatesModel.BankAggregate;
using Settings.Domain.AggregatesModel.CarrierTypeAggregate;
using Settings.Domain.AggregatesModel.ClientSegmentAggregate;
using Settings.Domain.AggregatesModel.DeferredPaymentConditionAggregate;
using Settings.Domain.AggregatesModel.DrivingLicenceCategoryAggregate;
using Settings.Domain.AggregatesModel.LoadingMethodAggregate;
using Settings.Domain.AggregatesModel.PackagingAggregate;
using Settings.Domain.AggregatesModel.RequestPurposeAggregate;
using Settings.Domain.AggregatesModel.RequestSourceAggregate;
using Settings.Domain.AggregatesModel.QuoteSourceAggregate;
using Settings.Domain.AggregatesModel.SalesFunnelStatusAggregate;
using Settings.Domain.AggregatesModel.TransportTypeAggregate;
using Settings.Domain.AggregatesModel.WorkerPostAggregate;
using Settings.Domain.AggregatesModel.WayOfNegotiationAggregate;
using Settings.Domain.AggregatesModel.ResultTypeAggregate;
using Settings.Domain.AggregatesModel.FunnelResultAggregate;
using Settings.Domain.AggregatesModel.StateAggregate;
using Settings.Domain.AggregatesModel.CityAggregate;
using Settings.Domain.AggregatesModel.GlobalZoneAggregate;
using Settings.Domain.AggregatesModel.CountryAggregate;
using Settings.Domain.AggregatesModel.ClientSourceAggregate;
using Settings.Domain.AggregatesModel.CompanyAggregate;
using Settings.Domain.AggregatesModel.ExecutionPlaceAggregate;
using Settings.Domain.AggregatesModel.MeetingTypeAggregate;
using Settings.Domain.AggregatesModel.TaskStatusAggregate;
using Settings.Domain.AggregatesModel.TaskPriorityAggregate;
using Settings.Domain.AggregatesModel.MeetingStatusAggregate;
using Settings.Domain.AggregatesModel.MeetingResultAggregate;
using Settings.Domain.AggregatesModel.MeetingPriorityAggregate;
using Settings.Domain.AggregatesModel.UomAggregate;
using Settings.Domain.AggregatesModel.PricingTypeAggregate;
using Settings.Domain.AggregatesModel.DepartmentAggregate;
using Settings.Domain.AggregatesModel.AddressTypeAggregate;
using Settings.Domain.AggregatesModel.GeneralSettingAggregate;
using Settings.Domain.AggregatesModel.NumerationAggregate;
using Settings.Domain.AggregatesModel.SystemLogAggregate;
using Settings.Application.Interfaces.Services;
using Settings.Application.Services;
using Settings.Domain.AggregatesModel.ActionLogAggregate;
using Settings.Domain.AggregatesModel.EmailAccountAggregate;
using Settings.Domain.AggregatesModel.EmployeeGroupAggregate;
using Settings.Domain.AggregatesModel.MessageLogAggregate;
using Settings.Domain.AggregatesModel.TemplateAggregate;
using Settings.Infrastructure.Persistence;
using Settings.Infrastructure.Persistence.Repositories;
using Settings.Infrastructure.Services;

namespace Settings.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is required.");
        services.AddDbContext<SettingsDbContext>(o => o
            .UseNpgsql(conn, npgsql =>
            {
                npgsql.CommandTimeout(15);
                npgsql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            }));

        services.AddDataProtection();
        services.AddSingleton<ISmtpMailboxSecretProtector, SmtpMailboxSecretProtector>();
        services.AddSingleton<ISmtpMailboxTester, MailboxSmtpTester>();
        services.AddSingleton<ISmtpMailboxMessageSender, MailboxSmtpMessageSender>();

        services.AddScoped<IBankRepository, BankRepository>();
        services.AddScoped<ICarrierTypeRepository, CarrierTypeRepository>();
        services.AddScoped<IClientSegmentRepository, ClientSegmentRepository>();
        services.AddScoped<IRequestSourceRepository, RequestSourceRepository>();
        services.AddScoped<IQuoteSourceRepository, QuoteSourceRepository>();
        services.AddScoped<ISalesFunnelStatusRepository, SalesFunnelStatusRepository>();
        services.AddScoped<ITransportTypeRepository, TransportTypeRepository>();
        services.AddScoped<IRequestPurposeRepository, RequestPurposeRepository>();
        services.AddScoped<IDeferredPaymentConditionRepository, DeferredPaymentConditionRepository>();
        services.AddScoped<IPackagingRepository, PackagingRepository>();
        services.AddScoped<ILoadingMethodRepository, LoadingMethodRepository>();
        services.AddScoped<IWorkerPostRepository, WorkerPostRepository>();
        services.AddScoped<IDrivingLicenceCategoryRepository, DrivingLicenceCategoryRepository>();
        services.AddScoped<IWayOfNegotiationRepository, WayOfNegotiationRepository>();
        services.AddScoped<IResultTypeRepository, ResultTypeRepository>();
        services.AddScoped<IFunnelResultRepository, FunnelResultRepository>();
        services.AddScoped<IStateRepository, StateRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IGlobalZoneRepository, GlobalZoneRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IClientSourceRepository, ClientSourceRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IExecutionPlaceRepository, ExecutionPlaceRepository>();
        services.AddScoped<IMeetingTypeRepository, MeetingTypeRepository>();
        services.AddScoped<ITaskStatusRepository, TaskStatusRepository>();
        services.AddScoped<ITaskPriorityRepository, TaskPriorityRepository>();
        services.AddScoped<IMeetingStatusRepository, MeetingStatusRepository>();
        services.AddScoped<IMeetingResultRepository, MeetingResultRepository>();
        services.AddScoped<IMeetingPriorityRepository, MeetingPriorityRepository>();
        services.AddScoped<IUomRepository, UomRepository>();
        services.AddScoped<IPricingTypeRepository, PricingTypeRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IAddressTypeRepository, AddressTypeRepository>();
        services.AddScoped<IGeneralSettingRepository, GeneralSettingRepository>();
        services.AddScoped<INumerationRepository, NumerationRepository>();
        services.AddScoped<ISystemLogRepository, SystemLogRepository>();
        services.AddScoped<IActionLogRepository, ActionLogRepository>();
        services.AddScoped<IMessageLogRepository, MessageLogRepository>();
        services.AddScoped<ITemplateRepository, TemplateRepository>();
        services.AddScoped<IInternalActionLogService, InternalActionLogService>();
        services.AddScoped<IEmailAccountSettingRepository, EmailAccountSettingRepository>();
        services.AddScoped<IEmployeeGroupRepository, EmployeeGroupRepository>();

        return services;
    }
}
