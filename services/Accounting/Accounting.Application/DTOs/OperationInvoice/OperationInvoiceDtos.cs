namespace Accounting.Application.DTOs.OperationInvoice;

/// <summary>Line payload aligned with Figma (Stock, Description, Until, QTY, Price, Disc%, totals, VAT%, Tax exemption).</summary>
public class OperationInvoiceLineDto
{
    public Guid Id { get; init; }
    public int SortOrder { get; init; }
    public string? StockCode { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime? ValidUntil { get; init; }
    public decimal Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal DiscountPercent { get; init; }
    public decimal VatPercent { get; init; }
    /// <summary>Ex-VAT line total after discount (mirrors LineNet for Figma "Line total").</summary>
    public decimal LineTotalExVat { get; init; }
    public decimal LineNet { get; init; }
    public decimal LineVat { get; init; }
    /// <summary>VAT-inclusive line total (Figma "Taxinc total"; mirrors LineGross).</summary>
    public decimal TaxInclusiveTotal { get; init; }
    public decimal LineGross { get; init; }
    public decimal TaxExemptionAmount { get; init; }
}

public class OperationInvoiceDto
{
    public Guid Id { get; init; }
    public Guid OperationId { get; init; }

    /// <summary>Display id e.g. #123454356.</summary>
    public string? PublicReference { get; init; }

    public string InvoiceNumber { get; init; } = string.Empty;
    public DateTime DocumentDate { get; init; }
    public TimeSpan? IssueTime { get; init; }
    public DateTime? DueDate { get; init; }
    public int? PostponedDays { get; init; }

    public string? InvoiceTypeCode { get; init; }
    public string? InvoiceAccountCode { get; init; }
    public string? ContractNumber { get; init; }
    public string? InvoiceAddress { get; init; }
    public string? InvoiceNote { get; init; }
    public string? ExpenseCenterCode { get; init; }
    public string? SpecialCode { get; init; }

    public string? ContractorName { get; init; }
    public string? PayerName { get; init; }
    public string? PricingTypeCode { get; init; }
    public string? BreakingRule { get; init; }
    public string? WarehouseCode { get; init; }

    public string? HeadCode { get; init; }
    public string? DepartmentCode { get; init; }
    public string? LanguageCode { get; init; }
    public string? TemplateCode { get; init; }

    public string CurrencyCode { get; init; } = "USD";
    public string? Notes { get; init; }
    public bool IsSent { get; init; }
    public decimal SubtotalExclVat { get; init; }
    public decimal VatTotal { get; init; }
    public decimal TotalInclVat { get; init; }
    public string? PaymentsBalanceCurrency { get; init; }
    public decimal? PaymentsBalanceAmount { get; init; }
    public DateTime CreatedAtUtc { get; init; }
    public DateTime? UpdatedAtUtc { get; init; }
    public decimal PaidAmountInInvoiceCurrency { get; init; }
    public decimal OutstandingInInvoiceCurrency { get; init; }

    public decimal HeaderLineTotal { get; init; }
    public decimal HeaderAdditions { get; init; }
    public decimal HeaderDiscount { get; init; }
    public decimal HeaderNetTotal { get; init; }
    public decimal HeaderTaxTotal { get; init; }
    public decimal HeaderTaxInclusiveTotal { get; init; }
    public decimal HeaderVatExemption { get; init; }
    public decimal HeaderStoppage { get; init; }
    public decimal HeaderRounding { get; init; }
    public decimal HeaderAmountInExchange { get; init; }
    public decimal HeaderGeneralTotal { get; init; }

