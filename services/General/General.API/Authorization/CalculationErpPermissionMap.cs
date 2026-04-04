using System.Diagnostics.CodeAnalysis;
using General.Domain.ErpPermissions;

namespace General.API.Authorization;

/// <summary>
/// Maps salary-calculation API actions to <c>Calculation.*</c> claims (Figma: Calculation of s/n).
/// </summary>
public static class CalculationErpPermissionMap
{
    private static readonly Dictionary<string, Dictionary<string, string[]>> Map = CreateMap();

    private static Dictionary<string, Dictionary<string, string[]>> CreateMap()
    {
        var c = StringComparer.OrdinalIgnoreCase;

        // Read: explicit view/editing flags or access scope (All / own department / own — not "none").
        string[] salaryView =
        [
            CalculationErpPermissionClaims.ViewSalaryCalculation,
            CalculationErpPermissionClaims.EditingSalaryCalculation,
            CalculationErpPermissionClaims.AccessToSalaryCalculationOwn,
            CalculationErpPermissionClaims.AccessToSalaryCalculationOwnDepartment,
            CalculationErpPermissionClaims.AccessToSalaryCalculationAll,
        ];

        return new Dictionary<string, Dictionary<string, string[]>>(c)
        {
            ["SalaryCalculations"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = salaryView,
            },
        };
    }

    public static bool TryGetRequiredPermissions(
        string controllerName,
        string actionName,
        [NotNullWhen(true)] out string[]? anyOf)
    {
        anyOf = null;
        if (!Map.TryGetValue(controllerName, out var actions))
            return false;
        if (!actions.TryGetValue(actionName, out var perms))
            return false;
        anyOf = perms;
        return true;
    }
}
