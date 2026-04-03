using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.UserAggregate;
using MediatR;
using MrStyx.Application.Exceptions;
using MrStyx.Domain.SeedWork.Persistence;

namespace Identity.Application.Features.Auth.Commands.ConfirmEmail;

public sealed class ConfirmEmailCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILicensingService licensingService) : IRequestHandler<ConfirmEmailCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILicensingService _licensingService = licensingService;

    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Token))
            throw new BadRequestException("Confirmation token is required");

        var user = await _userRepository.GetFirstOrDefaultAsync
            (
                u => u.EmailConfirmationToken == request.Token &&
                     u.EmailConfirmationTokenExpiresAt != null &&
                     u.EmailConfirmationTokenExpiresAt > DateTime.UtcNow,
                cancellationToken,
                trackingMode: QueryTrackingMode.Tracking
            ) ?? throw new BadRequestException("Invalid or expired confirmation token");

        await _licensingService.EnsureCanActivateAnotherUserAsync(cancellationToken);

        user.Activate();
        user.ClearEmailConfirmationToken();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}