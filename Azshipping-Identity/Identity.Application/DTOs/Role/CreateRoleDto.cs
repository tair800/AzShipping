namespace Identity.Application.DTOs.Role;

public record CreateRoleDto(string Name, IReadOnlyCollection<long> PermissionIds);