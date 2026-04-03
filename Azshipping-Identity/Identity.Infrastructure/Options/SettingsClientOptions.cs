namespace Identity.Infrastructure.Options;

public sealed class SettingsClientOptions
{
    public const string SectionName = "Settings";

    public string BaseUrl { get; set; } = "http://localhost:5064";

    /// <summary>When true and <see cref="SystemEmailSendApiKey"/> is set, <see cref="Application.Interfaces.Services.IEmailService.SendAsync"/> uses Settings (first <c>IsSystemEmail</c> mailbox) instead of local SMTP.</summary>
    public bool UseSystemEmailMailbox { get; set; }

    /// <summary>Same value as Settings.API <c>EmailSystemSend:ApiKey</c>. Sent as header <c>X-AzShipping-System-Email-Key</c>.</summary>
    public string? SystemEmailSendApiKey { get; set; }

    /// <summary>Same value as Settings.API <c>EmployeeGroupResolve:ApiKey</c>. Sent as header <c>X-AzShipping-Employee-Groups-Resolve-Key</c>.</summary>
    public string? EmployeeGroupResolveApiKey { get; set; }
}
