using Identity.Domain.AggregatesModel.UserAggregate;
using MediatR;
using MrStyx.Application.Exceptions;
using MrStyx.Domain.SeedWork.Persistence;

namespace Identity.Application.Features.Users.Commands.Delete;

public sealed class DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken, trackingMode: QueryTrackingMode.Tracking)
            ?? throw new NotFoundException($"Can't find user by id \"{request.Id}\"");

        user.MarkDeleted();

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
