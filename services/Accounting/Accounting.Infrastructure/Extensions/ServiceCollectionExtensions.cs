using Accounting.Domain.AggregatesModel.InvoiceLookupAggregate;
using Accounting.Domain.AggregatesModel.OperationActAggregate;
using Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;
using Accounting.Domain.AggregatesModel.PaymentAggregate;
using Accounting.Domain.AggregatesModel.VatDefinitionAggregate;
using Accounting.Infrastructure.Persistence;
using Accounting.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is required.");
        services.AddDbContext<AccountingDbContext>(o => o.UseNpgsql(conn));
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IOperationInvoiceRepository, OperationInvoiceRepository>();
        services.AddScoped<IOperationActRepository, OperationActRepository>();
        services.AddScoped<IVatDefinitionRepository, VatDefinitionRepository>();
        services.AddScoped<IInvoiceLookupRepository, InvoiceLookupRepository>();
        return services;
    }
}
