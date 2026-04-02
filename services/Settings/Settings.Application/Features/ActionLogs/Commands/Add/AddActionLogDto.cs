namespace Settings.Application.Features.ActionLogs.Commands.Add;

public record AddActionLogDto(
    string Action,
    string Data,
    string? SessionId = null,
    string? IpAddress = null,
    string? Location = null,
    string? Browser = null,
    Guid? EmployeeId = null,
    string? EmployeeName = null);
