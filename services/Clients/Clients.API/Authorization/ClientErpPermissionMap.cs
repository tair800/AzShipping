using System.Diagnostics.CodeAnalysis;
using Clients.Domain.ErpPermissions;

namespace Clients.API.Authorization;

/// <summary>
/// Maps MVC controller/actions to required <c>erp_permission</c> values. Unmapped actions are not checked.
/// </summary>
public static class ClientErpPermissionMap
{
    private static readonly Dictionary<string, Dictionary<string, string[]>> Map = CreateMap();

    private static Dictionary<string, Dictionary<string, string[]>> CreateMap()
    {
        var c = StringComparer.OrdinalIgnoreCase;
        string[] P(params string[] x) => x;

        return new Dictionary<string, Dictionary<string, string[]>>(c)
        {
            ["Clients"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = P(ClientErpPermissionClaims.ViewClients),
                ["GetById"] = P(ClientErpPermissionClaims.ViewClients),
                ["Create"] = P(ClientErpPermissionClaims.EditingClients),
                ["Update"] = P(ClientErpPermissionClaims.EditingClients),
                ["UpdateStage"] = P(ClientErpPermissionClaims.EditingClients),
                ["UpdateAdditionalField"] = P(ClientErpPermissionClaims.CommentsEditing, ClientErpPermissionClaims.EditingClients),
                ["Delete"] = P(ClientErpPermissionClaims.DeletingClients),
            },
            ["Negotiations"] = new Dictionary<string, string[]>(c)
            {
                ["GetById"] = P(ClientErpPermissionClaims.ViewClients, ClientErpPermissionClaims.CommentsView),
                ["GetByClientId"] = P(ClientErpPermissionClaims.ViewClients, ClientErpPermissionClaims.CommentsView),
                ["Create"] = P(ClientErpPermissionClaims.CommentsEditing),
                ["Update"] = P(ClientErpPermissionClaims.CommentsEditing),
                ["Delete"] = P(ClientErpPermissionClaims.CommentsDelete),
            },
            ["Documents"] = new Dictionary<string, string[]>(c)
            {
                ["GetById"] = P(ClientErpPermissionClaims.ShowRequiredDocuments, ClientErpPermissionClaims.ViewClients),
                ["GetByClientId"] = P(ClientErpPermissionClaims.ShowRequiredDocuments, ClientErpPermissionClaims.ViewClients),
                ["Create"] = P(ClientErpPermissionClaims.EditRequiredDocuments),
                ["Upload"] = P(ClientErpPermissionClaims.EditRequiredDocuments),
                ["UploadFile"] = P(ClientErpPermissionClaims.EditRequiredDocuments),
                ["Update"] = P(ClientErpPermissionClaims.EditRequiredDocuments),
                ["Delete"] = P(ClientErpPermissionClaims.EditRequiredDocuments),
            },
            ["Directions"] = new Dictionary<string, string[]>(c)
            {
                ["GetById"] = P(ClientErpPermissionClaims.ViewClients),
                ["GetByClientId"] = P(ClientErpPermissionClaims.ViewClients),
                ["Create"] = P(ClientErpPermissionClaims.EditingContacts, ClientErpPermissionClaims.EditingClients),
                ["Update"] = P(ClientErpPermissionClaims.EditingContacts, ClientErpPermissionClaims.EditingClients),
                ["Delete"] = P(ClientErpPermissionClaims.EditingContacts, ClientErpPermissionClaims.EditingClients),
            },
            ["Currencies"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = P(ClientErpPermissionClaims.ViewClients),
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
