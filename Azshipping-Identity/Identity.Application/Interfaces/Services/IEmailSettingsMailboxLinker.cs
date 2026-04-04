namespace Identity.Application.Interfaces.Services;

/// <summary>After Identity user is created, optionally link a Settings email-settings row via <c>PATCH .../link-identity-user</c>.</summary>
public interface IEmailSettingsMailboxLinker
{
    /// <summary>Best-effort: logs warning on non-success; does not throw for typical HTTP errors.</summary>
    Task TryLinkMailboxAsync(Guid emailSettingId, long identityUserId, string? linkedUserDisplayName, CancellationToken cancellationToken = default);
}
