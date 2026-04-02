using Accounting.Domain.AggregatesModel.PaymentAggregate;

namespace Accounting.Application.Features.OperationInvoices;

public static class OperationInvoicePaymentTotals
{
    /// <summary>Sums incoming (client) payments allocated to the invoice, in the same currency as the invoice.</summary>
    public static decimal SumIncomingInInvoiceCurrency(IReadOnlyList<Payment> payments, string invoiceCurrency)
    {
        var c = (invoiceCurrency ?? string.Empty).Trim().ToUpperInvariant();
        decimal s = 0;
        foreach (var p in payments)
        {
            if (p.Direction != PaymentDirection.Incoming)
                continue;
            if (!string.Equals(p.CurrencyCode, c, StringComparison.OrdinalIgnoreCase))
                continue;
            s += p.PaidAmount;
        }

        return Math.Round(s, 2, MidpointRounding.AwayFromZero);
    }
}
