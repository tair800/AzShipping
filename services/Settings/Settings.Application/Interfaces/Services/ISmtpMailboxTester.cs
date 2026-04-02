using Settings.Domain.AggregatesModel.EmailAccountAggregate;

namespace Settings.Application.Interfaces.Services;

public interface ISmtpMailboxTester
{
    /// <summary>Sends a short test message. <paramref name="passwordPlain"/> ignored when <see cref="EmailAccountSetting.WithoutPassword"/>.</summary>
    Task SendTestAsync(EmailAccountSetting account, string? passwordPlain, string toEmail, CancellationToken cancellationToken = default);
}
