namespace Settings.API.Options;

/// <summary>Protects <c>POST /api/employee-groups/resolve-permissions</c> for Identity (no user JWT at login).</summary>
public sealed class EmployeeGroupResolveOptions
{
    public const string SectionName = "EmployeeGroupResolve";

    /// <summary>Shared secret; caller sends header <c>X-AzShipping-Employee-Groups-Resolve-Key</c>.</summary>
    public string? ApiKey { get; set; }
}
