using Settings.Application.DTOs.EmailSetting;
using Settings.Domain.AggregatesModel.EmailAccountAggregate;

namespace Settings.Application.Features.EmailSettings;

public static class EmailSettingMapper
{
    public static EmailSettingListItemDto ToListItem(EmailAccountSetting e) =>
        new(e.Id, e.AccountEmail, e.IdentityUserId, e.LinkedUserDisplayName, e.IsSystemEmail);

    public static EmailSettingDetailDto ToDetail(EmailAccountSetting e) =>
        new(
            e.Id,
            e.AccountEmail,
            e.UseSeparateAuthLogin,
            e.SmtpAuthUsername,
            e.ProtectedPassword is { Length: > 0 },
            e.WithoutPassword,
            e.ConnectionMode,
            e.SmtpHost,
            e.SmtpPort,
            e.SmtpSecurity,
            e.IsSystemEmail,
            e.IdentityUserId,
            e.LinkedUserDisplayName,
            e.CreatedAtUtc,
            e.UpdatedAtUtc);
}
