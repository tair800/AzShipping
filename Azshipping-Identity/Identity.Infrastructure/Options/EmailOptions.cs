namespace Identity.Infrastructure.Options;

public class EmailOptions
{
    public const string SectionName = "Email";

    public string Host { get; init; } = "localhost";
    public int Port { get; init; } = 25;
    public string? Username { get; init; }
    public string? Password { get; init; }
    public string From { get; init; } = "noreply@example.com";
    public string FromDisplayName { get; init; } = "Identity Service";
    public bool UseSsl { get; init; }
    public string BaseFrontUrl { get; init; } = "";
    public string BaseBackUrl { get; init; } = "";
    public int ConfirmationTokenLifeTimeHours { get; init; } = 24;
}