using Identity.Application.DTOs.Role;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Mapster;
using MediatR;

namespace Identity.Application.Features.Roles.Queries.GetAll;

public sealed class GetAllRolesQueryHandler(IRoleRepository roleRepository) : IRequestHandler<GetAllRolesQuery, RoleList>
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<RoleList> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetAllAsync(cancellationToken);

        var result = roles.Adapt<RoleList>();

        return result;
    }
}