namespace General.Domain.ErpPermissions;

/// <summary>
/// Flattened <c>erp_permission</c> values for the Task module (employee-group JSON under <c>Task</c>).
/// Matches ERP Settings → Task tab: View / Editing / Delete tasks. Use <c>ErpModuleAccess:ModulePrefixes</c> <c>Task</c>.
/// </summary>
/// <remarks>
/// Example:
/// <code>
/// {
///   "Task": {
///     "viewTasks": true,
///     "editingTasks": true,
///     "deleteTasks": true
///   }
/// }
/// </code>
/// “Access to orders” is a separate scope field in UI, not a flattened permission string unless your Identity/Settings maps it to claims.
/// </remarks>
public static class TaskErpPermissionClaims
{
    public const string ViewTasks = "Task.viewTasks";
    public const string EditingTasks = "Task.editingTasks";
    public const string DeleteTasks = "Task.deleteTasks";
}
