using MimeKit;
using MimeKit.Text;
using Settings.Application.Interfaces.Services;
using Settings.Domain.AggregatesModel.EmailAccountAggregate;

namespace Settings.Infrastructure.Services;

public sealed class MailboxSmtpMessageSender : ISmtpMailboxMessageSender
{
    public Task SendAsync(
        EmailAccountSetting account,
        string? passwordPlain,
        string to,
        string subject,
        string body,
        bool isHtml,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(to))
            throw new InvalidOperationException("Recipient email is required.");

        var msg = new MimeMessage();
        msg.From.Add(MailboxAddress.Parse(account.AccountEmail.Trim()));
        msg.To.Add(MailboxAddress.Parse(to.Trim()));
        msg.Subject = subject;
        msg.Body = new TextPart(isHtml ? TextFormat.Html : TextFormat.Plain) { Text = body };

        return SmtpKitHelper.SendMessageAsync(account, passwordPlain, msg, cancellationToken);
    }
}
