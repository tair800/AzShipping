namespace Settings.Application.DTOs.EmailSetting;

public record EmailSettingDetailDto(
    Guid Id,
    string AccountEmail,
    bool UseSeparateAuthLogin,
    string? SmtpAuthUsername,
    bool HasStoredPassword,
    bool WithoutPassword,
    string ConnectionMode,
    string SmtpHost,
    int SmtpPort,
    string SmtpSecurity,
    bool IsSystemEmail,
    long? IdentityUserId,
    string? LinkedUserDisplayName,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc);
