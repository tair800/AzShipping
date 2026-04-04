namespace General.Domain.ErpPermissions;

/// <summary>
/// Flattened <c>erp_permission</c> values for the Calculation of s/n tab (employee-group JSON under <c>Calculation</c>).
/// Boolean keys become dotted claims; the access dropdown is stored as a string and becomes <c>Calculation.accessToSalaryCalculation=&lt;value&gt;</c>.
/// </summary>
/// <remarks>
/// Example:
/// <code>
/// {
///   "Calculation": {
///     "accessToSalaryCalculation": "all",
///     "viewSalaryCalculation": true,
///     "editingSalaryCalculation": true
///   }
/// }
/// </code>
/// Allowed <c>accessToSalaryCalculation</c> values (standard &quot;Access to&quot; dropdown):
/// <c>all</c> (All), <c>ownDepartment</c> (Your own department), <c>own</c> (Your own), <c>none</c> (no access / not selected).
/// </remarks>
public static class CalculationErpPermissionClaims
{
    /// <summary>JWT claim when JSON has <c>"accessToSalaryCalculation": "none|own|ownDepartment|all"</c>.</summary>
    public const string AccessToSalaryCalculationNone = "Calculation.accessToSalaryCalculation=none";
    public const string AccessToSalaryCalculationOwn = "Calculation.accessToSalaryCalculation=own";
    public const string AccessToSalaryCalculationOwnDepartment = "Calculation.accessToSalaryCalculation=ownDepartment";
    public const string AccessToSalaryCalculationAll = "Calculation.accessToSalaryCalculation=all";

    public const string ViewSalaryCalculation = "Calculation.viewSalaryCalculation";
    public const string EditingSalaryCalculation = "Calculation.editingSalaryCalculation";
}
