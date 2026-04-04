namespace Carrier.Domain.ErpPermissions;

/// <summary>
/// Flattened <c>erp_permission</c> values for the Carriers module (employee-group JSON under <c>Carriers</c>).
/// Matches ERP UI tab "Carriers"; API config uses <c>ErpModuleAccess:ModulePrefixes</c> including <c>Carriers</c>.
/// </summary>
/// <remarks>
/// Example:
/// <code>
/// {
///   "Carriers": {
///     "viewCarriers": true,
///     "commentsEditing": true,
///     "viewOrders": true,
///     "showRequiredDocuments": true,
///     "commentsDelete": true,
///     "workPermission": true,
///     "viewTerminals": true,
///     "removingTerminals": true,
///     "editingCarriers": true,
///     "changeCreditLimit": true,
///     "editingContacts": true,
///     "editRequiredDocuments": true,
///     "commentsView": true,
///     "deletingCarriers": true,
///     "editingTerminals": true
///   }
/// }
/// </code>
/// </remarks>
public static class CarrierErpPermissionClaims
{
    public const string ViewCarriers = "Carriers.viewCarriers";
    public const string CommentsEditing = "Carriers.commentsEditing";
    public const string ViewOrders = "Carriers.viewOrders";
    public const string ShowRequiredDocuments = "Carriers.showRequiredDocuments";
    public const string CommentsDelete = "Carriers.commentsDelete";
    public const string WorkPermission = "Carriers.workPermission";
    public const string ViewTerminals = "Carriers.viewTerminals";
    public const string RemovingTerminals = "Carriers.removingTerminals";
    public const string EditingCarriers = "Carriers.editingCarriers";
    public const string ChangeCreditLimit = "Carriers.changeCreditLimit";
    public const string EditingContacts = "Carriers.editingContacts";
    public const string EditRequiredDocuments = "Carriers.editRequiredDocuments";
    public const string CommentsView = "Carriers.commentsView";
    public const string DeletingCarriers = "Carriers.deletingCarriers";
    public const string EditingTerminals = "Carriers.editingTerminals";
}
