namespace AzShipping.ApiSecurity;

/// <summary>
/// Canonical string values for ERP &quot;Access to&quot; dropdowns (employee-group JSON → JWT <c>prefix=key=value</c>).
/// UI labels (EN): <c>all</c> → &quot;All&quot;, <c>ownDepartment</c> → &quot;Your own department&quot;, <c>own</c> → &quot;Your own&quot;.
/// </summary>
/// <remarks>
/// Use camelCase in JSON. Settings <c>EmployeeGroupPermissionMerger</c> merges multiple groups by most permissive rank:
/// <c>none</c> &lt; <c>own</c> &lt; <c>ownDepartment</c> &lt; <c>all</c>.
/// </remarks>
public static class ErpAccessScopeValues
{
    public const string None = "none";
    public const string Own = "own";
    public const string OwnDepartment = "ownDepartment";
    public const string All = "all";
}
