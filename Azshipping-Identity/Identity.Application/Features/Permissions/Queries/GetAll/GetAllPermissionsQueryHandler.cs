using Identity.Application.DTOs.Permission;
using Identity.Domain.AggregatesModel.PermissionAggregate;
using Mapster;
using MediatR;

namespace Identity.Application.Features.Permissions.Queries.GetAll;

public sealed class GetAllPermissionsQueryHandler(IPermissionRepository permissionRepository) : IRequestHandler<GetAllPermissionsQuery, PermissionList>
{
    private readonly IPermissionRepository _permissionRepository = permissionRepository;

    public async Task<PermissionList> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
    {
        var permissions = await _permissionRepository.GetAllAsync(cancellationToken);

        var result = permissions.Adapt<PermissionList>();

        return result;
    }
}