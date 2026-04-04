namespace Clients.Domain.ErpPermissions;

/// <summary>
/// Flattened <c>erp_permission</c> claim values for the Clients module (employee-group JSON under <c>Clients</c>).
/// </summary>
/// <remarks>
/// Example:
/// <code>
/// {
///   "Clients": {
///     "viewClients": true,
///     "commentsEditing": true,
///     "editingContacts": true,
///     "editRequiredDocuments": true,
///     "commentsDelete": true,
///     "deletingClients": true,
///     "editingClients": true,
///     "changeCreditLimit": true,
///     "showRequiredDocuments": true,
///     "workPermission": true,
///     "commentsView": true
///   }
/// }
/// </code>
/// </remarks>
public static class ClientErpPermissionClaims
{
    public const string ViewClients = "Clients.viewClients";
    public const string CommentsEditing = "Clients.commentsEditing";
    public const string EditingContacts = "Clients.editingContacts";
    public const string EditRequiredDocuments = "Clients.editRequiredDocuments";
    public const string CommentsDelete = "Clients.commentsDelete";
    public const string DeletingClients = "Clients.deletingClients";
    public const string EditingClients = "Clients.editingClients";
    public const string ChangeCreditLimit = "Clients.changeCreditLimit";
    public const string ShowRequiredDocuments = "Clients.showRequiredDocuments";
    public const string WorkPermission = "Clients.workPermission";
    public const string CommentsView = "Clients.commentsView";
}
