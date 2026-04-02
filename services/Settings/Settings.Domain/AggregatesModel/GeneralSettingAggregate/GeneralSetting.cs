namespace Settings.Domain.AggregatesModel.GeneralSettingAggregate;

/// <summary>
/// General application settings. Singleton - one row.
/// Currency and Bank are stored as codes; data will be fetched from Central Bank API later.
/// </summary>
public class GeneralSetting
{
    public Guid Id { get; set; }
    /// <summary>Path or URL to company logo.</summary>
    public string? LogoPath { get; set; }
    /// <summary>Currency code from Central Bank API (e.g. AZN, USD).</summary>
    public string? CurrencyCode { get; set; }
    /// <summary>Date format pattern (e.g. dd/mm/yyyy).</summary>
    public string? DateFormat { get; set; }
    /// <summary>Price display: freight | freight+VAT | freight with vat.</summary>
    public string? PriceDisplayType { get; set; }
    /// <summary>Default language code.</summary>
    public string? DefaultLanguageCode { get; set; }
    /// <summary>Language for notifications.</summary>
    public string? NotificationLanguageCode { get; set; }
    /// <summary>Bank code from Central Bank API.</summary>
    public string? BankCode { get; set; }
    /// <summary>Timezone (e.g. UTC+04:00 Azerbaijan Time).</summary>
    public string? Timezone { get; set; }
    public bool UseCreditLimit { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
