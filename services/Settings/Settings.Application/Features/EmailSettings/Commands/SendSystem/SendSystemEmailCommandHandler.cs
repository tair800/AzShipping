using MediatR;
using Settings.Application.Interfaces.Services;
using Settings.Domain.AggregatesModel.EmailAccountAggregate;

namespace Settings.Application.Features.EmailSettings.Commands.SendSystem;

public sealed class SendSystemEmailCommandHandler(
    IEmailAccountSettingRepository repository,
    ISmtpMailboxSecretProtector secretProtector,
    ISmtpMailboxMessageSender messageSender)
    : IRequestHandler<SendSystemEmailCommand, bool>
{
    public async Task<bool> Handle(SendSystemEmailCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        if (string.IsNullOrWhiteSpace(dto.To))
            throw new InvalidOperationException("Recipient (To) is required.");
        if (string.IsNullOrWhiteSpace(dto.Subject))
            throw new InvalidOperationException("Subject is required.");

        var account = await repository.GetFirstSystemMailboxAsync(cancellationToken);
        if (account == null)
            throw new InvalidOperationException(
                "No system email account is configured. In Settings, add an email row and enable \"Is system email\".");

        string? pwd = null;
        if (!account.WithoutPassword && account.ProtectedPassword is { Length: > 0 })
            pwd = secretProtector.Unprotect(account.ProtectedPassword);

        await messageSender.SendAsync(account, pwd, dto.To.Trim(), dto.Subject, dto.Body ?? "", dto.IsHtml, cancellationToken);
        return true;
    }
}
