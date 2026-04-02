namespace Settings.Application.Features.SessionLogs.Queries.GetSessionLogs;

/// <summary>Session log grouped by date, then by manager.</summary>
public sealed record GetSessionLogsResult(IReadOnlyList<SessionLogDateGroup> Groups);

public sealed record SessionLogDateGroup(string Date, IReadOnlyList<SessionLogManagerGroup> Managers);

public sealed record SessionLogManagerGroup(Guid? EmployeeId, string? EmployeeName, IReadOnlyList<SessionLogRow> Sessions, SessionLogSummary Summary);

public sealed record SessionLogRow(string SessionId, string IpAddress, string Location, string Browser, string Time, string TimeIntervals, IReadOnlyList<string> Actions);

public sealed record SessionLogSummary(string Ips, string Locations, string Browsers, string TotalTime);
