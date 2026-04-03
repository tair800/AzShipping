using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.UserAggregate;
using Identity.Domain.AggregatesModel.UserAggregate.ValueObjects;
using MediatR;
using MrStyx.Application.Exceptions;
using MrStyx.Domain.SeedWork.Persistence;

namespace Identity.Application.Features.Auth.Commands.ResetPassword;

public sealed class ResetPasswordCommandHandler(IUserRepository userRepository, IPasswordService passwordService, IUnitOfWork unitOfWork) : IRequestHandler<ResetPasswordCommand, Unit>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetFirstOrDefaultAsync
            (
                u => u.PasswordResetToken == request.Token &&
                     u.PasswordResetTokenExpiresAt != null &&
                     u.PasswordResetTokenExpiresAt > DateTime.UtcNow,
                cancellationToken,
                trackingMode: QueryTrackingMode.Tracking
            ) ?? throw new BadRequestException("Invalid or expired confirmation token");

        var passwordHashVO = PasswordHash.Create(_passwordService.HashPassword(request.NewPassword));

        user.ChangePassword(passwordHashVO);
        user.ClearPasswordResetToken();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}