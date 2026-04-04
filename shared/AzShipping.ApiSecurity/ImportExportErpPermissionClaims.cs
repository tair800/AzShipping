namespace AzShipping.ApiSecurity;

/// <summary>
/// Flattened <c>erp_permission</c> values for the Import/Export ERP tab (employee-group JSON under <c>ImportExport</c>).
/// </summary>
/// <remarks>
/// Example structure (booleans under nested objects merge to dotted claim paths):
/// <code>
/// {
///   "ImportExport": {
///     "request": { "exportToExcel": true, "importFromExcel": true },
///     "orders": {
///       "exportToExcel": true,
///       "exportFlightsToExcel": true,
///       "exportToXml": true,
///       "importFromExcel": true,
///       "exportPayrollCalculationToExcel": true
///     },
///     "cargos": { "exportToExcel": true, "exportCargoStatusesToExcel": true, "importFromExcel": true },
///     "documents": {
///       "exportIssuedInvoicesToExcel": true,
///       "exportIssuedInvoicesToXml": true,
///       "exportReceivedInvoicesToExcel": true,
///       "exportReceivedInvoicesToXml": true,
///       "exportActsToExcel": true,
///       "exportIncomingPaymentsToExcel": true
///     },
///     "clients": { "exportToExcel": true, "exportToXml": true, "importFromExcel": true },
///     "carriers": {
///       "exportToExcel": true,
///       "exportToXml": true,
///       "importFromExcel": true,
///       "importTerminalsToExcel": true
///     },
///     "transport": { "importFromExcel": true },
///     "drivers": { "importFromExcel": true },
///     "reports": { "exportToExcel": true }
///   }
/// }
/// </code>
/// </remarks>
public static class ImportExportErpPermissionClaims
{
    public const string RequestExportToExcel = "ImportExport.request.exportToExcel";
    public const string RequestImportFromExcel = "ImportExport.request.importFromExcel";

    public const string OrdersExportToExcel = "ImportExport.orders.exportToExcel";
    public const string OrdersExportFlightsToExcel = "ImportExport.orders.exportFlightsToExcel";
    public const string OrdersExportToXml = "ImportExport.orders.exportToXml";
    public const string OrdersImportFromExcel = "ImportExport.orders.importFromExcel";
    public const string OrdersExportPayrollCalculationToExcel = "ImportExport.orders.exportPayrollCalculationToExcel";

    public const string CargosExportToExcel = "ImportExport.cargos.exportToExcel";
    public const string CargosExportCargoStatusesToExcel = "ImportExport.cargos.exportCargoStatusesToExcel";
    public const string CargosImportFromExcel = "ImportExport.cargos.importFromExcel";

    public const string DocumentsExportIssuedInvoicesToExcel = "ImportExport.documents.exportIssuedInvoicesToExcel";
    public const string DocumentsExportIssuedInvoicesToXml = "ImportExport.documents.exportIssuedInvoicesToXml";
    public const string DocumentsExportReceivedInvoicesToExcel = "ImportExport.documents.exportReceivedInvoicesToExcel";
    public const string DocumentsExportReceivedInvoicesToXml = "ImportExport.documents.exportReceivedInvoicesToXml";
    public const string DocumentsExportActsToExcel = "ImportExport.documents.exportActsToExcel";
    public const string DocumentsExportIncomingPaymentsToExcel = "ImportExport.documents.exportIncomingPaymentsToExcel";

    public const string ClientsExportToExcel = "ImportExport.clients.exportToExcel";
    public const string ClientsExportToXml = "ImportExport.clients.exportToXml";
    public const string ClientsImportFromExcel = "ImportExport.clients.importFromExcel";

    public const string CarriersExportToExcel = "ImportExport.carriers.exportToExcel";
    public const string CarriersExportToXml = "ImportExport.carriers.exportToXml";
    public const string CarriersImportFromExcel = "ImportExport.carriers.importFromExcel";
    public const string CarriersImportTerminalsToExcel = "ImportExport.carriers.importTerminalsToExcel";

    public const string TransportImportFromExcel = "ImportExport.transport.importFromExcel";
    public const string DriversImportFromExcel = "ImportExport.drivers.importFromExcel";

    public const string ReportsExportToExcel = "ImportExport.reports.exportToExcel";
}
