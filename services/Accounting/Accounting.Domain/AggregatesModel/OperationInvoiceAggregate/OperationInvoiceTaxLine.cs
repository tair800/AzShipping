namespace Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;

/// <summary>Figma Taxes tab row (taxable base, type, amounts).</summary>
public class OperationInvoiceTaxLine
{
    public Guid Id { get; set; }
    public Guid OperationInvoiceId { get; set; }
    public OperationInvoice OperationInvoice { get; set; } = null!;

    public int SortOrder { get; set; }
    public decimal TaxableAmount { get; set; }
    /// <summary>Usually VAT definition selling account code or symbolic code from UI.</summary>
    public string? TaxTypeCode { get; set; }
    public decimal TaxPercent { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal FinalAmount { get; set; }
    public decimal ExemptAmount { get; set; }
    public decimal? Rounding { get; set; }
    public string? AccountCode { get; set; }
}
