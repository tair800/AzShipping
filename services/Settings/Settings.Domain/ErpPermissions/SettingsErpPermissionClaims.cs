namespace Settings.Domain.ErpPermissions;

/// <summary>
/// Flattened <c>erp_permission</c> values for the Settings tab (employee-group JSON under <c>Settings</c>).
/// </summary>
/// <remarks>
/// Example:
/// <code>
/// {
///   "Settings": {
///     "system": { "view": true, "editing": true },
///     "organization": { "view": true, "editing": true },
///     "classifiers": { "view": true, "editing": true },
///     "templates": { "view": true, "editing": true },
///     "dataTransferViaApi": { "roleActivate": true }
///   }
/// }
/// </code>
/// </remarks>
public static class SettingsErpPermissionClaims
{
    public const string SystemView = "Settings.system.view";
    public const string SystemEditing = "Settings.system.editing";

    public const string OrganizationView = "Settings.organization.view";
    public const string OrganizationEditing = "Settings.organization.editing";

    public const string ClassifiersView = "Settings.classifiers.view";
    public const string ClassifiersEditing = "Settings.classifiers.editing";

    public const string TemplatesView = "Settings.templates.view";
    public const string TemplatesEditing = "Settings.templates.editing";

    public const string DataTransferViaApiRoleActivate = "Settings.dataTransferViaApi.roleActivate";
}
