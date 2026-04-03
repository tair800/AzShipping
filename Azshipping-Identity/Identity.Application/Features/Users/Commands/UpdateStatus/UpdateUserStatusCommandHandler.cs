using Identity.Application.DTOs.User;
using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.UserAggregate;
using Identity.Domain.AggregatesModel.UserAggregate.Enumerations;
using Mapster;
using MediatR;
using MrStyx.Application.Exceptions;
using MrStyx.Domain.SeedWork.Persistence;

namespace Identity.Application.Features.Users.Commands.UpdateStatus;

public sealed class UpdateUserStatusCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    ILicensingService licensingService,
    IUserDtoEnrichmentService enrichmentService) : IRequestHandler<UpdateUserStatusCommand, UserDto>
{
    public async Task<UserDto> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var newStatus = UserStatus.GetAll().FirstOrDefault(s =>
                            string.Equals(s.Name, dto.Status, StringComparison.OrdinalIgnoreCase))
                        ?? throw new BadRequestException(
                            $"Unknown status \"{dto.Status}\". Use one of: {string.Join(", ", UserStatus.GetAll().Select(s => s.Name))}.");

        var user = await userRepository.GetByIdAsync(dto.Id, cancellationToken, trackingMode: QueryTrackingMode.Tracking)
            ?? throw new NotFoundException($"Can't find user by id \"{dto.Id}\"");

        if (newStatus == UserStatus.Active && user.Status != UserStatus.Active)
            await licensingService.EnsureCanActivateAnotherUserAsync(cancellationToken);

        user.ApplyStatus(newStatus);

        // Admin / manual activation: same end state as email confirmation (user can log in; no stale token).
        if (newStatus == UserStatus.Active)
            user.ClearEmailConfirmationToken();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var updated = await userRepository.GetByIdAsync(user.Id, cancellationToken)
            ?? throw new NotFoundException($"Can't find user by id \"{dto.Id}\"");

        return await enrichmentService.EnrichAsync(updated.Adapt<UserDto>(), cancellationToken);
    }
}
