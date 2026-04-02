using MediatR;
using Settings.Application.DTOs.ActionLog;
using Settings.Domain.AggregatesModel.ActionLogAggregate;

namespace Settings.Application.Features.ActionLogs.Queries.GetPaged;

public sealed class GetActionLogsPagedQueryHandler(IActionLogRepository repository)
    : IRequestHandler<GetActionLogsPagedQuery, GetActionLogsPagedResult>
{
    public async Task<GetActionLogsPagedResult> Handle(GetActionLogsPagedQuery request, CancellationToken ct)
    {
        var (items, total) = await repository.GetPagedAsync(
            request.DateFrom, request.DateTo, request.EmployeeId, request.EmployeeName,
            request.Action, request.OrderFilter, request.Page, request.PageSize, ct);

        var dtos = items.Select(x => new ActionLogDto(
            x.Id, x.CreatedAt, x.Action, FormatDataForDisplay(x.Data),
            x.SessionId, ToFriendlyIp(x.IpAddress), x.Location ?? "-", ToFriendlyBrowser(x.Browser),
            x.EmployeeId, x.EmployeeName)).ToList();
        return new GetActionLogsPagedResult(dtos, total);
    }

    private static string FormatDataForDisplay(string data) =>
        string.IsNullOrEmpty(data) ? "" : data.Replace("; ", " • ", StringComparison.Ordinal);

    private static string ToFriendlyIp(string? ip) =>
        string.IsNullOrEmpty(ip) || ip is "127.0.0.1" or "::1" or "localhost" ? "Local" : ip;

    private static string ToFriendlyBrowser(string? ua)
    {
        if (string.IsNullOrEmpty(ua)) return "-";
        if (ua.Length < 60) return ua; // Already friendly
        var browser = ua.Contains("Edg/") ? "Edge" : ua.Contains("Chrome/") && !ua.Contains("Chromium") ? "Chrome"
            : ua.Contains("Firefox/") ? "Firefox" : ua.Contains("Safari/") && !ua.Contains("Chrome") ? "Safari"
            : ua.Contains("Opera") || ua.Contains("OPR/") ? "Opera" : "Browser";
        var os = ua.Contains("Windows NT") ? "Windows" : ua.Contains("Mac OS") ? "macOS"
            : ua.Contains("Linux") ? "Linux" : ua.Contains("Android") ? "Android"
            : ua.Contains("iPhone") || ua.Contains("iPad") ? "iOS" : "";
        return string.IsNullOrEmpty(os) ? browser : $"{browser} on {os}";
    }
}
