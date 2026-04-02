using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Settings.Application.Interfaces.Services;

namespace Settings.Infrastructure.Services;

public sealed class SmtpMailboxSecretProtector(IDataProtectionProvider provider) : ISmtpMailboxSecretProtector
{
    private readonly IDataProtector _protector = provider.CreateProtector("Settings.EmailAccount.SmtpPassword.v1");

    public byte[]? Protect(string? plaintext)
    {
        if (string.IsNullOrEmpty(plaintext)) return null;
        return _protector.Protect(Encoding.UTF8.GetBytes(plaintext));
    }

    public string? Unprotect(byte[]? protectedBytes)
    {
        if (protectedBytes == null || protectedBytes.Length == 0) return null;
        var bytes = _protector.Unprotect(protectedBytes);
        return Encoding.UTF8.GetString(bytes);
    }
}
