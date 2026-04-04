namespace Request.Domain.ErpPermissions;

/// <summary>
/// Flattened <c>erp_permission</c> claim values for the Request module (employee-group JSON under <c>Request</c>).
/// Example group JSON:
/// <code>
/// { "Request": {
///   "viewRequest": true,
///   "commentsEditing": true,
///   "deletingRequests": true,
///   "requestRate": true,
///   "priceProposalsEdit": true,
///   "commentsView": true,
///   "editingRequest": true,
///   "changeProtectedStatuses": true,
///   "commentsDelete": true,
///   "priceProposalsView": true,
///   "priceProposalsDelete": true,
///   "priceProposalsEditOthers": true
/// }}
/// </code>
/// </summary>
public static class RequestErpPermissionClaims
{
    public const string ViewRequest = "Request.viewRequest";
    public const string CommentsEditing = "Request.commentsEditing";
    public const string DeletingRequests = "Request.deletingRequests";
    public const string RequestRate = "Request.requestRate";
    public const string PriceProposalsEdit = "Request.priceProposalsEdit";
    public const string CommentsView = "Request.commentsView";
    public const string EditingRequest = "Request.editingRequest";
    public const string ChangeProtectedStatuses = "Request.changeProtectedStatuses";
    public const string CommentsDelete = "Request.commentsDelete";
    public const string PriceProposalsView = "Request.priceProposalsView";
    public const string PriceProposalsDelete = "Request.priceProposalsDelete";
    public const string PriceProposalsEditOthers = "Request.priceProposalsEditOthers";
}
