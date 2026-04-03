using System.Net.Http.Headers;
using System.Text.Json;
using Identity.Infrastructure.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure.Services;

/// <summary>Loads company, department, and worker post display names from Settings.API (same IDs stored on Identity users).</summary>
public sealed class SettingsCatalogHttpReader(
    IHttpClientFactory httpClientFactory,
    IHttpContextAccessor httpContextAccessor,
    IOptions<SettingsClientOptions> options)
{
    private static readonly JsonSerializerOptions JsonOpts = new() { PropertyNameCaseInsensitive = true };

    public async Task<Dictionary<Guid, string>> LoadCompanyNamesAsync(CancellationToken cancellationToken)
    {
        var map = new Dictionary<Guid, string>();
        var baseUrl = options.Value.BaseUrl.TrimEnd('/');
        var client = httpClientFactory.CreateClient();
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/api/Companies");
            AttachAuth(request);
            var response = await client.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode) return map;
            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var items = await JsonSerializer.DeserializeAsync<List<CompanyJson>>(stream, JsonOpts, cancellationToken);
            if (items == null) return map;
            foreach (var c in items)
                map[c.Id] = c.Name ?? "";
        }
        catch
        {
            /* leave partial/empty */
        }

        return map;
    }

    public async Task<Dictionary<Guid, string>> LoadDepartmentNamesAsync(CancellationToken cancellationToken)
    {
        var map = new Dictionary<Guid, string>();
        var baseUrl = options.Value.BaseUrl.TrimEnd('/');
        var client = httpClientFactory.CreateClient();
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/api/departments");
            AttachAuth(request);
            var response = await client.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode) return map;
            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var items = await JsonSerializer.DeserializeAsync<List<DepartmentJson>>(stream, JsonOpts, cancellationToken);
            if (items == null) return map;
            foreach (var d in items)
                map[d.Id] = d.Name ?? "";
        }
        catch
        {
        }

        return map;
    }

    public async Task<Dictionary<Guid, string>> LoadWorkerPostNamesAsync(CancellationToken cancellationToken)
    {
        var map = new Dictionary<Guid, string>();
        var baseUrl = options.Value.BaseUrl.TrimEnd('/');
        var client = httpClientFactory.CreateClient();
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/api/workerposts");
            AttachAuth(request);
            var response = await client.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode) return map;
            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var items = await JsonSerializer.DeserializeAsync<List<WorkerPostJson>>(stream, JsonOpts, cancellationToken);
            if (items == null) return map;
            foreach (var p in items)
                map[p.Id] = p.Name ?? "";
        }
        catch
        {
        }

        return map;
    }

    private void AttachAuth(HttpRequestMessage request)
    {
        if (httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("Authorization", out var authValues) != true)
            return;
        var auth = authValues.ToString();
        if (string.IsNullOrWhiteSpace(auth)) return;
        if (auth.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            var token = auth["Bearer ".Length..].Trim();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        else
            request.Headers.TryAddWithoutValidation("Authorization", auth);
    }

    private sealed class CompanyJson
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    private sealed class DepartmentJson
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }

    private sealed class WorkerPostJson
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
