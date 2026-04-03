using Identity.Application.DTOs.Role;
using MediatR;
using MrStyx.Application.Interfaces;

namespace Identity.Application.Features.Roles.Commands.Create;

public sealed record CreateRoleCommand : IRequest<RoleDto>, ITransactionalRequest
{
    public CreateRoleDto CreateRoleDto { get; init; }

    public CreateRoleCommand(CreateRoleDto createRoleDto)
    {
        CreateRoleDto = createRoleDto ?? throw new ArgumentNullException(nameof(createRoleDto));
    }
}