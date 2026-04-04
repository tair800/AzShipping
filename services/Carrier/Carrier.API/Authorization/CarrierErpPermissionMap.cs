using System.Diagnostics.CodeAnalysis;
using Carrier.Domain.ErpPermissions;

namespace Carrier.API.Authorization;

/// <summary>
/// Maps MVC controller/actions to <c>erp_permission</c> values for Carrier.API. Unmapped = not checked.
/// </summary>
public static class CarrierErpPermissionMap
{
    private static readonly Dictionary<string, Dictionary<string, string[]>> Map = CreateMap();

    private static Dictionary<string, Dictionary<string, string[]>> CreateMap()
    {
        var c = StringComparer.OrdinalIgnoreCase;
        string[] P(params string[] x) => x;

        return new Dictionary<string, Dictionary<string, string[]>>(c)
        {
            ["Carriers"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["GetById"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["Create"] = P(CarrierErpPermissionClaims.EditingCarriers),
                ["Update"] = P(CarrierErpPermissionClaims.EditingCarriers),
                ["Delete"] = P(CarrierErpPermissionClaims.DeletingCarriers),
            },
            ["Terminals"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = P(CarrierErpPermissionClaims.ViewTerminals, CarrierErpPermissionClaims.ViewCarriers),
                ["GetById"] = P(CarrierErpPermissionClaims.ViewTerminals, CarrierErpPermissionClaims.ViewCarriers),
                ["Create"] = P(CarrierErpPermissionClaims.EditingTerminals),
                ["Update"] = P(CarrierErpPermissionClaims.EditingTerminals),
                ["Delete"] = P(CarrierErpPermissionClaims.RemovingTerminals, CarrierErpPermissionClaims.EditingTerminals),
            },
            ["CarrierDocuments"] = new Dictionary<string, string[]>(c)
            {
                ["GetByCarrierId"] = P(CarrierErpPermissionClaims.ShowRequiredDocuments, CarrierErpPermissionClaims.ViewCarriers),
                ["GetCarrierDocumentById"] = P(CarrierErpPermissionClaims.ShowRequiredDocuments, CarrierErpPermissionClaims.ViewCarriers),
                ["Create"] = P(CarrierErpPermissionClaims.EditRequiredDocuments),
                ["UploadFile"] = P(CarrierErpPermissionClaims.EditRequiredDocuments),
                ["Update"] = P(CarrierErpPermissionClaims.EditRequiredDocuments),
                ["Delete"] = P(CarrierErpPermissionClaims.EditRequiredDocuments),
            },
            ["CarrierDirections"] = new Dictionary<string, string[]>(c)
            {
                ["GetByCarrierId"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["GetCarrierDirectionById"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["Create"] = P(CarrierErpPermissionClaims.EditingContacts, CarrierErpPermissionClaims.EditingCarriers),
                ["Update"] = P(CarrierErpPermissionClaims.EditingContacts, CarrierErpPermissionClaims.EditingCarriers),
                ["Delete"] = P(CarrierErpPermissionClaims.EditingContacts, CarrierErpPermissionClaims.EditingCarriers),
            },
            ["CarrierTasks"] = new Dictionary<string, string[]>(c)
            {
                ["GetByCarrierId"] = P(CarrierErpPermissionClaims.ViewCarriers, CarrierErpPermissionClaims.CommentsView),
                ["GetCarrierTaskById"] = P(CarrierErpPermissionClaims.ViewCarriers, CarrierErpPermissionClaims.CommentsView),
                ["Create"] = P(CarrierErpPermissionClaims.CommentsEditing, CarrierErpPermissionClaims.WorkPermission),
                ["Update"] = P(CarrierErpPermissionClaims.CommentsEditing, CarrierErpPermissionClaims.WorkPermission),
                ["Delete"] = P(CarrierErpPermissionClaims.CommentsDelete),
            },
            ["CarrierDrivers"] = new Dictionary<string, string[]>(c)
            {
                ["GetByCarrierId"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["Create"] = P(CarrierErpPermissionClaims.EditingContacts, CarrierErpPermissionClaims.EditingCarriers),
            },
            ["CarrierVehicles"] = new Dictionary<string, string[]>(c)
            {
                ["GetByCarrierId"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["Create"] = P(CarrierErpPermissionClaims.EditingCarriers, CarrierErpPermissionClaims.WorkPermission),
            },
            ["Drivers"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["GetById"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["Create"] = P(CarrierErpPermissionClaims.EditingContacts, CarrierErpPermissionClaims.EditingCarriers),
                ["Update"] = P(CarrierErpPermissionClaims.EditingContacts, CarrierErpPermissionClaims.EditingCarriers),
                ["Delete"] = P(CarrierErpPermissionClaims.EditingContacts, CarrierErpPermissionClaims.EditingCarriers),
                ["UploadPassport"] = P(CarrierErpPermissionClaims.EditRequiredDocuments, CarrierErpPermissionClaims.EditingContacts),
                ["UploadDrivingLicence"] = P(CarrierErpPermissionClaims.EditRequiredDocuments, CarrierErpPermissionClaims.EditingContacts),
            },
            ["Vehicles"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["GetById"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["Create"] = P(CarrierErpPermissionClaims.EditingCarriers, CarrierErpPermissionClaims.WorkPermission),
                ["Update"] = P(CarrierErpPermissionClaims.EditingCarriers, CarrierErpPermissionClaims.WorkPermission),
                ["Delete"] = P(CarrierErpPermissionClaims.EditingCarriers, CarrierErpPermissionClaims.WorkPermission),
            },
            ["VehicleLookups"] = new Dictionary<string, string[]>(c)
            {
                ["GetBrands"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["GetModels"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["GetEuroEmissionClasses"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["GetGroups"] = P(CarrierErpPermissionClaims.ViewCarriers),
            },
            ["Airlines"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["GetById"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["Create"] = P(CarrierErpPermissionClaims.EditingCarriers),
                ["Update"] = P(CarrierErpPermissionClaims.EditingCarriers),
                ["Delete"] = P(CarrierErpPermissionClaims.EditingCarriers),
            },
            ["RailwayStations"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["GetById"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["Create"] = P(CarrierErpPermissionClaims.EditingCarriers),
                ["Update"] = P(CarrierErpPermissionClaims.EditingCarriers),
                ["Delete"] = P(CarrierErpPermissionClaims.EditingCarriers),
            },
            ["ShippingAgents"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["GetById"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["Create"] = P(CarrierErpPermissionClaims.EditingCarriers),
                ["Update"] = P(CarrierErpPermissionClaims.EditingCarriers),
                ["Delete"] = P(CarrierErpPermissionClaims.EditingCarriers),
            },
            ["ShippingLines"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["GetById"] = P(CarrierErpPermissionClaims.ViewCarriers),
                ["Create"] = P(CarrierErpPermissionClaims.EditingCarriers),
                ["Update"] = P(CarrierErpPermissionClaims.EditingCarriers),
                ["Delete"] = P(CarrierErpPermissionClaims.EditingCarriers),
            },
            ["Projects"] = new Dictionary<string, string[]>(c)
            {
                ["GetByCarrierId"] = P(CarrierErpPermissionClaims.ViewCarriers, CarrierErpPermissionClaims.ViewOrders),
                ["Create"] = P(CarrierErpPermissionClaims.EditingCarriers),
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
