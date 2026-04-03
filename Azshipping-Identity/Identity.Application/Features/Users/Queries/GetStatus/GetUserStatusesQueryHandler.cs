using Identity.Domain.AggregatesModel.UserAggregate.Enumerations;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetStatus;

public sealed class GetUserStatusesQueryHandler : IRequestHandler<GetUserStatusesQuery, IReadOnlyCollection<UserStatus>>
{
    public Task<IReadOnlyCollection<UserStatus>> Handle(GetUserStatusesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(UserStatus.GetAll());
    }
}