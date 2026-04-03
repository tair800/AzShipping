using Identity.Application.DTOs.Role;
using Identity.Application.SearchPredicates;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Mapster;
using MediatR;

namespace Identity.Application.Features.Roles.Queries.GetWhere;

public sealed class GetRolesWhereQueryHandler(IRoleRepository roleRepository) : IRequestHandler<GetRolesWhereQuery, RoleList>
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<RoleList> Handle(GetRolesWhereQuery request, CancellationToken cancellationToken)
    {
        var dto = request.SearchRoleDto;

        var predicate = SearchRolePredicate.BuildPredicate(dto);

        var roles = await _roleRepository.GetWhereAsync(predicate, cancellationToken);

        var result = roles.Adapt<RoleList>();

        return result;
    }
}