using Accounting.Application.DTOs.OperationInvoice;
using Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;
using Accounting.Domain.AggregatesModel.PaymentAggregate;
using MediatR;

namespace Accounting.Application.Features.OperationInvoices.Queries.GetAllForList;

public sealed class GetAllOperationInvoicesForListQueryHandler(
    IOperationInvoiceRepository invoiceRepo,
    IPaymentRepository paymentRepo)
    : IRequestHandler<GetAllOperationInvoicesForListQuery, IReadOnlyList<OperationInvoiceListItemDto>>
{
    public async Task<IReadOnlyList<OperationInvoiceListItemDto>> Handle(GetAllOperationInvoicesForListQuery request,
        CancellationToken cancellationToken)
    {
        var list = await invoiceRepo.GetAllForListAsync(cancellationToken);
        var result = new List<OperationInvoiceListItemDto>(list.Count);
        foreach (var inv in list)
        {
            var payments = await paymentRepo.GetByOperationInvoiceIdAsync(inv.Id, cancellationToken);
            var paid = OperationInvoicePaymentTotals.SumIncomingInInvoiceCurrency(payments, inv.CurrencyCode);
            result.Add(OperationInvoiceMapper.ToListItemDto(inv, paid));
        }

        return result;
    }
}
