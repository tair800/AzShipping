using Identity.Application.DTOs.Role;
using MediatR;
using MrStyx.Application.Interfaces;

namespace Identity.Application.Features.Roles.Commands.Update;

public sealed record UpdateRoleCommand : IRequest<RoleDto>, ITransactionalRequest
{
    public UpdateRoleDto UpdateRoleDto { get; init; }

    public UpdateRoleCommand(UpdateRoleDto updateRoleDto)
    {
        UpdateRoleDto = updateRoleDto ?? throw new ArgumentNullException(nameof(updateRoleDto));
    }
}