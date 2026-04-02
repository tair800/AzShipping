namespace Settings.Application.DTOs.GeneralSetting;

public record GeneralSettingDto(
    Guid Id,
    string? LogoPath,
    string? CurrencyCode,
    string? DateFormat,
    string? PriceDisplayType,
    string? DefaultLanguageCode,
    string? NotificationLanguageCode,
    string? BankCode,
    string? Timezone,
    bool UseCreditLimit,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
