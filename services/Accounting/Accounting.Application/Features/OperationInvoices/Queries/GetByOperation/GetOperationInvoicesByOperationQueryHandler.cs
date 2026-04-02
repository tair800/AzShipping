using Accounting.Application.DTOs.OperationInvoice;
using Accounting.Application.Features.OperationInvoices;
using Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;
using Accounting.Domain.AggregatesModel.PaymentAggregate;
using MediatR;

namespace Accounting.Application.Features.OperationInvoices.Queries.GetByOperation;

public class GetOperationInvoicesByOperationQueryHandler(
    IOperationInvoiceRepository invoiceRepo,
    IPaymentRepository paymentRepo)
    : IRequestHandler<GetOperationInvoicesByOperationQuery, IReadOnlyList<OperationInvoiceDto>>
{
    public async Task<IReadOnlyList<OperationInvoiceDto>> Handle(GetOperationInvoicesByOperationQuery request,
        CancellationToken cancellationToken)
    {
        if (request.OperationId == Guid.Empty)
            return [];

        var list = await invoiceRepo.GetByOperationIdAsync(request.OperationId, cancellationToken);
        var result = new List<OperationInvoiceDto>(list.Count);
        foreach (var inv in list)
        {
            var payments = await paymentRepo.GetByOperationInvoiceIdAsync(inv.Id, cancellationToken);
            var paid = OperationInvoicePaymentTotals.SumIncomingInInvoiceCurrency(payments, inv.CurrencyCode);
            result.Add(OperationInvoiceMapper.ToDto(inv, paid));
        }

        return result;
    }
}
