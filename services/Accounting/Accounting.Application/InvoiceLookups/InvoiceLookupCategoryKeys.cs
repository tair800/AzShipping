using Accounting.Domain.AggregatesModel.InvoiceLookupAggregate;

namespace Accounting.Application.InvoiceLookups;

public static class InvoiceLookupCategoryKeys
{
    private static readonly Dictionary<string, InvoiceLookupCategory> Map =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["invoiceType"] = InvoiceLookupCategory.InvoiceType,
            ["invoiceAccount"] = InvoiceLookupCategory.InvoiceAccount,
            ["contractNumber"] = InvoiceLookupCategory.ContractNumber,
            ["expenseCenter"] = InvoiceLookupCategory.ExpenseCenter,
            ["specialCode"] = InvoiceLookupCategory.SpecialCode,
            ["contractor"] = InvoiceLookupCategory.Contractor,
            ["payer"] = InvoiceLookupCategory.Payer,
            ["pricingType"] = InvoiceLookupCategory.PricingType,
            ["warehouse"] = InvoiceLookupCategory.Warehouse,
            ["head"] = InvoiceLookupCategory.Head,
            ["department"] = InvoiceLookupCategory.Department,
            ["language"] = InvoiceLookupCategory.Language,
            ["template"] = InvoiceLookupCategory.Template,
            ["invoiceDiscountType"] = InvoiceLookupCategory.InvoiceDiscountType,
            ["invoiceNoteType"] = InvoiceLookupCategory.InvoiceNoteType,
            ["invoicePaymentAppcard"] = InvoiceLookupCategory.InvoicePaymentAppcard,
        };

    private static readonly HashSet<InvoiceLookupCategory> UserCreatable =
    [
        InvoiceLookupCategory.ExpenseCenter,
        InvoiceLookupCategory.SpecialCode
    ];

    /// <summary>These lists are loaded from Settings.API (or static UI data), not from Accounting Db.</summary>
    public static bool IsMergedFromSettings(InvoiceLookupCategory c) => c switch
    {
        InvoiceLookupCategory.PricingType or
            InvoiceLookupCategory.Warehouse or
            InvoiceLookupCategory.Head or
            InvoiceLookupCategory.Department or
            InvoiceLookupCategory.Language or
            InvoiceLookupCategory.Template => true,
        _ => false
    };

    public static bool TryParseApiKey(string? key, out InvoiceLookupCategory category)
    {
        category = default;
        if (string.IsNullOrWhiteSpace(key)) return false;
        return Map.TryGetValue(key.Trim(), out category);
    }

    public static string ToApiKey(InvoiceLookupCategory c) => c switch
    {
        InvoiceLookupCategory.InvoiceType => "invoiceType",
        InvoiceLookupCategory.InvoiceAccount => "invoiceAccount",
        InvoiceLookupCategory.ContractNumber => "contractNumber",
        InvoiceLookupCategory.ExpenseCenter => "expenseCenter",
        InvoiceLookupCategory.SpecialCode => "specialCode",
        InvoiceLookupCategory.Contractor => "contractor",
        InvoiceLookupCategory.Payer => "payer",
        InvoiceLookupCategory.PricingType => "pricingType",
        InvoiceLookupCategory.Warehouse => "warehouse",
        InvoiceLookupCategory.Head => "head",
        InvoiceLookupCategory.Department => "department",
        InvoiceLookupCategory.Language => "language",
        InvoiceLookupCategory.Template => "template",
        InvoiceLookupCategory.InvoiceDiscountType => "invoiceDiscountType",
        InvoiceLookupCategory.InvoiceNoteType => "invoiceNoteType",
        InvoiceLookupCategory.InvoicePaymentAppcard => "invoicePaymentAppcard",
        _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
    };

    public static bool IsUserCreatable(InvoiceLookupCategory c) => UserCreatable.Contains(c);
}
