namespace Operation.Domain.ErpPermissions;

/// <summary>
/// Flattened <c>erp_permission</c> values for the Warehouse tab (employee-group JSON under <c>Warehouse</c>).
/// </summary>
/// <remarks>
/// Example:
/// <code>
/// {
///   "Warehouse": {
///     "stockView": true,
///     "warehouseEditing": true,
///     "useWarehouseMobileApplications": true,
///     "documentsActivation": true,
///     "requestForDeliveryFromCustomers": { "view": true, "editing": true, "delete": true },
///     "act": { "view": true, "editing": true, "delete": true },
///     "requestForDeliveryToCarrier": { "view": true, "editing": true, "delete": true },
///     "waybill": { "view": true, "editing": true, "delete": true, "roleConfirm": true }
///   }
/// }
/// </code>
/// </remarks>
public static class WarehouseErpPermissionClaims
{
    public const string StockView = "Warehouse.stockView";
    public const string WarehouseEditing = "Warehouse.warehouseEditing";
    public const string UseWarehouseMobileApplications = "Warehouse.useWarehouseMobileApplications";
    public const string DocumentsActivation = "Warehouse.documentsActivation";

    public const string RequestFromCustomersView = "Warehouse.requestForDeliveryFromCustomers.view";
    public const string RequestFromCustomersEditing = "Warehouse.requestForDeliveryFromCustomers.editing";
    public const string RequestFromCustomersDelete = "Warehouse.requestForDeliveryFromCustomers.delete";

    public const string ActView = "Warehouse.act.view";
    public const string ActEditing = "Warehouse.act.editing";
    public const string ActDelete = "Warehouse.act.delete";

    public const string RequestToCarrierView = "Warehouse.requestForDeliveryToCarrier.view";
    public const string RequestToCarrierEditing = "Warehouse.requestForDeliveryToCarrier.editing";
    public const string RequestToCarrierDelete = "Warehouse.requestForDeliveryToCarrier.delete";

    public const string WaybillView = "Warehouse.waybill.view";
    public const string WaybillEditing = "Warehouse.waybill.editing";
    public const string WaybillDelete = "Warehouse.waybill.delete";
    public const string WaybillRoleConfirm = "Warehouse.waybill.roleConfirm";
}
