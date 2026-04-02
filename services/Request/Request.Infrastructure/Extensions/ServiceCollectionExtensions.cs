using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Request.Application.Services;
using Request.Domain.AggregatesModel.CommercialOfferAggregate;
using Request.Domain.AggregatesModel.PriceProposalAggregate;
using Request.Domain.AggregatesModel.RequestAggregate;
using Request.Domain.AggregatesModel.RequestCommentAggregate;
using Request.Domain.AggregatesModel.RequestNegotiationAggregate;
using Request.Domain.AggregatesModel.SaleAggregate;
using Request.Domain.AggregatesModel.SaleStatusAggregate;
using Request.Infrastructure.Persistence;
using Request.Infrastructure.Persistence.Repositories;
using Request.Infrastructure.Services;

namespace Request.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is required.");
        services.AddDbContext<RequestDbContext>(o => o.UseNpgsql(conn)
            .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning)));
        services.Configure<VatRateLookupServiceOptions>(o =>
        {
            o.AccountingBaseUrl = configuration["Services:Accounting"] ?? "http://localhost:5072";
            o.SettingsBaseUrl = configuration["Services:Settings"] ?? "http://localhost:5064";
        });
        services.AddHttpClient();
        services.AddScoped<IVatRateLookupService, VatRateLookupService>();
        services.AddScoped<IActionLogClient, ActionLogClient>();
        services.AddScoped<ISaleStatusRepository, SaleStatusRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<IRequestNegotiationRepository, RequestNegotiationRepository>();
        services.AddScoped<IRequestCommentRepository, RequestCommentRepository>();
        services.AddScoped<IPriceProposalRepository, PriceProposalRepository>();
        services.AddScoped<ICommercialOfferRepository, CommercialOfferRepository>();
        services.AddScoped<IRequestTypeRepository, RequestTypeRepository>();
        services.AddScoped<IRequestRepository, RequestRepository>();
        services.AddScoped<IRequestDimensionRepository, RequestDimensionRepository>();
        services.AddScoped<IRequestVasRepository, RequestVasRepository>();
        return services;
    }
}
