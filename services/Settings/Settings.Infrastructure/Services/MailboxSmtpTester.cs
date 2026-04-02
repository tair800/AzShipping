using MimeKit;
using Settings.Application.Interfaces.Services;
using Settings.Domain.AggregatesModel.EmailAccountAggregate;

namespace Settings.Infrastructure.Services;

public sealed class MailboxSmtpTester : ISmtpMailboxTester
{
    public async Task SendTestAsync(EmailAccountSetting account, string? passwordPlain, string toEmail, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(toEmail))
            throw new InvalidOperationException("Recipient email is required.");

        var msg = new MimeMessage();
        msg.From.Add(MailboxAddress.Parse(account.AccountEmail.Trim()));
        msg.To.Add(MailboxAddress.Parse(toEmail.Trim()));
        msg.Subject = "AzShipping — mailbox test";
        msg.Body = new TextPart("plain") { Text = "This is a test message from the Settings service (email account configuration)." };

        await SmtpKitHelper.SendMessageAsync(account, passwordPlain, msg, cancellationToken);
    }
}
