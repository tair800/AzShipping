using Settings.Domain.AggregatesModel.EmailAccountAggregate;

namespace Settings.Application.Interfaces.Services;

/// <summary>Sends an arbitrary message using a stored <see cref="EmailAccountSetting"/> (SMTP credentials stay in Settings).</summary>
public interface ISmtpMailboxMessageSender
{
    Task SendAsync(
        EmailAccountSetting account,
        string? passwordPlain,
        string to,
        string subject,
        string body,
        bool isHtml,
        CancellationToken cancellationToken = default);
}
