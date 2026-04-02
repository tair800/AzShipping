namespace Accounting.Application.DTOs.Payment;

public record CreatePaymentDto
{
    /// <summary>0=Incoming (client paid you), 1=Outgoing (payments made to carrier/vendor).</summary>
    public int Direction { get; init; }

    /// <summary>ISO 4217 currency code (e.g. USD).</summary>
    public string CurrencyCode { get; init; } = string.Empty;

    public decimal PaidAmount { get; init; }

    /// <summary>Maps to domain enum PaymentMethod: 0=Cash, 1=NonCash, 2=BankTransfer, 3=Card.</summary>
    public int PaymentMethod { get; init; }

    public DateTime? PaymentDate { get; init; }

    public string? ReceivedBy { get; init; }

    public string? Notes { get; init; }

    public string? OrderNo { get; init; }
    public string? AccountLabel { get; init; }
    public string? InvoiceReference { get; init; }

    /// <summary>Optional link to an operation invoice (client receipt against AR invoice).</summary>
    public Guid? OperationInvoiceId { get; init; }

    public string? CounterpartyName { get; init; }
}