    public IReadOnlyList<OperationInvoiceLineDto> Lines { get; init; } = [];
    public IReadOnlyList<OperationInvoiceDiscountLineDto> DiscountLines { get; init; } = [];
    public IReadOnlyList<OperationInvoiceTaxLineDto> TaxLines { get; init; } = [];
    public IReadOnlyList<OperationInvoiceNoteLineDto> NoteLines { get; init; } = [];
    public IReadOnlyList<OperationInvoicePaymentLineDto> PaymentLines { get; init; } = [];
}

/// <summary>Flat row for global invoice list (no line items).</summary>
public class OperationInvoiceListItemDto
{
    public Guid Id { get; init; }
    public Guid OperationId { get; init; }
    public string? PublicReference { get; init; }
    public string InvoiceNumber { get; init; } = string.Empty;
    public DateTime DocumentDate { get; init; }
    public string? PayerName { get; init; }
    public string? InvoiceTypeCode { get; init; }
    public string CurrencyCode { get; init; } = "USD";
    public decimal SubtotalExclVat { get; init; }
    public decimal VatTotal { get; init; }
    public decimal TotalInclVat { get; init; }
    public decimal PaidAmountInInvoiceCurrency { get; init; }
    public decimal OutstandingInInvoiceCurrency { get; init; }
    public bool IsSent { get; init; }
}

public class OperationInvoiceDiscountLineDto
{
    public Guid Id { get; init; }
    public int SortOrder { get; init; }
    public string? TypeCode { get; init; }
    public decimal Percent { get; init; }
    public decimal Amount { get; init; }
    public string? AllowanceChargeReason { get; init; }
}

public class OperationInvoiceTaxLineDto
{
    public Guid Id { get; init; }
    public int SortOrder { get; init; }
    public decimal TaxableAmount { get; init; }
    public string? TaxTypeCode { get; init; }
    public decimal TaxPercent { get; init; }
    public decimal TaxAmount { get; init; }
    public decimal FinalAmount { get; init; }
    public decimal ExemptAmount { get; init; }
    public decimal? Rounding { get; init; }
    public string? AccountCode { get; init; }
}

public class OperationInvoiceNoteLineDto
{
    public Guid Id { get; init; }
    public int SortOrder { get; init; }
    public string? CreatorDisplayName { get; init; }
    public string? NoteTypeCode { get; init; }
    public string NoteText { get; init; } = string.Empty;
}

public class OperationInvoicePaymentLineDto
{
    public Guid Id { get; init; }
    public int SortOrder { get; init; }
    public string? AppcardName { get; init; }
    public decimal? Amount { get; init; }
    public decimal? ConvertedAmount { get; init; }
    public string? CurrencyCode { get; init; }
    public decimal? CurrencyRate { get; init; }
    public string? PersonName { get; init; }
}

public class CreateOperationInvoiceLineDto
{
    public string? StockCode { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime? ValidUntil { get; init; }
    public decimal Quantity { get; init; } = 1;
    public decimal UnitPrice { get; init; }
    public decimal DiscountPercent { get; init; }
    public decimal VatPercent { get; init; }
    public decimal TaxExemptionAmount { get; init; }
}

public class CreateOperationInvoiceDto
{
    public Guid OperationId { get; init; }
    public string? PublicReference { get; init; }
    public string InvoiceNumber { get; init; } = string.Empty;
    public DateTime DocumentDate { get; init; }
    /// <summary>Wall-clock time on document date (serialized as time span or ISO time in JSON).</summary>
    public TimeSpan? IssueTime { get; init; }
    public DateTime? DueDate { get; init; }
    public int? PostponedDays { get; init; }

    public string? InvoiceTypeCode { get; init; }
    public string? InvoiceAccountCode { get; init; }
    public string? ContractNumber { get; init; }
    public string? InvoiceAddress { get; init; }
    public string? InvoiceNote { get; init; }
    public string? ExpenseCenterCode { get; init; }
    public string? SpecialCode { get; init; }

    public string? ContractorName { get; init; }
    public string? PayerName { get; init; }
    public string? PricingTypeCode { get; init; }
    public string? BreakingRule { get; init; }
    public string? WarehouseCode { get; init; }

