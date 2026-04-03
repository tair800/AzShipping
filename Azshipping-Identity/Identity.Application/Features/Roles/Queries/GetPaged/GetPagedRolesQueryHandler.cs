using Identity.Application.DTOs.Role;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Mapster;
using MediatR;
using MrStyx.Domain.SeedWork.Utils;

namespace Identity.Application.Features.Roles.Queries.GetPaged;

public sealed class GetPagedRolesQueryHandler(IRoleRepository roleRepository) : IRequestHandler<GetPagedRolesQuery, PagedRoleList>
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<PagedRoleList> Handle(GetPagedRolesQuery request, CancellationToken cancellationToken)
    {
        var paged = await _roleRepository.GetPagedAsync(
            pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            configure: null,
            cancellationToken: cancellationToken
        );

        var dtoPaged = paged.Adapt<PaginationResult<RoleDto>>();

        var result = new PagedRoleList(
            Items: new RoleList(dtoPaged.Items),
            Meta: PaginationMetaFactory.From(dtoPaged)
        );

        return result;
    }
}