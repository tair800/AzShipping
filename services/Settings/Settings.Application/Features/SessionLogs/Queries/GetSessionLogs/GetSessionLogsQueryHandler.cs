using MediatR;
using Settings.Domain.AggregatesModel.ActionLogAggregate;

namespace Settings.Application.Features.SessionLogs.Queries.GetSessionLogs;

public sealed class GetSessionLogsQueryHandler(IActionLogRepository repository)
    : IRequestHandler<GetSessionLogsQuery, GetSessionLogsResult>
{
    private const int ActivityGapMinutes = 30; // Consecutive actions within 30 min = same activity block

    public async Task<GetSessionLogsResult> Handle(GetSessionLogsQuery request, CancellationToken ct)
    {
        var entries = await repository.GetForSessionLogAsync(
            request.DateFrom, request.DateTo, request.EmployeeId, request.EmployeeName, 50000, ct);

        Console.WriteLine("[Session Log API] Request: dateFrom={0}, dateTo={1} | Entries with SessionId: {2} | SessionIds: {3}",
            request.DateFrom, request.DateTo, entries.Count,
            string.Join(", ", entries.Select(x => x.SessionId).Distinct().Take(10)));

        // Group by SessionId
        var bySession = entries
            .GroupBy(x => x.SessionId!)
            .Select(g => BuildSession(g.ToList()))
            .Where(s => s != null)
            .ToList()!;

        // Group by date (from first activity in session)
        var byDate = bySession
            .GroupBy(s => s!.DateKey)
            .OrderByDescending(g => g.Key)
            .Select(dateGrp => new SessionLogDateGroup(
                dateGrp.Key,
                dateGrp
                    .GroupBy(s => (s!.EmployeeId, EmployeeName: s.EmployeeName ?? ""))
                    .OrderBy(mg => mg.Key.EmployeeName)
                    .Select(mg => BuildManagerGroup(mg.Where(s => s != null).Cast<SessionInfo>().ToList()))
                    .ToList()))
            .ToList();

        var allSessionIds = byDate.SelectMany(g => g.Managers.SelectMany(m => m.Sessions.Select(s => s.SessionId))).ToList();
        Console.WriteLine("[Session Log API] Output session IDs: {0}", string.Join(", ", allSessionIds));

        return new GetSessionLogsResult(byDate);
    }

    private static SessionInfo? BuildSession(IReadOnlyList<ActionLog> entries)
    {
        if (entries.Count == 0) return null;
        var first = entries.OrderBy(x => x.CreatedAt).First();
        var timestamps = entries.Select(x => x.CreatedAt).OrderBy(x => x).ToList();
        var (intervals, totalMinutes) = BuildTimeIntervals(timestamps);
        var timeStr = FormatDuration(totalMinutes);
        var actions = entries.Select(x => x.Action ?? "").Where(a => !string.IsNullOrEmpty(a)).Distinct().ToList();
        return new SessionInfo
        {
            SessionId = first.SessionId!,
            IpAddress = ToFriendlyIp(first.IpAddress),
            Location = first.Location ?? "-",
            Browser = ToFriendlyBrowser(first.Browser),
            EmployeeId = first.EmployeeId,
            EmployeeName = first.EmployeeName,
            DateKey = first.CreatedAt.ToString("dd.MM.yyyy"),
            Time = timeStr,
            TimeIntervals = intervals,
            TotalMinutes = totalMinutes,
            Actions = actions
        };
    }

    private static (string intervals, int totalMinutes) BuildTimeIntervals(IReadOnlyList<DateTime> sorted)
    {
        if (sorted.Count == 0) return ("", 0);
        var blocks = new List<(DateTime start, DateTime end)>();
        var start = sorted[0];
        var end = sorted[0];
        foreach (var t in sorted.Skip(1))
        {
            if ((t - end).TotalMinutes <= ActivityGapMinutes)
                end = t;
            else
            {
                blocks.Add((start, end));
                start = t;
                end = t;
            }
        }
        blocks.Add((start, end));
        var totalMinutes = (int)blocks.Sum(b => (b.end - b.start).TotalMinutes);
        if (totalMinutes == 0 && sorted.Count > 0) totalMinutes = 1; // at least 1 min if we have activity
        var intervalsStr = string.Join("; ", blocks.Select(b =>
            b.start.ToString("HH:mm") + " - " + b.end.ToString("HH:mm")));
        return (intervalsStr, totalMinutes);
    }

    private static string FormatDuration(int totalMinutes)
    {
        if (totalMinutes < 60) return "0:" + totalMinutes.ToString("D2");
        var h = totalMinutes / 60;
        var m = totalMinutes % 60;
        return h + ":" + m.ToString("D2");
    }

    private static SessionLogManagerGroup BuildManagerGroup(IReadOnlyList<SessionInfo> sessions)
    {
        var empId = sessions[0].EmployeeId;
        var empName = sessions[0].EmployeeName ?? "-";
        var rows = sessions.Select(s => new SessionLogRow(s.SessionId, s.IpAddress, s.Location, s.Browser, s.Time, s.TimeIntervals, s.Actions ?? Array.Empty<string>())).ToList();
        var ips = string.Join("; ", sessions.Select(s => s.IpAddress).Distinct().OrderBy(x => x));
        var locs = string.Join("; ", sessions.Select(s => s.Location).Distinct().Where(x => x != "-").OrderBy(x => x));
        if (string.IsNullOrEmpty(locs)) locs = "-";
        var browsers = string.Join("; ", sessions.Select(s => s.Browser).Distinct().Where(x => x != "-").OrderBy(x => x));
        if (string.IsNullOrEmpty(browsers)) browsers = "-";
        var totalMin = sessions.Sum(s => s.TotalMinutes);
        var totalTime = FormatDuration(totalMin);
        return new SessionLogManagerGroup(empId, empName, rows, new SessionLogSummary(ips, locs, browsers, totalTime));
    }

    private static string ToFriendlyIp(string? ip) =>
        string.IsNullOrEmpty(ip) || ip is "127.0.0.1" or "::1" or "localhost" ? "Local" : ip;

    private static string ToFriendlyBrowser(string? ua)
    {
        if (string.IsNullOrEmpty(ua)) return "-";
        if (ua.Length < 60) return ua;
        var browser = ua.Contains("Edg/") ? "Edge" : ua.Contains("Chrome/") && !ua.Contains("Chromium") ? "Chrome"
            : ua.Contains("Firefox/") ? "Firefox" : ua.Contains("Safari/") && !ua.Contains("Chrome") ? "Safari"
            : ua.Contains("Opera") || ua.Contains("OPR/") ? "Opera" : "Browser";
        var os = ua.Contains("Windows NT") ? "Windows" : ua.Contains("Mac OS") ? "macOS"
            : ua.Contains("Linux") ? "Linux" : ua.Contains("Android") ? "Android"
            : ua.Contains("iPhone") || ua.Contains("iPad") ? "iOS" : "";
        return string.IsNullOrEmpty(os) ? browser : $"{browser} on {os}";
    }

    private class SessionInfo
    {
        public string SessionId { get; set; } = "";
        public string IpAddress { get; set; } = "";
        public string Location { get; set; } = "";
        public string Browser { get; set; } = "";
        public Guid? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string DateKey { get; set; } = "";
        public string Time { get; set; } = "";
        public string TimeIntervals { get; set; } = "";
        public int TotalMinutes { get; set; }
        public IReadOnlyList<string> Actions { get; set; } = Array.Empty<string>();
    }
}
