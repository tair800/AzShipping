namespace Identity.Infrastructure.Options;

public sealed class GeneralClientOptions
{
    public const string SectionName = "General";

    /// <summary>General.API base URL (e.g. http://localhost:5068).</summary>
    public string BaseUrl { get; set; } = "http://localhost:5068";

    /// <summary>When false, skip HTTP calls (e.g. local Identity-only tests).</summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// When the create-user HTTP request has no <c>Authorization</c> header, mint a JWT as this Identity user (e.g. <c>admin</c>) to call General.API.
    /// Use for Swagger/local tools; leave empty in production and rely on the caller's Bearer token.
    /// </summary>
    public string? ProvisioningActAsUsername { get; set; }
}
