using System.Text.Json;
using Settings.Application.DTOs.EmployeeGroup;
using Settings.Domain.AggregatesModel.EmployeeGroupAggregate;

namespace Settings.Application.Features.EmployeeGroups;

public static class EmployeeGroupMapper
{
    public static EmployeeGroupListItemDto ToListItem(EmployeeGroup e) =>
        new(e.Id, e.Name, e.CompanyId, e.Company?.Name);

    public static EmployeeGroupDetailDto ToDetail(EmployeeGroup e)
    {
        var raw = string.IsNullOrWhiteSpace(e.PermissionsJson) ? "{}" : e.PermissionsJson;
        var permissions = JsonSerializer.Deserialize<JsonElement>(raw);
        return new EmployeeGroupDetailDto(
            e.Id,
            e.Name,
            e.CompanyId,
            e.Company?.Name,
            permissions,
            e.CreatedAtUtc,
            e.UpdatedAtUtc);
    }
}
