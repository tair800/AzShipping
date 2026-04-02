namespace Accounting.Domain.AggregatesModel.PaymentAggregate;

public class Payment
{
    public Guid Id { get; set; }
    public DateTime CreatedAtUtc { get; set; }

    /// <summary>Incoming = client receipt; Outgoing = payment made to carrier/vendor (AP).</summary>
    public PaymentDirection Direction { get; set; }

    /// <summary>ISO 4217 code (e.g. USD).</summary>
    public string CurrencyCode { get; set; } = string.Empty;

    public decimal PaidAmount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public DateTime? PaymentDate { get; set; }

    /// <summary>Person who received or recorded the payment (UI: "Received the payment" / "Accepted payment").</summary>
    public string? ReceivedBy { get; set; }

    public string? Notes { get; set; }

    /// <summary>Shipping / operation order reference (e.g. AZ4523546).</summary>
    public string? OrderNo { get; set; }

    /// <summary>Cash / bank account label for display (which account the payment used).</summary>
    public string? AccountLabel { get; set; }

    /// <summary>Vendor invoice or cost document reference.</summary>
    public string? InvoiceReference { get; set; }

    /// <summary>When set, allocates this payment to an operation-issued invoice (usually incoming client receipt).</summary>
    public Guid? OperationInvoiceId { get; set; }

    /// <summary>Carrier or client name for list display (until linked entity ids exist).</summary>
    public string? CounterpartyName { get; set; }
}
