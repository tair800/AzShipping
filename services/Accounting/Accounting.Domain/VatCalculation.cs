namespace Accounting.Domain;

/// <summary>
/// VAT amounts from a net (excluding VAT) base. Percent is the statutory rate (e.g. 18 for 18%).
/// </summary>
public static class VatCalculation
{
    public static decimal VatAmountFromNet(decimal amountExcludingVat, decimal percent)
        => Math.Round(amountExcludingVat * (percent / 100m), 2, MidpointRounding.AwayFromZero);

    public static decimal GrossFromNet(decimal amountExcludingVat, decimal percent)
        => Math.Round(amountExcludingVat * (1 + percent / 100m), 2, MidpointRounding.AwayFromZero);

    /// <summary>Derive net from a gross (VAT-inclusive) amount.</summary>
    public static decimal NetFromGross(decimal amountIncludingVat, decimal percent)
    {
        if (percent <= -100m)
            throw new ArgumentOutOfRangeException(nameof(percent), "Percent must be greater than -100.");
        return Math.Round(amountIncludingVat / (1 + percent / 100m), 2, MidpointRounding.AwayFromZero);
    }

    /// <summary>VAT portion of a gross amount (gross − net).</summary>
    public static decimal VatAmountFromGross(decimal amountIncludingVat, decimal percent)
    {
        var net = NetFromGross(amountIncludingVat, percent);
        return Math.Round(amountIncludingVat - net, 2, MidpointRounding.AwayFromZero);
    }

    /// <summary>Net, VAT, and gross with VAT rounded from net (consistent with price proposal logic).</summary>
    public static (decimal Net, decimal Vat, decimal Gross) SplitFromNet(decimal amountExcludingVat, decimal percent)
    {
        var vat = VatAmountFromNet(amountExcludingVat, percent);
        var gross = Math.Round(amountExcludingVat + vat, 2, MidpointRounding.AwayFromZero);
        return (amountExcludingVat, vat, gross);
    }
}
