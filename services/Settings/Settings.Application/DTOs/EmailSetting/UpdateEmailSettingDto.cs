namespace Settings.Application.DTOs.EmailSetting;

public record UpdateEmailSettingDto(
    string AccountEmail,
    bool UseSeparateAuthLogin,
    string? SmtpAuthUsername,
    bool WithoutPassword,
    string ConnectionMode,
    string SmtpHost,
    int SmtpPort,
    string SmtpSecurity,
    bool IsSystemEmail,
    long? IdentityUserId,
    string? LinkedUserDisplayName,
    bool ChangePassword,
    string? Password);
