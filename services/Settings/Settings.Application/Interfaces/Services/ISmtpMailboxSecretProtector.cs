namespace Settings.Application.Interfaces.Services;

/// <summary>Protects SMTP passwords at rest (Data Protection in Infrastructure).</summary>
public interface ISmtpMailboxSecretProtector
{
    byte[]? Protect(string? plaintext);
    string? Unprotect(byte[]? protectedBytes);
}
