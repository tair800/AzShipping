namespace Settings.Application.DTOs.Department;

public record CreateDepartmentDto(Guid CompanyId, string Name, string? Prefix = null, bool IsActive = true);
