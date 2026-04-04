namespace Quotes.Domain.ErpPermissions;

/// <summary>
/// Flattened <c>erp_permission</c> values for the Reports module (employee-group JSON under <c>Reports</c>).
/// Aligns with ERP Settings → Reports tab checkboxes (Figma).
/// </summary>
/// <remarks>
/// Example:
/// <code>
/// {
///   "Reports": {
///     "debts": true,
///     "expenses": true,
///     "contactWithClients": true,
///     "revenueDetailing": true,
///     "statistics": true,
///     "subcontractedDeals": true,
///     "individualReports": true,
///     "purchaseFunnel": true,
///     "post": true,
///     "balance": true,
///     "documents": true,
///     "reportTransportExpense": true,
///     "trips": true,
///     "reconciliation": true,
///     "roundTripCosts": true
///   }
/// }
/// </code>
/// Replace Figma label <c>SettingRole_Report_Transport_Expense</c> in UI with user-facing text; JSON key <c>reportTransportExpense</c>.
/// </remarks>
public static class ReportErpPermissionClaims
{
    public const string Debts = "Reports.debts";
    public const string Expenses = "Reports.expenses";
    public const string ContactWithClients = "Reports.contactWithClients";
    public const string RevenueDetailing = "Reports.revenueDetailing";
    public const string Statistics = "Reports.statistics";
    public const string SubcontractedDeals = "Reports.subcontractedDeals";
    public const string IndividualReports = "Reports.individualReports";
    public const string PurchaseFunnel = "Reports.purchaseFunnel";
    public const string Post = "Reports.post";
    public const string Balance = "Reports.balance";
    public const string Documents = "Reports.documents";
    /// <summary>Figma technical key <c>SettingRole_Report_Transport_Expense</c> → camelCase in JSON.</summary>
    public const string ReportTransportExpense = "Reports.reportTransportExpense";
    public const string Trips = "Reports.trips";
    public const string Reconciliation = "Reports.reconciliation";
    public const string RoundTripCosts = "Reports.roundTripCosts";
}
