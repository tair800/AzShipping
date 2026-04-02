using System.Security.Cryptography;
using System.Text;

namespace Settings.API.Security;

public static class SystemEmailSendAuth
{
    public const string HeaderName = "X-AzShipping-System-Email-Key";

    public static bool IsAuthorized(string? configuredKey, string? providedKey)
    {
        if (string.IsNullOrEmpty(configuredKey) || providedKey == null)
            return false;
        var a = Encoding.UTF8.GetBytes(configuredKey);
        var b = Encoding.UTF8.GetBytes(providedKey);
        if (a.Length != b.Length)
            return false;
        return CryptographicOperations.FixedTimeEquals(a, b);
    }
}
