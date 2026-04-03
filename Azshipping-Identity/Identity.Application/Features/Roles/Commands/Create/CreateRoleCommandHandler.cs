using Mapster;
using MediatR;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Identity.Application.DTOs.Role;
using Identity.Application.Rules.RoleRules;
using MrStyx.Domain.SeedWork.Persistence;

namespace Identity.Application.Features.Roles.Commands.Create;

public sealed class CreateRoleCommandHandler
(
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork,
    IRoleRules roleRules

) : IRequestHandler<CreateRoleCommand, RoleDto>
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRoleRules _roleRules = roleRules;

    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var dto = request.CreateRoleDto;

        await _roleRules.RoleUniquenessCheckAsync(dto.Name, cancellationToken);

        await _roleRules.FindMissingPermissionsAsync(dto.PermissionIds, cancellationToken);

        var role = Role.Create(dto.Name, dto.PermissionIds);

        var createdRole = await _roleRepository.AddAsync(role, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var result = createdRole.Adapt<RoleDto>();

        return result;
    }
}