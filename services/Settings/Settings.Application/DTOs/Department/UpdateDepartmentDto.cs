namespace Settings.Application.DTOs.Department;

public record UpdateDepartmentDto(Guid CompanyId, string Name, string? Prefix, bool IsActive = true);
