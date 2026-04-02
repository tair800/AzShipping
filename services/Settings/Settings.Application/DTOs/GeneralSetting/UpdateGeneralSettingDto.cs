namespace Settings.Application.DTOs.GeneralSetting;

public record UpdateGeneralSettingDto(
    string? LogoPath = null,
    string? CurrencyCode = null,
    string? DateFormat = null,
    string? PriceDisplayType = null,
    string? DefaultLanguageCode = null,
    string? NotificationLanguageCode = null,
    string? BankCode = null,
    string? Timezone = null,
    bool? UseCreditLimit = null);
