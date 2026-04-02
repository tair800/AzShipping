using Accounting.Application.DTOs.OperationInvoice;
using Accounting.Application.Features.OperationInvoices;
using Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;
using Accounting.Domain.AggregatesModel.PaymentAggregate;
using MediatR;

namespace Accounting.Application.Features.OperationInvoices.Queries.GetById;

public class GetOperationInvoiceByIdQueryHandler(
    IOperationInvoiceRepository invoiceRepo,
    IPaymentRepository paymentRepo)
    : IRequestHandler<GetOperationInvoiceByIdQuery, OperationInvoiceDto?>
{
    public async Task<OperationInvoiceDto?> Handle(GetOperationInvoiceByIdQuery request,
        CancellationToken cancellationToken)
    {
        var inv = await invoiceRepo.GetByIdWithLinesAsync(request.Id, cancellationToken);
        if (inv == null)
            return null;

        var payments = await paymentRepo.GetByOperationInvoiceIdAsync(inv.Id, cancellationToken);
        var paid = OperationInvoicePaymentTotals.SumIncomingInInvoiceCurrency(payments, inv.CurrencyCode);
        return OperationInvoiceMapper.ToDto(inv, paid);
    }
}
