namespace Settings.API.Options;

/// <summary>Shared secret for server-to-server calls (Identity → Settings) to send mail using the configured system mailbox.</summary>
public sealed class EmailSystemSendOptions
{
    public const string SectionName = "EmailSystemSend";

    /// <summary>Must match Identity <c>Settings:SystemEmailSendApiKey</c>. If empty, <c>POST .../system/send</c> is disabled.</summary>
    public string? ApiKey { get; set; }
}
