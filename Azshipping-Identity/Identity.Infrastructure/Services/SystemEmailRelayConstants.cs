namespace Identity.Infrastructure.Services;

internal static class SystemEmailRelayConstants
{
    /// <summary>Must match Settings.API header name for system email relay.</summary>
    internal const string HeaderName = "X-AzShipping-System-Email-Key";
}
