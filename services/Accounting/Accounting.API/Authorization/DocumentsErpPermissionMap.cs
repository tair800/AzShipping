using System.Diagnostics.CodeAnalysis;
using Accounting.Domain.ErpPermissions;

namespace Accounting.API.Authorization;

/// <summary>
/// Maps controller/action to required <c>erp_permission</c> claim values (any match is enough).
/// Actions not listed are not checked by the global filter.
/// </summary>
public static class DocumentsErpPermissionMap
{
    private static readonly Dictionary<string, Dictionary<string, string[]>> Map = CreateMap();

    private static Dictionary<string, Dictionary<string, string[]>> CreateMap()
    {
        var c = StringComparer.OrdinalIgnoreCase;
        string[] P(params string[] x) => x;

        var invoiceView = P(
            DocumentsErpPermissionClaims.IssuedInvoicesView,
            DocumentsErpPermissionClaims.ReceivedInvoicesView);
        var invoiceEdit = P(
            DocumentsErpPermissionClaims.IssuedInvoicesEditing,
            DocumentsErpPermissionClaims.ReceivedInvoicesEditing);
        var invoiceEditPaid = P(
            DocumentsErpPermissionClaims.IssuedInvoicesEditing,
            DocumentsErpPermissionClaims.ReceivedInvoicesEditing,
            DocumentsErpPermissionClaims.IssuedInvoicesEditingPaid,
            DocumentsErpPermissionClaims.ReceivedInvoicesEditingPaid);
        var invoiceDelete = P(
            DocumentsErpPermissionClaims.IssuedInvoicesDelete,
            DocumentsErpPermissionClaims.ReceivedInvoicesDelete);

        var paymentView = P(
            DocumentsErpPermissionClaims.IncomingPaymentsView,
            DocumentsErpPermissionClaims.EffectedIncomingPaymentsView);
        var paymentEdit = P(
            DocumentsErpPermissionClaims.IncomingPaymentsEditing,
            DocumentsErpPermissionClaims.EffectedIncomingPaymentsEditing);
        var paymentDelete = P(
            DocumentsErpPermissionClaims.IncomingPaymentsDelete,
            DocumentsErpPermissionClaims.EffectedIncomingPaymentsDelete);

        return new Dictionary<string, Dictionary<string, string[]>>(c)
        {
            ["OperationInvoices"] = new Dictionary<string, string[]>(c)
            {
                ["GetByOperation"] = invoiceView,
                ["Create"] = invoiceEdit,
                ["Update"] = invoiceEditPaid,
                ["Delete"] = invoiceDelete,
            },
            ["OperationActs"] = new Dictionary<string, string[]>(c)
            {
                ["Create"] = P(DocumentsErpPermissionClaims.ActEditing),
                ["Delete"] = P(DocumentsErpPermissionClaims.ActDelete),
            },
            ["Payments"] = new Dictionary<string, string[]>(c)
            {
                ["GetPaymentsMade"] = P(DocumentsErpPermissionClaims.EffectedIncomingPaymentsView),
                ["GetAll"] = paymentView,
                ["GetById"] = paymentView,
                ["Create"] = paymentEdit,
            },
            ["InvoiceLookups"] = new Dictionary<string, string[]>(c)
            {
                ["Create"] = invoiceEdit,
            },
            ["VatDefinitions"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = P(DocumentsErpPermissionClaims.OtherDocumentsView),
                ["GetById"] = P(DocumentsErpPermissionClaims.OtherDocumentsView),
                ["Create"] = P(DocumentsErpPermissionClaims.OtherDocumentsEditing),
                ["Update"] = P(DocumentsErpPermissionClaims.OtherDocumentsEditing),
                ["Delete"] = P(DocumentsErpPermissionClaims.OtherDocumentsDelete),
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
