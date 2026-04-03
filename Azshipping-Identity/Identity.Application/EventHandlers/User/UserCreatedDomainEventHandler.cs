using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.UserAggregate;
using Identity.Domain.AggregatesModel.UserAggregate.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using MrStyx.Application.EventHandlers;
using MrStyx.Domain.SeedWork.Persistence;

namespace Identity.Application.EventHandlers.User;

public sealed class UserCreatedDomainEventHandler
(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    ILogger<UserCreatedDomainEventHandler> logger

) : INotificationHandler<DomainEventNotification<UserCreatedDomainEvent>>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEmailService _emailService = emailService;
    private readonly ILogger<UserCreatedDomainEventHandler> _logger = logger;

    public async Task Handle(DomainEventNotification<UserCreatedDomainEvent> notification, CancellationToken cancellationToken)
    {
        var ev = notification.DomainEvent;


        var user = await _userRepository.GetFirstOrDefaultAsync(u => u.Email.Value == ev.Email, cancellationToken, trackingMode: QueryTrackingMode.Tracking);
        if (user is null)
        {
            _logger.LogWarning("UserCreatedDomainEvent: user not found by email {Email}", ev.Email);
            return;
        }

        var (token, expiresAt) = _emailService.GenerateConfirmationToken();

        user.SetEmailConfirmationToken(token, expiresAt);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var confirmLink = _emailService.GetConfirmationLink(token);
        var isDevelopment = string.Equals(
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            "Development",
            StringComparison.OrdinalIgnoreCase);

        if (isDevelopment)
        {
            _logger.LogInformation("Development: open this URL in a browser to confirm {Email}: {ConfirmationUrl}", ev.Email, confirmLink);
        }

        var body = $@"
            <p>Здравствуйте!</p>
            <p>Подтвердите регистрацию, перейдя по ссылке:</p>
            <p><a href=""{confirmLink}"">Подтвердить email</a></p>
            <p>Если вы не регистрировались, проигнорируйте это письмо.</p>
        ";

        try
        {
            await _emailService.SendAsync(user.Email.Value, "Подтверждение регистрации", body, cancellationToken);
            _logger.LogInformation("Confirmation email sent to {Email}", ev.Email);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(
                ex,
                "User created and confirmation token saved, but sending email to {Email} failed (check SMTP / Gmail app password / relay config). In Development, use logged confirmation URL.",
                user.Email.Value
            );
        }
    }
}