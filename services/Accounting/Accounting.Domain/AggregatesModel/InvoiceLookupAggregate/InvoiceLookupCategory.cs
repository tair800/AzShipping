namespace Accounting.Domain.AggregatesModel.InvoiceLookupAggregate;

/// <summary>Reference lists for operation invoice header (Figma dropdowns).</summary>
public enum InvoiceLookupCategory
{
    InvoiceType = 1,
    InvoiceAccount = 2,
    ContractNumber = 3,
    ExpenseCenter = 4,
    SpecialCode = 5,
    Contractor = 6,
    Payer = 7,
    PricingType = 8,
    Warehouse = 9,
    Head = 10,
    Department = 11,
    Language = 12,
    Template = 13,

    /// <summary>Discount tab "Type" (allowance / cash discount / …).</summary>
    InvoiceDiscountType = 14,

    /// <summary>Notes tab "Type".</summary>
    InvoiceNoteType = 15,

    /// <summary>Payments tab appcard / terminal name.</summary>
    InvoicePaymentAppcard = 16
}
