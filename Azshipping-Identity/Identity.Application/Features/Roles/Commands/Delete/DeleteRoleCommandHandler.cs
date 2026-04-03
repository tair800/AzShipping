using Identity.Application.Rules.RoleRules;
using Identity.Domain.AggregatesModel.RoleAggregate;
using MediatR;
using MrStyx.Application.Exceptions;

namespace Identity.Application.Features.Roles.Commands.Delete;

public sealed class DeleteRoleCommandHandler(IRoleRepository roleRepository, IRoleRules roleRules) : IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IRoleRepository _roleRepository = roleRepository;
    private readonly IRoleRules _roleRules = roleRules;

    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(request.Id, cancellationToken) ?? 
            throw new NotFoundException($"Can't find role by id \"{request.Id}\"");

        await _roleRules.IsAssignedToUserAsync(role.Id, cancellationToken);

        role.IsSystemRole();

        return await _roleRepository.DeleteAsync(role.Id, cancellationToken);
    }
}