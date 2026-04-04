namespace Operation.Domain.ErpPermissions;

/// <summary>
/// Flattened <c>erp_permission</c> claim values for the Orders module (employee-group JSON under <c>Orders</c>).
/// Keys follow the ERP UI: general checkboxes at the root, grouped actions nested (freight, financialOperations, …).
/// </summary>
/// <remarks>
/// Example group JSON:
/// <code>
/// {
///   "Orders": {
///     "view": true,
///     "comments": true,
///     "all": true,
///     "commentsRemoving": true,
///     "profit": true,
///     "clientInformation": true,
///     "statistic": true,
///     "commentsView": true,
///     "margin": true,
///     "editing": true,
///     "tripsInOrder": true,
///     "total": true,
///     "providers": true,
///     "cargoInformation": true,
///     "documents": true,
///     "consignmentStatusEdit": true,
///     "carrierInformation": true,
///     "deleting": true,
///     "freight": { "view": true, "editing": true },
///     "financialOperations": { "view": true, "editing": true, "delete": true },
///     "notIncludedOperations": { "view": true, "editing": true, "delete": true },
///     "tripCost": { "view": true, "editing": true, "delete": true },
///     "additionalTripExpenses": { "view": true, "editing": true, "delete": true }
///   }
/// }
/// </code>
/// </remarks>
public static class OrderErpPermissionClaims
{
    public const string View = "Orders.view";
    public const string Comments = "Orders.comments";
    public const string All = "Orders.all";
    public const string CommentsRemoving = "Orders.commentsRemoving";
    public const string Profit = "Orders.profit";
    public const string ClientInformation = "Orders.clientInformation";
    public const string Statistic = "Orders.statistic";
    public const string CommentsView = "Orders.commentsView";
    public const string Margin = "Orders.margin";
    public const string Editing = "Orders.editing";
    public const string TripsInOrder = "Orders.tripsInOrder";
    public const string Total = "Orders.total";
    public const string Providers = "Orders.providers";
    public const string CargoInformation = "Orders.cargoInformation";
    public const string Documents = "Orders.documents";
    public const string ConsignmentStatusEdit = "Orders.consignmentStatusEdit";
    public const string CarrierInformation = "Orders.carrierInformation";
    public const string Deleting = "Orders.deleting";

    public const string FreightView = "Orders.freight.view";
    public const string FreightEditing = "Orders.freight.editing";

    public const string FinancialOperationsView = "Orders.financialOperations.view";
    public const string FinancialOperationsEditing = "Orders.financialOperations.editing";
    public const string FinancialOperationsDelete = "Orders.financialOperations.delete";

    public const string NotIncludedOperationsView = "Orders.notIncludedOperations.view";
    public const string NotIncludedOperationsEditing = "Orders.notIncludedOperations.editing";
    public const string NotIncludedOperationsDelete = "Orders.notIncludedOperations.delete";

    public const string TripCostView = "Orders.tripCost.view";
    public const string TripCostEditing = "Orders.tripCost.editing";
    public const string TripCostDelete = "Orders.tripCost.delete";

    public const string AdditionalTripExpensesView = "Orders.additionalTripExpenses.view";
    public const string AdditionalTripExpensesEditing = "Orders.additionalTripExpenses.editing";
    public const string AdditionalTripExpensesDelete = "Orders.additionalTripExpenses.delete";
}
