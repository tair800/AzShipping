using System.Diagnostics.CodeAnalysis;
using Operation.Domain.ErpPermissions;

namespace Operation.API.Authorization;

/// <summary>
/// Maps MVC controller/action names to required <c>erp_permission</c> values for Orders and Warehouse (Operation.API).
/// Unmapped actions are not checked (legacy behaviour).
/// </summary>
public static class OrderErpPermissionMap
{
    private static readonly Dictionary<string, Dictionary<string, string[]>> Map = CreateMap();

    private static Dictionary<string, Dictionary<string, string[]>> CreateMap()
    {
        var c = StringComparer.OrdinalIgnoreCase;
        string[] P(params string[] x) => x;

        // Warehouse tab (Figma): any of these grants access alongside Orders.* for shared Operation.API routes.
        string[] wRead =
        [
            WarehouseErpPermissionClaims.StockView,
            WarehouseErpPermissionClaims.RequestFromCustomersView,
            WarehouseErpPermissionClaims.RequestToCarrierView,
            WarehouseErpPermissionClaims.WaybillView,
            WarehouseErpPermissionClaims.ActView,
        ];
        string[] wWrite =
        [
            WarehouseErpPermissionClaims.WarehouseEditing,
            WarehouseErpPermissionClaims.RequestFromCustomersEditing,
            WarehouseErpPermissionClaims.RequestToCarrierEditing,
            WarehouseErpPermissionClaims.WaybillEditing,
            WarehouseErpPermissionClaims.ActEditing,
            WarehouseErpPermissionClaims.DocumentsActivation,
        ];
        string[] wWriteUpdate =
        [
            ..wWrite,
            WarehouseErpPermissionClaims.WaybillRoleConfirm,
        ];
        string[] wDelete =
        [
            WarehouseErpPermissionClaims.RequestFromCustomersDelete,
            WarehouseErpPermissionClaims.RequestToCarrierDelete,
            WarehouseErpPermissionClaims.WaybillDelete,
            WarehouseErpPermissionClaims.ActDelete,
        ];

        return new Dictionary<string, Dictionary<string, string[]>>(c)
        {
            ["Operations"] = new Dictionary<string, string[]>(c)
            {
                ["GetTypes"] = [OrderErpPermissionClaims.View, ..wRead],
                ["GetTypeById"] = [OrderErpPermissionClaims.View, ..wRead],
                ["CalculateAirDimensions"] =
                [
                    OrderErpPermissionClaims.FreightView,
                    OrderErpPermissionClaims.CargoInformation,
                    ..wRead,
                ],
                ["CalculateFinanceAmounts"] = P(
                    OrderErpPermissionClaims.FinancialOperationsView,
                    OrderErpPermissionClaims.Profit,
                    OrderErpPermissionClaims.Margin),
                ["GetAll"] = [OrderErpPermissionClaims.View, ..wRead],
                ["GetList"] = [OrderErpPermissionClaims.View, ..wRead],
                ["GetTripsList"] =
                [
                    OrderErpPermissionClaims.View,
                    OrderErpPermissionClaims.TripsInOrder,
                    ..wRead,
                ],
                ["GetCargosList"] =
                [
                    OrderErpPermissionClaims.View,
                    OrderErpPermissionClaims.CargoInformation,
                    ..wRead,
                ],
                ["GetById"] = [OrderErpPermissionClaims.View, ..wRead],
                ["Create"] = [OrderErpPermissionClaims.Editing, ..wWrite],
                ["Update"] = [OrderErpPermissionClaims.Editing, ..wWriteUpdate],
                ["Delete"] =
                [
                    OrderErpPermissionClaims.Deleting,
                    OrderErpPermissionClaims.FinancialOperationsDelete,
                    ..wDelete,
                ],
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
