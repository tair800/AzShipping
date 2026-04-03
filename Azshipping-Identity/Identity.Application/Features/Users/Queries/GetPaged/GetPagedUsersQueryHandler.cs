using Identity.Application.DTOs.User;
using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.UserAggregate;
using Mapster;
using MediatR;
using MrStyx.Domain.SeedWork.Utils;

namespace Identity.Application.Features.Users.Queries.GetPaged;

public sealed class GetPagedUsersQueryHandler(IUserRepository userRepository, IUserDtoEnrichmentService enrichmentService)
    : IRequestHandler<GetPagedUsersQuery, PagedUserList>
{
    public async Task<PagedUserList> Handle(GetPagedUsersQuery request, CancellationToken cancellationToken)
    {
        var paged = await userRepository.GetPagedAsync(
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
