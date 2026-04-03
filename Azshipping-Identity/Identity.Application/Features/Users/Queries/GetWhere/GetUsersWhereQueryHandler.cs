using Identity.Application.DTOs.User;
using Identity.Application.Interfaces.Services;
using Identity.Application.SearchPredicates;
using Identity.Domain.AggregatesModel.UserAggregate;
using Mapster;
using MediatR;

namespace Identity.Application.Features.Users.Queries.GetWhere;

public sealed class GetUsersWhereQueryHandler(IUserRepository userRepository, IUserDtoEnrichmentService enrichmentService)
    : IRequestHandler<GetUsersWhereQuery, UserList>
{
    public async Task<UserList> Handle(GetUsersWhereQuery request, CancellationToken cancellationToken)
    {
        var dto = request.SearchUserDto;
        var predicate = SearchUserPredicate.BuildPredicate(dto);
        var users = await userRepository.GetWhereAsync(predicate, cancellationToken);
        var dtos = users.Adapt<IReadOnlyList<UserDto>>();
        var enriched = await enrichmentService.EnrichAsync(dtos, cancellationToken);
        return new UserList(enriched);
    }
}
