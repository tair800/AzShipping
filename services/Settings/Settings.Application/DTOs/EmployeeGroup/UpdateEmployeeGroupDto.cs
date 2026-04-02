namespace Settings.Application.DTOs.EmployeeGroup;

public record UpdateEmployeeGroupDto(string Name, Guid? CompanyId, string? PermissionsJson);
