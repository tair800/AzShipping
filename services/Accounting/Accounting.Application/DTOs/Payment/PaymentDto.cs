namespace Accounting.Application.DTOs.Payment;

public record PaymentDto
{
    public Guid Id { get; init; }
    public DateTime CreatedAtUtc { get; init; }
    public int Direction { get; init; }
    public string CurrencyCode { get; init; } = string.Empty;
    public decimal PaidAmount { get; init; }
    public int PaymentMethod { get; init; }
    public DateTime? PaymentDate { get; init; }
    public string? ReceivedBy { get; init; }
    public string? Notes { get; init; }
    public string? OrderNo { get; init; }
    public string? AccountLabel { get; init; }
    public string? InvoiceReference { get; init; }
    public Guid? OperationInvoiceId { get; init; }
    public string? CounterpartyName { get; init; }
}
