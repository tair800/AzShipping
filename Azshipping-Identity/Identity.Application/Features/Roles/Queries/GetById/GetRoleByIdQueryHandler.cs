using Mapster;
using MediatR;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Identity.Application.DTOs.Role;
using MrStyx.Application.Exceptions;

namespace Identity.Application.Features.Roles.Queries.GetById;

public sealed class GetRoleByIdQueryHandler(IRoleRepository roleRepository) : IRequestHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id, cancellationToken) ?? 
            throw new NotFoundException($"Can't find role by id \"{request.Id}\"");

        var result = role.Adapt<RoleDto>();

        return result;
    }
}