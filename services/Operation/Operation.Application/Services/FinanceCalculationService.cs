namespace Operation.Application.Services;

/// <summary>
/// VAT-inclusive freight/finance line math used by Operation.API. Line subtotal = amount × unit price; VAT = subtotal × (rate/100); total with VAT = subtotal + VAT.
/// Profit (when both sides present): ex-VAT = income subtotal − expense subtotal; incl. VAT = income total − expense total.
/// </summary>
public static class FinanceCalculationService
{
    private const decimal MaxVatPercent = 100m;

    public static FinanceLineCalculationResult CalculateLine(decimal? amount, decimal? unitPrice, decimal? vatRatePercent)
    {
        var a = amount ?? 0m;
        var p = unitPrice ?? 0m;
        var rate = ClampVatPercent(vatRatePercent ?? 0m);
        var subtotal = Math.Round(a * p, 2, MidpointRounding.AwayFromZero);
        var vat = Math.Round(subtotal * rate / 100m, 2, MidpointRounding.AwayFromZero);
        var withVat = Math.Round(subtotal + vat, 2, MidpointRounding.AwayFromZero);
        return new FinanceLineCalculationResult(subtotal, vat, withVat);
    }

    private static decimal ClampVatPercent(decimal v)
    {
        if (v < 0m) return 0m;
        return v > MaxVatPercent ? MaxVatPercent : v;
    }
}

public readonly record struct FinanceLineCalculationResult(decimal LineSubtotal, decimal VatAmount, decimal TotalWithVat);
