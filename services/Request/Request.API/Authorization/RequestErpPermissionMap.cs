using System.Diagnostics.CodeAnalysis;
using Request.Domain.ErpPermissions;

namespace Request.API.Authorization;

/// <summary>
/// Maps MVC <see cref="Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor.ControllerName"/>
/// and <c>ActionName</c> to required <c>erp_permission</c> claim values (any match is enough).
/// Actions not listed are not checked by the global filter (legacy behaviour).
/// </summary>
public static class RequestErpPermissionMap
{
    private static readonly Dictionary<string, Dictionary<string, string[]>> Map = CreateMap();

    private static Dictionary<string, Dictionary<string, string[]>> CreateMap()
    {
        var c = StringComparer.OrdinalIgnoreCase;
        string[] P(params string[] x) => x;

        return new Dictionary<string, Dictionary<string, string[]>>(c)
        {
            ["Requests"] = new Dictionary<string, string[]>(c)
            {
                ["GetTypes"] = P(RequestErpPermissionClaims.ViewRequest),
                ["GetTypeById"] = P(RequestErpPermissionClaims.ViewRequest),
                ["CreateType"] = P(RequestErpPermissionClaims.EditingRequest),
                ["UpdateType"] = P(RequestErpPermissionClaims.EditingRequest),
                ["DeleteType"] = P(RequestErpPermissionClaims.DeletingRequests),
                ["GetAll"] = P(RequestErpPermissionClaims.ViewRequest),
                ["GetById"] = P(RequestErpPermissionClaims.ViewRequest),
                ["CalculateDimensions"] = P(RequestErpPermissionClaims.RequestRate),
                ["Create"] = P(RequestErpPermissionClaims.EditingRequest),
                ["Update"] = P(RequestErpPermissionClaims.EditingRequest),
                ["Delete"] = P(RequestErpPermissionClaims.DeletingRequests),
            },
            ["RequestComments"] = new Dictionary<string, string[]>(c)
            {
                ["GetByRequestId"] = P(RequestErpPermissionClaims.CommentsView),
                ["GetById"] = P(RequestErpPermissionClaims.CommentsView),
                ["Create"] = P(RequestErpPermissionClaims.CommentsEditing),
                ["Update"] = P(RequestErpPermissionClaims.CommentsEditing),
                ["Delete"] = P(RequestErpPermissionClaims.CommentsDelete),
            },
            ["PriceProposals"] = new Dictionary<string, string[]>(c)
            {
                ["GetByRequestId"] = P(RequestErpPermissionClaims.PriceProposalsView),
                ["GetById"] = P(RequestErpPermissionClaims.PriceProposalsView),
                ["Create"] = P(RequestErpPermissionClaims.PriceProposalsEdit),
                ["Update"] = P(RequestErpPermissionClaims.PriceProposalsEdit, RequestErpPermissionClaims.PriceProposalsEditOthers),
                ["Delete"] = P(RequestErpPermissionClaims.PriceProposalsDelete),
            },
            ["CommercialOffers"] = new Dictionary<string, string[]>(c)
            {
                ["GetByRequestId"] = P(RequestErpPermissionClaims.ViewRequest),
                ["GetById"] = P(RequestErpPermissionClaims.ViewRequest),
                ["Create"] = P(RequestErpPermissionClaims.EditingRequest),
                ["Update"] = P(RequestErpPermissionClaims.EditingRequest),
                ["Delete"] = P(RequestErpPermissionClaims.DeletingRequests),
            },
            ["Sales"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = P(RequestErpPermissionClaims.ViewRequest),
                ["GetById"] = P(RequestErpPermissionClaims.ViewRequest),
                ["Create"] = P(RequestErpPermissionClaims.EditingRequest),
                ["Update"] = P(RequestErpPermissionClaims.EditingRequest),
                ["Delete"] = P(RequestErpPermissionClaims.DeletingRequests),
            },
            ["SaleStatuses"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = P(RequestErpPermissionClaims.ViewRequest),
                ["GetById"] = P(RequestErpPermissionClaims.ViewRequest),
                ["Create"] = P(RequestErpPermissionClaims.ChangeProtectedStatuses),
                ["Update"] = P(RequestErpPermissionClaims.ChangeProtectedStatuses),
                ["Delete"] = P(RequestErpPermissionClaims.ChangeProtectedStatuses),
            },
            ["RequestNegotiations"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = P(RequestErpPermissionClaims.ViewRequest),
                ["GetById"] = P(RequestErpPermissionClaims.ViewRequest),
                ["Create"] = P(RequestErpPermissionClaims.EditingRequest),
                ["Update"] = P(RequestErpPermissionClaims.EditingRequest),
                ["Delete"] = P(RequestErpPermissionClaims.DeletingRequests),
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
