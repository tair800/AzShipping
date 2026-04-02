namespace Settings.Application.DTOs.Department;

public record DepartmentDto(Guid Id, Guid CompanyId, string CompanyName, string Name, string? Prefix, bool IsActive, DateTime CreatedAt, DateTime? UpdatedAt);
