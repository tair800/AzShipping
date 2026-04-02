using System.Text.Json;

namespace Settings.Application.Features.EmployeeGroups;

public static class EmployeeGroupPermissionJson
{
    public static string Normalize(string? json)
    {
        if (string.IsNullOrWhiteSpace(json)) return "{}";
        try
        {
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.GetRawText();
        }
        catch (JsonException)
        {
            throw new InvalidOperationException("Permissions must be valid JSON.");
        }
    }
}
