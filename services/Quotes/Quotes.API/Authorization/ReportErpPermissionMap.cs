using System.Diagnostics.CodeAnalysis;
using AzShipping.ApiSecurity;
using Quotes.Domain.ErpPermissions;

namespace Quotes.API.Authorization;

/// <summary>
/// Maps controller actions to Reports and Import/Export permissions. Quote CRUD and dimension calculator stay unmapped.
/// </summary>
public static class ReportErpPermissionMap
{
    private static readonly Dictionary<string, Dictionary<string, string[]>> Map = CreateMap();

    private static Dictionary<string, Dictionary<string, string[]>> CreateMap()
    {
        var c = StringComparer.OrdinalIgnoreCase;
        string[] P(params string[] x) => x;

        return new Dictionary<string, Dictionary<string, string[]>>(c)
        {
            ["Quotes"] = new Dictionary<string, string[]>(c)
            {
                ["GetTypes"] = P(
                    ReportErpPermissionClaims.IndividualReports,
                    ReportErpPermissionClaims.Statistics),
                ["GetAll"] = P(
                    ReportErpPermissionClaims.IndividualReports,
                    ReportErpPermissionClaims.Statistics),
                ["ExportToExcel"] = P(
                    ImportExportErpPermissionClaims.ReportsExportToExcel,
                    ReportErpPermissionClaims.IndividualReports,
                    ReportErpPermissionClaims.Statistics),
                ["ExportSingleToExcel"] = P(
                    ImportExportErpPermissionClaims.ReportsExportToExcel,
                    ReportErpPermissionClaims.IndividualReports,
                    ReportErpPermissionClaims.Statistics),
                ["GetFunnel"] = P(ReportErpPermissionClaims.PurchaseFunnel),
                ["GetById"] = P(
                    ReportErpPermissionClaims.IndividualReports,
                    ReportErpPermissionClaims.Statistics),
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
