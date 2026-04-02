using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Settings.Domain.AggregatesModel.EmailAccountAggregate;

namespace Settings.Infrastructure.Services;

internal static class SmtpKitHelper
{
    internal static string GetAuthUser(EmailAccountSetting account) =>
        account.UseSeparateAuthLogin
            ? (string.IsNullOrWhiteSpace(account.SmtpAuthUsername) ? account.AccountEmail : account.SmtpAuthUsername.Trim())
            : account.AccountEmail.Trim();

    internal static SecureSocketOptions ParseSecurity(string? value)
    {
        var v = value?.Trim();
        if (string.IsNullOrEmpty(v)) return SecureSocketOptions.StartTls;
        if (v.Equals("Ssl", StringComparison.OrdinalIgnoreCase) || v.Equals("SSL", StringComparison.OrdinalIgnoreCase))
            return SecureSocketOptions.SslOnConnect;
        if (v.Equals("None", StringComparison.OrdinalIgnoreCase))
            return SecureSocketOptions.None;
        return SecureSocketOptions.StartTls;
    }

    internal static async Task SendMessageAsync(
        EmailAccountSetting account,
        string? passwordPlain,
        MimeMessage message,
        CancellationToken cancellationToken)
    {
        if (!account.WithoutPassword && string.IsNullOrEmpty(passwordPlain))
            throw new InvalidOperationException("No password stored for this mailbox. Enable \"Without password\" or set a password.");

        var security = ParseSecurity(account.SmtpSecurity);
        using var client = new SmtpClient();
        await client.ConnectAsync(account.SmtpHost.Trim(), account.SmtpPort, security, cancellationToken);

        if (!account.WithoutPassword)
            await client.AuthenticateAsync(GetAuthUser(account), passwordPlain!, cancellationToken);

        await client.SendAsync(message, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
    }
}
