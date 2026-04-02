namespace Settings.Application.DTOs.EmailSetting;

public record EmailSettingListItemDto(
    Guid Id,
    string AccountEmail,
    long? IdentityUserId,
    string? LinkedUserDisplayName,
    bool IsSystemEmail);
