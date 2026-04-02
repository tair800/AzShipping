namespace Settings.Application.DTOs.EmailSetting;

public record CreateEmailSettingDto(
    string AccountEmail,
    bool UseSeparateAuthLogin,
    string? SmtpAuthUsername,
    string? Password,
    bool WithoutPassword,
    string ConnectionMode,
    string SmtpHost,
    int SmtpPort,
    string SmtpSecurity,
    bool IsSystemEmail,
    long? IdentityUserId,
    string? LinkedUserDisplayName);
