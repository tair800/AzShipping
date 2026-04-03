using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.UserAggregate;
using MediatR;
using MrStyx.Domain.SeedWork.Persistence;

namespace Identity.Application.Features.Auth.Commands.ForgotPassword;

public sealed class ForgotPasswordCommandHandler(IUserRepository userRepository, IEmailService emailService, IUnitOfWork unitOfWork) : IRequestHandler<ForgotPasswordCommand, Unit>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEmailService _emailService = emailService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetFirstOrDefaultAsync(u => u.Email.Value == request.Email, cancellationToken, trackingMode: QueryTrackingMode.Tracking);

        if (user is not null)
        {
            var (token, expiresAt) = _emailService.GenerateConfirmationToken();

            user.SetPasswordResetToken(token, expiresAt);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var confirmLink = _emailService.GetPasswordResetLink(token);
            var body = $@"
            <p>Здравствуйте!</p>
            <p>Подтвердите сброс пароля, перейдя по ссылке:</p>
            <p><a href=""{confirmLink}"">Сбросить пароль</a></p>
            <p>Если вы не запрашивали сброс, проигнорируйте это письмо.</p>
            ";

            await _emailService.SendAsync(user.Email.Value, "Сброс пароля", body, cancellationToken);
        }

        return Unit.Value;
    }
}