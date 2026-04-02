using System.Net.Http.Headers;
using System.Text.Json;
using General.Application.DTOs.Employee;
using General.Application.Features.Employees;
using General.Application.Services;
using General.Domain.AggregatesModel.EmployeeAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace General.Infrastructure.Services;

public sealed class SettingsCatalogLookup(
    IHttpClientFactory httpClientFactory,
    IHttpContextAccessor httpContextAccessor,
    IOptions<ActionLogClientOptions> options) : ISettingsCatalogLookup
{
    private static readonly JsonSerializerOptions JsonOpts = new() { PropertyNameCaseInsensitive = true };

    private Dictionary<Guid, string>? _departmentNames;
    private Dictionary<Guid, string>? _workerPostNames;

    public async Task<EmployeeDto> ToEmployeeDtoAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        var list = await ToEmployeeDtosAsync([employee], cancellationToken);
        return list[0];
    }

    public async Task<IReadOnlyList<EmployeeDto>> ToEmployeeDtosAsync(IReadOnlyList<Employee> employees, CancellationToken cancellationToken = default)
    {
        await EnsureMapsAsync(cancellationToken);
        return employees.Select(e =>
        {
            var core = EmployeeMapper.ToCoreDto(e);
            return core with
            {
                DepartmentName = ResolveName(e.DepartmentId, _departmentNames!),
                WorkerPostName = ResolveName(e.WorkerPostId, _workerPostNames!)
            };
        }).ToList();
    }

    public async Task<EmployeeSummaryDto> ToEmployeeSummaryAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        var list = await ToEmployeeSummariesAsync([employee], cancellationToken);
        return list[0];
    }

    public async Task<IReadOnlyList<EmployeeSummaryDto>> ToEmployeeSummariesAsync(IReadOnlyList<Employee> employees, CancellationToken cancellationToken = default)
    {
        await EnsureMapsAsync(cancellationToken);
        return employees.Select(e =>
        {
            var core = EmployeeMapper.ToCoreSummary(e);
            return core with
            {
                DepartmentName = ResolveName(e.DepartmentId, _departmentNames!),
                WorkerPostName = ResolveName(e.WorkerPostId, _workerPostNames!)
            };
        }).ToList();
    }

    private static string? ResolveName(Guid? id, Dictionary<Guid, string> map)
    {
        if (!id.HasValue) return null;
        return map.TryGetValue(id.Value, out var n) ? n : null;
    }

    private async Task EnsureMapsAsync(CancellationToken cancellationToken)
    {
        if (_departmentNames != null && _workerPostNames != null) return;

        _departmentNames = new Dictionary<Guid, string>();
        _workerPostNames = new Dictionary<Guid, string>();

        var baseUrl = (options.Value.SettingsBaseUrl ?? "http://localhost:5064").TrimEnd('/');
        var client = httpClientFactory.CreateClient();

        await LoadDepartmentsAsync(client, baseUrl, cancellationToken);
        await LoadWorkerPostsAsync(client, baseUrl, cancellationToken);
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

    private async Task LoadDepartmentsAsync(HttpClient client, string baseUrl, CancellationToken cancellationToken)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/api/departments");
            AttachAuth(request);
            var response = await client.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode) return;
            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var items = await JsonSerializer.DeserializeAsync<List<DepartmentJson>>(stream, JsonOpts, cancellationToken);
            if (items == null) return;
            foreach (var d in items)
                _departmentNames![d.Id] = d.Name ?? "";
        }
        catch
        {
            /* names stay empty */
        }
    }

    private async Task LoadWorkerPostsAsync(HttpClient client, string baseUrl, CancellationToken cancellationToken)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/api/workerposts");
            AttachAuth(request);
            var response = await client.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode) return;
            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var items = await JsonSerializer.DeserializeAsync<List<WorkerPostJson>>(stream, JsonOpts, cancellationToken);
            if (items == null) return;
            foreach (var p in items)
                _workerPostNames![p.Id] = p.Name ?? "";
        }
        catch
        {
            /* names stay empty */
        }
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
