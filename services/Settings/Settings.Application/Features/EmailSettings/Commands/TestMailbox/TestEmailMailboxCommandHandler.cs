using MediatR;
using Settings.Application.Interfaces.Services;
using Settings.Domain.AggregatesModel.EmailAccountAggregate;

namespace Settings.Application.Features.EmailSettings.Commands.TestMailbox;

public sealed class TestEmailMailboxCommandHandler(
    IEmailAccountSettingRepository repository,
    ISmtpMailboxSecretProtector secretProtector,
    ISmtpMailboxTester smtpTester)
    : IRequestHandler<TestEmailMailboxCommand, bool>
{
    public async Task<bool> Handle(TestEmailMailboxCommand request, CancellationToken cancellationToken)
    {
        var e = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (e == null)
            throw new InvalidOperationException("Email setting not found.");

        string? pwd = null;
        if (!e.WithoutPassword && e.ProtectedPassword is { Length: > 0 })
            pwd = secretProtector.Unprotect(e.ProtectedPassword);

        var to = string.IsNullOrWhiteSpace(request.Dto.ToEmail) ? e.AccountEmail : request.Dto.ToEmail!.Trim();
        await smtpTester.SendTestAsync(e, pwd, to, cancellationToken);
        return true;
    }
}
