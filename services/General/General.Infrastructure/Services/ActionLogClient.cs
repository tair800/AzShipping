using System.Net.Http.Json;
using General.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace General.Infrastructure.Services;

public sealed class ActionLogClient(
    IHttpClientFactory httpClientFactory,
    IHttpContextAccessor httpContextAccessor,
    IOptions<ActionLogClientOptions> options,
    ILogger<ActionLogClient> logger) : IActionLogClient
{
    public async Task LogAsync(string action, string data, Guid? employeeId = null, string? employeeName = null, CancellationToken cancellationToken = default)
    {
        var baseUrl = (options.Value.SettingsBaseUrl ?? "http://localhost:5064").TrimEnd('/');
        try
        {
            var ctx = httpContextAccessor.HttpContext;
            var ipAddress = ctx?.Connection?.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(ipAddress) && ctx?.Request?.Headers.TryGetValue("X-Forwarded-For", out var forwarded) == true)
                ipAddress = forwarded.FirstOrDefault()?.Split(',')[0]?.Trim();
            var browser = ParseBrowserFriendly(ctx?.Request?.Headers["User-Agent"].FirstOrDefault());
            var sessionId = ctx?.Request?.Headers["X-Session-Id"].FirstOrDefault();

            var isLocalhost = ipAddress is "127.0.0.1" or "::1" or "localhost" or null;
            var location = isLocalhost ? "Local" : (await ResolveLocationAsync(ipAddress, cancellationToken));
            var ipDisplay = isLocalhost ? "Local" : ipAddress;

            var payload = new
            {
                action,
                data,
                sessionId,
                ipAddress = ipDisplay,
                location,
                browser,
                employeeId,
                employeeName
            };
            var client = httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync($"{baseUrl}/api/actionlogs", payload, cancellationToken);
            if (!response.IsSuccessStatusCode)
                logger.LogWarning("ActionLog POST failed: HTTP {StatusCode}", response.StatusCode);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "ActionLog POST failed for action {Action}", action);
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

    private static async Task<string?> ResolveLocationAsync(string? ip, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(ip) || ip is "127.0.0.1" or "::1" or "localhost")
            return null;
        try
        {
            using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };
            var json = await client.GetStringAsync($"http://ip-api.com/json/{ip}?fields=city,country", ct);
            if (string.IsNullOrEmpty(json)) return null;
            using var doc = System.Text.Json.JsonDocument.Parse(json);
            var root = doc.RootElement;
            var city = root.TryGetProperty("city", out var c) ? c.GetString() : null;
            var country = root.TryGetProperty("country", out var co) ? co.GetString() : null;
            if (!string.IsNullOrEmpty(city) || !string.IsNullOrEmpty(country))
                return string.Join(", ", new[] { city, country }.Where(x => !string.IsNullOrEmpty(x)));
        }
        catch { /* ignore geo lookup failures */ }
        return null;
    }
}
