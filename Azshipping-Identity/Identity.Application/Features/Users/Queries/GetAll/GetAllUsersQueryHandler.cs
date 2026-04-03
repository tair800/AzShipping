using Identity.Application.DTOs.User;
using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.UserAggregate;
using Mapster;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetAll;

public sealed class GetAllUsersQueryHandler(IUserRepository userRepository, IUserDtoEnrichmentService enrichmentService)
    : IRequestHandler<GetAllUsersQuery, UserList>
{
    public async Task<UserList> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllAsync(cancellationToken);
        var dtos = users.Adapt<IReadOnlyList<UserDto>>();
        var enriched = await enrichmentService.EnrichAsync(dtos, cancellationToken);
        return new UserList(enriched);
    }
}
