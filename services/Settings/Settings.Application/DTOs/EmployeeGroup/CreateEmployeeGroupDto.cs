namespace Settings.Application.DTOs.EmployeeGroup;

public record CreateEmployeeGroupDto(string Name, Guid? CompanyId, string? PermissionsJson);
