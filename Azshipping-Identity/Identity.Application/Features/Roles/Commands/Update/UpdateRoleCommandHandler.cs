using Mapster;
using MediatR;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Identity.Application.DTOs.Role;
using Identity.Application.Rules.RoleRules;
using MrStyx.Domain.SeedWork.Persistence;
using MrStyx.Application.Exceptions;

namespace Identity.Application.Features.Roles.Commands.Update;

public sealed class UpdateRoleCommandHandler
(
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork,
    IRoleRules roleRules

) : IRequestHandler<UpdateRoleCommand, RoleDto>
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IRoleRules _roleRules = roleRules;

    public async Task<RoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var dto = request.UpdateRoleDto;

        var role = await _roleRepository.GetByIdAsync(dto.Id, cancellationToken, trackingMode: QueryTrackingMode.Tracking) ?? 
            throw new NotFoundException($"Can't find role by id \"{dto.Id}\"");

        await _roleRules.RoleUniquenessCheckAsync(dto.Name, dto.Id, cancellationToken);

        await _roleRules.FindMissingPermissionsAsync(dto.PermissionIds, cancellationToken);

        role.Update(dto.Name, dto.PermissionIds);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var updatedRole = await _roleRepository.GetByIdAsync(role.Id, cancellationToken);

        var result = updatedRole.Adapt<RoleDto>();

        return result;
    }
}