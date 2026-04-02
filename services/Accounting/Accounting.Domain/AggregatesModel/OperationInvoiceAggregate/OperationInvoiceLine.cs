namespace Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;

/// <summary>Single invoice line: net = QTY × Price × (1 − Disc%); VAT and gross via VatCalculation.</summary>
public class OperationInvoiceLine
{
    public Guid Id { get; set; }
    public Guid OperationInvoiceId { get; set; }
    public OperationInvoice OperationInvoice { get; set; } = null!;

    public int SortOrder { get; set; }

    /// <summary>Figma: Stock (SKU / item code).</summary>
    public string? StockCode { get; set; }

    public string Description { get; set; } = string.Empty;

    /// <summary>Figma: Until (e.g. service / price validity end).</summary>
    public DateTime? ValidUntil { get; set; }

    public decimal Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }

    /// <summary>Percent 0–100 applied to (Qty × Unit price) to produce net ex-VAT base.</summary>
    public decimal DiscountPercent { get; set; }

    /// <summary>VAT rate percent (e.g. 18).</summary>
    public decimal VatPercent { get; set; }

    /// <summary>
    /// Figma: Tax exemption amount for the line (stored for reporting;
    /// core VAT still uses full line net unless product rules extend the calculator).
    /// </summary>
    public decimal TaxExemptionAmount { get; set; }

    /// <summary>Line total ex VAT (Figma: Line total).</summary>
    public decimal LineNet { get; set; }

    public decimal LineVat { get; set; }

    /// <summary>Tax-inclusive line total (Figma: Taxinc total).</summary>
    public decimal LineGross { get; set; }
}
