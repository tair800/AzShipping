namespace Accounting.Domain.ErpPermissions;

/// <summary>
/// Flattened <c>erp_permission</c> values for the Documents tab (employee-group JSON under <c>Documents</c>).
/// Nested keys become dotted paths when Settings merges employee-group JSON into JWT claims.
/// </summary>
/// <remarks>
/// Example:
/// <code>
/// {
///   "Documents": {
///     "issuedInvoices": { "view": true, "editing": true, "delete": true, "editingPaidInvoices": true },
///     "receivedInvoices": { "view": true, "editing": true, "delete": true, "editingPaidInvoices": true },
///     "act": { "view": true, "editing": true, "delete": true },
///     "incomingPayments": { "view": true, "editing": true, "delete": true },
///     "effectedIncomingPayments": { "view": true, "editing": true, "delete": true },
///     "otherDocuments": { "view": true, "editing": true, "delete": true },
///     "documentsForRequest": { "view": true, "editing": true, "delete": true }
///   }
/// }
/// </code>
/// </remarks>
public static class DocumentsErpPermissionClaims
{
    public const string IssuedInvoicesView = "Documents.issuedInvoices.view";
    public const string IssuedInvoicesEditing = "Documents.issuedInvoices.editing";
    public const string IssuedInvoicesDelete = "Documents.issuedInvoices.delete";
    public const string IssuedInvoicesEditingPaid = "Documents.issuedInvoices.editingPaidInvoices";

    public const string ReceivedInvoicesView = "Documents.receivedInvoices.view";
    public const string ReceivedInvoicesEditing = "Documents.receivedInvoices.editing";
    public const string ReceivedInvoicesDelete = "Documents.receivedInvoices.delete";
    public const string ReceivedInvoicesEditingPaid = "Documents.receivedInvoices.editingPaidInvoices";

    public const string ActView = "Documents.act.view";
    public const string ActEditing = "Documents.act.editing";
    public const string ActDelete = "Documents.act.delete";

    public const string IncomingPaymentsView = "Documents.incomingPayments.view";
    public const string IncomingPaymentsEditing = "Documents.incomingPayments.editing";
    public const string IncomingPaymentsDelete = "Documents.incomingPayments.delete";

    public const string EffectedIncomingPaymentsView = "Documents.effectedIncomingPayments.view";
    public const string EffectedIncomingPaymentsEditing = "Documents.effectedIncomingPayments.editing";
    public const string EffectedIncomingPaymentsDelete = "Documents.effectedIncomingPayments.delete";

    public const string OtherDocumentsView = "Documents.otherDocuments.view";
    public const string OtherDocumentsEditing = "Documents.otherDocuments.editing";
    public const string OtherDocumentsDelete = "Documents.otherDocuments.delete";

    public const string DocumentsForRequestView = "Documents.documentsForRequest.view";
    public const string DocumentsForRequestEditing = "Documents.documentsForRequest.editing";
    public const string DocumentsForRequestDelete = "Documents.documentsForRequest.delete";
}
