namespace Settings.Application.DTOs.EmailSetting;

/// <summary>Partial update: bind an email-settings row to an Identity user (or clear with <c>null</c> user id).</summary>
public record LinkIdentityUserToEmailSettingDto(long? IdentityUserId, string? LinkedUserDisplayName);
