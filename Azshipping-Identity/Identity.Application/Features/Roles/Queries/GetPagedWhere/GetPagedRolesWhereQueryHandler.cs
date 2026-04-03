using Identity.Application.DTOs.Role;
using Identity.Application.SearchPredicates;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Mapster;
using MediatR;
using MrStyx.Domain.SeedWork.Utils;

namespace Identity.Application.Features.Roles.Queries.GetPagedWhere;

public sealed class GetPagedRolesWhereQueryHandler(IRoleRepository roleRepository) : IRequestHandler<GetPagedRolesWhereQuery, PagedRoleList>
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<PagedRoleList> Handle(GetPagedRolesWhereQuery request, CancellationToken cancellationToken)
    {
        var predicate = SearchRolePredicate.BuildPredicate(request.SearchRoleDto);

        var paged = await _roleRepository.GetPagedWhereAsync
            (
                predicate: predicate,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                configure: null,
                cancellationToken: cancellationToken
            );

        var dtoPaged = paged.Adapt<PaginationResult<RoleDto>>();

        var result = new PagedRoleList
            (
                Items: new RoleList(dtoPaged.Items),
                Meta: PaginationMetaFactory.From(dtoPaged)
            );

        return result;
    }
}