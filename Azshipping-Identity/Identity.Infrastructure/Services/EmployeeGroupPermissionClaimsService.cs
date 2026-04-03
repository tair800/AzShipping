using System.Text;
using System.Text.Json;
using Identity.Application.Interfaces.Services;
using Identity.Application.Services;
using Identity.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure.Services;

public sealed class EmployeeGroupPermissionClaimsService(
    IHttpClientFactory httpClientFactory,
    IOptions<SettingsClientOptions> options,
    ILogger<EmployeeGroupPermissionClaimsService> logger)
    : IEmployeeGroupPermissionClaimsService
{
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true
    };

    public async Task<ErpPermissionResolution> ResolveAsync(
        IReadOnlyList<Guid> employeeGroupIds,
        bool unlimitedAccess,
        CancellationToken cancellationToken = default)
    {
        if (unlimitedAccess)
            return new ErpPermissionResolution([], true);

        if (employeeGroupIds == null || employeeGroupIds.Count == 0)
            return new ErpPermissionResolution([], false);

        var opt = options.Value;
        var apiKey = opt.EmployeeGroupResolveApiKey?.Trim();
        if (string.IsNullOrEmpty(apiKey))
        {
            logger.LogWarning("Settings:EmployeeGroupResolveApiKey is not set; erp_permission claims will be empty.");
            return new ErpPermissionResolution([], false);
        }

        var baseUrl = opt.BaseUrl.TrimEnd('/');
        var client = httpClientFactory.CreateClient();
        try
        {
            var body = JsonSerializer.Serialize(new { ids = employeeGroupIds }, JsonOpts);
            using var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}/api/employee-groups/resolve-permissions")
            {
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            };
            request.Headers.TryAddWithoutValidation(ErpClaimTypes.ResolvePermissionsHeaderName, apiKey);
            var response = await client.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync(cancellationToken);
                logger.LogWarning(
                    "Settings resolve-permissions failed: HTTP {Status}. Body: {Body}",
                    (int)response.StatusCode,
                    err.Length > 400 ? err[..400] : err);
                return new ErpPermissionResolution([], false);
            }

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var dto = await JsonSerializer.DeserializeAsync<ResolveResponse>(stream, JsonOpts, cancellationToken);
            var claims = dto?.Claims ?? [];
            return new ErpPermissionResolution(claims, false);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to resolve employee group permissions from Settings.");
            return new ErpPermissionResolution([], false);
        }
    }

    private sealed class ResolveResponse
    {
        public List<string>? Claims { get; set; }
    }
}
