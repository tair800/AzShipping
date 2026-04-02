namespace Settings.Application.DTOs.ActionLog;

public record ActionLogDto(
    long Id,
    DateTime CreatedAt,
    string Action,
    string Data,
    string? SessionId,
    string? IpAddress,
    string? Location,
    string? Browser,
    Guid? EmployeeId,
    string? EmployeeName);
