using System.Text.Json;

namespace Settings.Application.DTOs.EmployeeGroup;

public record EmployeeGroupDetailDto(
    Guid Id,
    string Name,
    Guid? CompanyId,
    string? CompanyName,
    JsonElement Permissions,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc);
