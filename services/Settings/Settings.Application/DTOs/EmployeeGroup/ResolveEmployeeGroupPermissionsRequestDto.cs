namespace Settings.Application.DTOs.EmployeeGroup;

public record ResolveEmployeeGroupPermissionsRequestDto(IReadOnlyList<Guid>? Ids);
