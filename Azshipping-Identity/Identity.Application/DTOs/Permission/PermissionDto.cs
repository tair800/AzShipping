namespace Identity.Application.DTOs.Permission;

public record PermissionDto(long Id, string Name, string Module);

public record PermissionList(IReadOnlyCollection<PermissionDto> Permissions);