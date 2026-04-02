namespace Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;

/// <summary>Figma Discount tab row (allowance / charge).</summary>
public class OperationInvoiceDiscountLine
{
    public Guid Id { get; set; }
    public Guid OperationInvoiceId { get; set; }
    public OperationInvoice OperationInvoice { get; set; } = null!;

    public int SortOrder { get; set; }
    public string? TypeCode { get; set; }
    public decimal Percent { get; set; }
    public decimal Amount { get; set; }
    public string? AllowanceChargeReason { get; set; }
}
