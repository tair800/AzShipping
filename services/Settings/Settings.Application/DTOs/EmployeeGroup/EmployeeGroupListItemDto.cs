namespace Settings.Application.DTOs.EmployeeGroup;

public record EmployeeGroupListItemDto(Guid Id, string Name, Guid? CompanyId, string? CompanyName);
