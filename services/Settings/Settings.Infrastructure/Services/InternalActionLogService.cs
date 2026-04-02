using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Settings.Application.Services;
using Settings.Domain.AggregatesModel.ActionLogAggregate;

namespace Settings.Infrastructure.Services;

public sealed class InternalActionLogService(
    IActionLogRepository repository,
    IHttpContextAccessor httpContextAccessor,
    ILogger<InternalActionLogService> logger) : IInternalActionLogService
{
    public async Task LogAsync(string action, string data, Guid? employeeId = null, string? employeeName = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var ctx = httpContextAccessor.HttpContext;
            var ipAddress = ctx?.Connection?.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(ipAddress) && ctx?.Request?.Headers.TryGetValue("X-Forwarded-For", out var forwarded) == true)
                ipAddress = forwarded.FirstOrDefault()?.Split(',')[0]?.Trim();
            var browser = ParseBrowserFriendly(ctx?.Request?.Headers["User-Agent"].FirstOrDefault());
            var sessionId = ctx?.Request?.Headers["X-Session-Id"].FirstOrDefault();

            var isLocalhost = ipAddress is "127.0.0.1" or "::1" or "localhost" or null;
            var ipDisplay = isLocalhost ? "Local" : ipAddress;
            var location = isLocalhost ? "Local" : null; // Skip geo lookup for internal calls

            var entity = new ActionLog
            {
                CreatedAt = DateTime.UtcNow,
                Action = action,
                Data = data,
                SessionId = sessionId,
                IpAddress = ipDisplay,
                Location = location,
                Browser = browser,
                EmployeeId = employeeId,
                EmployeeName = employeeName
            };
            await repository.AddAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Internal ActionLog failed for action {Action}", action);
        }
    }

    private static string? ParseBrowserFriendly(string? ua)
    {
        if (string.IsNullOrEmpty(ua)) return null;
        var browser = "Unknown";
        if (ua.Contains("Edg/")) browser = "Edge";
        else if (ua.Contains("Chrome/") && !ua.Contains("Chromium")) browser = "Chrome";
        else if (ua.Contains("Firefox/")) browser = "Firefox";
        else if (ua.Contains("Safari/") && !ua.Contains("Chrome")) browser = "Safari";
        else if (ua.Contains("Opera") || ua.Contains("OPR/")) browser = "Opera";
        var os = "Unknown";
        if (ua.Contains("Windows NT")) os = "Windows";
        else if (ua.Contains("Mac OS")) os = "macOS";
        else if (ua.Contains("Linux")) os = "Linux";
        else if (ua.Contains("Android")) os = "Android";
        else if (ua.Contains("iPhone") || ua.Contains("iPad")) os = "iOS";
        return browser + " on " + os;
    }
}
