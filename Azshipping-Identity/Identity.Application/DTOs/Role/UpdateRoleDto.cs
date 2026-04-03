namespace Identity.Application.DTOs.Role;

public record UpdateRoleDto(long Id, string Name, IReadOnlyCollection<long> PermissionIds);