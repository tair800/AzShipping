namespace Settings.Application.DTOs.GeneralSetting;

/// <summary>
/// Price display type options for general settings.
/// </summary>
public static class PriceDisplayTypeOptions
{
    public const string Freight = "freight";
    public const string FreightPlusVat = "freight+VAT";
    public const string FreightWithVat = "freight with vat";

    public static readonly string[] All = [Freight, FreightPlusVat, FreightWithVat];
}
