namespace Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;

/// <summary>Figma Payments tab (planned/split lines on invoice; use Payments API to post actual receipts).</summary>
public class OperationInvoicePaymentLine
{
    public Guid Id { get; set; }
    public Guid OperationInvoiceId { get; set; }
    public OperationInvoice OperationInvoice { get; set; } = null!;

    public int SortOrder { get; set; }
    public string? AppcardName { get; set; }
    public decimal? Amount { get; set; }
    public decimal? ConvertedAmount { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal? CurrencyRate { get; set; }
    public string? PersonName { get; set; }
}
