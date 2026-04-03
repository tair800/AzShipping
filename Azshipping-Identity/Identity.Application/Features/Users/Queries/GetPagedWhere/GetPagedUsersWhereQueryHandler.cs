using Identity.Application.DTOs.User;
using Identity.Application.Interfaces.Services;
using Identity.Application.SearchPredicates;
using Identity.Domain.AggregatesModel.UserAggregate;
using Mapster;
using MediatR;
using MrStyx.Domain.SeedWork.Utils;

namespace Identity.Application.Features.Users.Queries.GetPagedWhere;

public sealed class GetPagedUsersWhereQueryHandler(IUserRepository userRepository, IUserDtoEnrichmentService enrichmentService)
    : IRequestHandler<GetPagedUsersWhereQuery, PagedUserList>
{
    public async Task<PagedUserList> Handle(GetPagedUsersWhereQuery request, CancellationToken cancellationToken)
    {
        var predicate = SearchUserPredicate.BuildPredicate(request.SearchUserDto);
        var paged = await userRepository.GetPagedWhereAsync(
            predicate: predicate,
            pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            configure: null,
            cancellationToken: cancellationToken);

        var dtoPaged = paged.Adapt<PaginationResult<UserDto>>();
        var enriched = await enrichmentService.EnrichAsync(dtoPaged.Items, cancellationToken);

        return new PagedUserList(
            Items: new UserList(enriched),
            Meta: PaginationMetaFactory.From(dtoPaged));
    }
}