    public string? HeadCode { get; init; }
    public string? DepartmentCode { get; init; }
    public string? LanguageCode { get; init; }
    public string? TemplateCode { get; init; }

    public string CurrencyCode { get; init; } = "USD";
    public string? Notes { get; init; }
    public bool IsSent { get; init; }
    public string? PaymentsBalanceCurrency { get; init; }
    public decimal? PaymentsBalanceAmount { get; init; }

    public decimal HeaderLineTotal { get; init; }
    public decimal HeaderAdditions { get; init; }
    public decimal HeaderDiscount { get; init; }
    public decimal HeaderNetTotal { get; init; }
    public decimal HeaderTaxTotal { get; init; }
    public decimal HeaderTaxInclusiveTotal { get; init; }
    public decimal HeaderVatExemption { get; init; }
    public decimal HeaderStoppage { get; init; }
    public decimal HeaderRounding { get; init; }
    public decimal HeaderAmountInExchange { get; init; }
    public decimal HeaderGeneralTotal { get; init; }

    public IReadOnlyList<CreateOperationInvoiceLineDto> Lines { get; init; } = [];
    public IReadOnlyList<CreateOperationInvoiceDiscountLineDto> DiscountLines { get; init; } = [];
    public IReadOnlyList<CreateOperationInvoiceTaxLineDto> TaxLines { get; init; } = [];
    public IReadOnlyList<CreateOperationInvoiceNoteLineDto> NoteLines { get; init; } = [];
    public IReadOnlyList<CreateOperationInvoicePaymentLineDto> PaymentLines { get; init; } = [];
}

public class CreateOperationInvoiceDiscountLineDto
{
    public string? TypeCode { get; init; }
    public decimal Percent { get; init; }
    public decimal Amount { get; init; }
    public string? AllowanceChargeReason { get; init; }
}

public class CreateOperationInvoiceTaxLineDto
{
    public decimal TaxableAmount { get; init; }
    public string? TaxTypeCode { get; init; }
    public decimal TaxPercent { get; init; }
    public decimal TaxAmount { get; init; }
    public decimal FinalAmount { get; init; }
    public decimal ExemptAmount { get; init; }
    public decimal? Rounding { get; init; }
    public string? AccountCode { get; init; }
}

public class CreateOperationInvoiceNoteLineDto
{
    public string? CreatorDisplayName { get; init; }
    public string? NoteTypeCode { get; init; }
    public string NoteText { get; init; } = string.Empty;
}

public class CreateOperationInvoicePaymentLineDto
{
    public string? AppcardName { get; init; }
    public decimal? Amount { get; init; }
    public decimal? ConvertedAmount { get; init; }
    public string? CurrencyCode { get; init; }
    public decimal? CurrencyRate { get; init; }
    public string? PersonName { get; init; }
}

public class UpdateOperationInvoiceDto : CreateOperationInvoiceDto
{
}

public class CalculateOperationInvoiceRequestDto
{
    public IReadOnlyList<CreateOperationInvoiceLineDto> Lines { get; init; } = [];
}

public class CalculateOperationInvoiceResponseDto
{
    public decimal SubtotalExclVat { get; init; }
    public decimal VatTotal { get; init; }
    public decimal TotalInclVat { get; init; }
    public IReadOnlyList<CalculatedLineDto> Lines { get; init; } = [];
}

public class CalculatedLineDto
{
    public int SortOrder { get; init; }
    public string? StockCode { get; init; }
    public string Description { get; init; } = string.Empty;
    public DateTime? ValidUntil { get; init; }
    public decimal Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal DiscountPercent { get; init; }
    public decimal VatPercent { get; init; }
    public decimal TaxExemptionAmount { get; init; }
    public decimal LineTotalExVat { get; init; }
    public decimal LineNet { get; init; }
    public decimal LineVat { get; init; }
    public decimal TaxInclusiveTotal { get; init; }
    public decimal LineGross { get; init; }
}
