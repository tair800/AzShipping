using MrStyx.Domain.SeedWork.Utils;

namespace Identity.Application.DTOs.Role;

public record RoleDto(long Id, string Name, IReadOnlyCollection<long> PermissionIds);

public record RoleList(IReadOnlyCollection<RoleDto> Roles);

public record PagedRoleList(RoleList Items, PaginationMeta Meta);