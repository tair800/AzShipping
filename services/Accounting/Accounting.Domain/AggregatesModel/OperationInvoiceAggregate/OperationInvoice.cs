namespace Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;

/// <summary>AR invoice tied to a logistics operation (Operation.API id). Amounts derived from lines.</summary>
public class OperationInvoice
{
    public Guid Id { get; set; }

    /// <summary>Operation.API entity id.</summary>
    public Guid OperationId { get; set; }

    /// <summary>UI / Figma display id (e.g. #123454356), optional second identifier alongside invoice number.</summary>
    public string? PublicReference { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public DateTime DocumentDate { get; set; }

    /// <summary>Time of issue on document date (Figma: Issue Hour).</summary>
    public TimeOnly? IssueTime { get; set; }

    public DateTime? DueDate { get; set; }

    /// <summary>Figma: Postponed days (payment deferral).</summary>
    public int? PostponedDays { get; set; }

    // --- Current information (Figma) ---
    public string? InvoiceTypeCode { get; set; }
    public string? InvoiceAccountCode { get; set; }
    public string? ContractNumber { get; set; }
    public string? InvoiceAddress { get; set; }
    /// <summary>Figma "Invoice note" (separate from free-text <see cref="Notes"/>).</summary>
    public string? InvoiceNote { get; set; }
    public string? ExpenseCenterCode { get; set; }
    public string? SpecialCode { get; set; }

    // --- Invoice information (Figma) ---
    public string? ContractorName { get; set; }
    public string? PayerName { get; set; }
    public string? PricingTypeCode { get; set; }
    public string? BreakingRule { get; set; }
    public string? WarehouseCode { get; set; }

    // --- General (Figma) ---
    /// <summary>Figma label "Hade" — head / responsible role code.</summary>
    public string? HeadCode { get; set; }
    public string? DepartmentCode { get; set; }
    public string? LanguageCode { get; set; }
    public string? TemplateCode { get; set; }

    public string CurrencyCode { get; set; } = "USD";

    /// <summary>Internal / legacy notes.</summary>
    public string? Notes { get; set; }

    public bool IsSent { get; set; }

    /// <summary>Sum of line nets (ex VAT).</summary>
    public decimal SubtotalExclVat { get; set; }

    public decimal VatTotal { get; set; }
    public decimal TotalInclVat { get; set; }

    // --- Figma summary panel (editable totals alongside line calculator) ---
    /// <summary>Sum of line nets / editable "Line total" mirror from Figma.</summary>
    public decimal HeaderLineTotal { get; set; }
    public decimal HeaderAdditions { get; set; }
    public decimal HeaderDiscount { get; set; }
    public decimal HeaderNetTotal { get; set; }
    public decimal HeaderTaxTotal { get; set; }
    public decimal HeaderTaxInclusiveTotal { get; set; }
    public decimal HeaderVatExemption { get; set; }
    public decimal HeaderStoppage { get; set; }
    public decimal HeaderRounding { get; set; }
    public decimal HeaderAmountInExchange { get; set; }
    public decimal HeaderGeneralTotal { get; set; }

    /// <summary>Optional display: balance in another currency (e.g. USD while invoice is AZN).</summary>
    public string? PaymentsBalanceCurrency { get; set; }
    public decimal? PaymentsBalanceAmount { get; set; }

    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }

    public ICollection<OperationInvoiceLine> Lines { get; set; } = new List<OperationInvoiceLine>();
    public ICollection<OperationInvoiceDiscountLine> DiscountLines { get; set; } = new List<OperationInvoiceDiscountLine>();
    public ICollection<OperationInvoiceTaxLine> TaxLines { get; set; } = new List<OperationInvoiceTaxLine>();
    public ICollection<OperationInvoiceNoteLine> NoteLines { get; set; } = new List<OperationInvoiceNoteLine>();
    public ICollection<OperationInvoicePaymentLine> PaymentLines { get; set; } = new List<OperationInvoicePaymentLine>();
}
