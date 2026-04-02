using Accounting.Application.DTOs.Payment;
using Accounting.Domain.AggregatesModel.PaymentAggregate;

namespace Accounting.Application.Features.Payments;

public static class PaymentMapper
{
    public static PaymentDto MapToDto(Payment e) => new()
    {
        Id = e.Id,
        CreatedAtUtc = e.CreatedAtUtc,
        Direction = (int)e.Direction,
        CurrencyCode = e.CurrencyCode,
        PaidAmount = e.PaidAmount,
        PaymentMethod = (int)e.PaymentMethod,
        PaymentDate = e.PaymentDate,
        ReceivedBy = e.ReceivedBy,
        Notes = e.Notes,
        OrderNo = e.OrderNo,
        AccountLabel = e.AccountLabel,
        InvoiceReference = e.InvoiceReference,
        OperationInvoiceId = e.OperationInvoiceId,
        CounterpartyName = e.CounterpartyName,
    };
}
