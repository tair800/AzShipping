using System.Diagnostics.CodeAnalysis;
using Settings.API.Controllers;
using Settings.Domain.ErpPermissions;

namespace Settings.API.Authorization;

/// <summary>
/// Maps Settings.API controllers to <c>Settings.*</c> ERP claims (Figma Settings tab).
/// </summary>
public static class SettingsErpPermissionMap
{
    // Must be declared before Map: static field initializers run in source order; CreateMap() reads this array.
    private static readonly string[] ClassifierControllerNames =
    [
        nameof(AddressTypesController).Replace("Controller", ""),
        nameof(BanksController).Replace("Controller", ""),
        nameof(CarrierTypesController).Replace("Controller", ""),
        nameof(CitiesController).Replace("Controller", ""),
        nameof(ClientSegmentsController).Replace("Controller", ""),
        nameof(ClientSourcesController).Replace("Controller", ""),
        nameof(CountriesController).Replace("Controller", ""),
        nameof(DeferredPaymentConditionsController).Replace("Controller", ""),
        nameof(DrivingLicenceCategoriesController).Replace("Controller", ""),
        nameof(FunnelResultsController).Replace("Controller", ""),
        nameof(GlobalZonesController).Replace("Controller", ""),
        nameof(LoadingMethodsController).Replace("Controller", ""),
        nameof(MeetingTypesController).Replace("Controller", ""),
        nameof(MeetingStatusesController).Replace("Controller", ""),
        nameof(MeetingResultsController).Replace("Controller", ""),
        nameof(MeetingPrioritiesController).Replace("Controller", ""),
        nameof(PackagingsController).Replace("Controller", ""),
        nameof(PricingTypesController).Replace("Controller", ""),
        nameof(RequestPurposesController).Replace("Controller", ""),
        nameof(RequestSourcesController).Replace("Controller", ""),
        nameof(ResultTypesController).Replace("Controller", ""),
        nameof(SalesFunnelStatusesController).Replace("Controller", ""),
        nameof(StatesController).Replace("Controller", ""),
        nameof(TaskPrioritiesController).Replace("Controller", ""),
        nameof(TaskStatusesController).Replace("Controller", ""),
        nameof(TransportTypesController).Replace("Controller", ""),
        nameof(UomsController).Replace("Controller", ""),
        nameof(WayOfNegotiationsController).Replace("Controller", ""),
    ];

    private static readonly Dictionary<string, Dictionary<string, string[]>> Map = CreateMap();

    private static Dictionary<string, Dictionary<string, string[]>> CreateMap()
    {
        var c = StringComparer.OrdinalIgnoreCase;
        string[] P(params string[] x) => x;

        var map = new Dictionary<string, Dictionary<string, string[]>>(c);

        foreach (var name in ClassifierControllerNames)
            map[name] = ClassifierCrud(c);

        map[nameof(QuoteSourcesController).Replace("Controller", "")] = QuoteSourcesCrud(c);

        map[nameof(CompaniesController).Replace("Controller", "")] = new Dictionary<string, string[]>(c)
        {
            ["GetAll"] = P(SettingsErpPermissionClaims.OrganizationView),
            ["GetById"] = P(SettingsErpPermissionClaims.OrganizationView),
            ["Create"] = P(SettingsErpPermissionClaims.OrganizationEditing),
            ["Update"] = P(SettingsErpPermissionClaims.OrganizationEditing),
            ["Delete"] = P(SettingsErpPermissionClaims.OrganizationEditing),
            ["UploadSignature"] = P(SettingsErpPermissionClaims.OrganizationEditing),
            ["GetSignatureFile"] = P(SettingsErpPermissionClaims.OrganizationView),
            ["DeleteSignature"] = P(SettingsErpPermissionClaims.OrganizationEditing),
        };

        foreach (var name in new[] { "Departments", "ExecutionPlaces", "WorkerPosts" })
            map[name] = OrganizationCrud(c);

        map[nameof(EmployeeGroupsController).Replace("Controller", "")] = new Dictionary<string, string[]>(c)
        {
            ["GetAll"] = P(SettingsErpPermissionClaims.OrganizationView),
            ["GetById"] = P(SettingsErpPermissionClaims.OrganizationView),
            ["Create"] = P(SettingsErpPermissionClaims.OrganizationEditing),
            ["Update"] = P(SettingsErpPermissionClaims.OrganizationEditing),
            ["Delete"] = P(SettingsErpPermissionClaims.OrganizationEditing),
            ["Clone"] = P(SettingsErpPermissionClaims.OrganizationEditing),
        };

        map[nameof(TemplatesController).Replace("Controller", "")] = new Dictionary<string, string[]>(c)
        {
            ["GetAll"] = P(SettingsErpPermissionClaims.TemplatesView),
            ["GetById"] = P(SettingsErpPermissionClaims.TemplatesView),
            ["Create"] = P(SettingsErpPermissionClaims.TemplatesEditing),
            ["Update"] = P(SettingsErpPermissionClaims.TemplatesEditing),
            ["Delete"] = P(SettingsErpPermissionClaims.TemplatesEditing),
        };

        map[nameof(GeneralSettingsController).Replace("Controller", "")] = new Dictionary<string, string[]>(c)
        {
            ["Get"] = P(SettingsErpPermissionClaims.SystemView),
            ["GetPriceDisplayTypes"] = P(SettingsErpPermissionClaims.SystemView),
            ["GetLogo"] = P(SettingsErpPermissionClaims.SystemView),
            ["Update"] = P(SettingsErpPermissionClaims.SystemEditing),
            ["UploadLogo"] = P(SettingsErpPermissionClaims.SystemEditing),
            ["DeleteLogo"] = P(SettingsErpPermissionClaims.SystemEditing),
        };

        map[nameof(NumerationsController).Replace("Controller", "")] = new Dictionary<string, string[]>(c)
        {
            ["GetAll"] = P(SettingsErpPermissionClaims.SystemView),
            ["GetNumerationForOptions"] = P(SettingsErpPermissionClaims.SystemView),
            ["GetFormulaElements"] = P(SettingsErpPermissionClaims.SystemView),
            ["GetById"] = P(SettingsErpPermissionClaims.SystemView),
            ["Preview"] = P(SettingsErpPermissionClaims.SystemEditing),
            ["Generate"] = P(SettingsErpPermissionClaims.SystemEditing),
            ["Create"] = P(SettingsErpPermissionClaims.SystemEditing),
            ["Update"] = P(SettingsErpPermissionClaims.SystemEditing),
            ["Delete"] = P(SettingsErpPermissionClaims.SystemEditing),
        };

        map[nameof(EmailSettingsController).Replace("Controller", "")] = new Dictionary<string, string[]>(c)
        {
            ["GetAll"] = P(SettingsErpPermissionClaims.SystemView),
            ["GetById"] = P(SettingsErpPermissionClaims.SystemView),
            ["Create"] = P(SettingsErpPermissionClaims.SystemEditing),
            ["LinkIdentityUser"] = P(SettingsErpPermissionClaims.SystemEditing),
            ["Update"] = P(SettingsErpPermissionClaims.SystemEditing),
            ["Delete"] = P(SettingsErpPermissionClaims.SystemEditing),
            ["TestMailbox"] = P(SettingsErpPermissionClaims.SystemEditing),
        };

        map[nameof(ActionLogsController).Replace("Controller", "")] = new Dictionary<string, string[]>(c)
        {
            ["GetPaged"] = P(SettingsErpPermissionClaims.SystemView),
            ["GetActions"] = P(SettingsErpPermissionClaims.SystemView),
        };

        map[nameof(MessageLogsController).Replace("Controller", "")] = new Dictionary<string, string[]>(c)
        {
            ["GetPaged"] = P(SettingsErpPermissionClaims.SystemView),
        };

        map[nameof(SessionLogsController).Replace("Controller", "")] = new Dictionary<string, string[]>(c)
        {
            ["Get"] = P(SettingsErpPermissionClaims.SystemView),
        };

        map[nameof(SystemLogsController).Replace("Controller", "")] = new Dictionary<string, string[]>(c)
        {
            ["GetLevels"] = P(SettingsErpPermissionClaims.SystemView),
            ["GetPaged"] = P(SettingsErpPermissionClaims.SystemView),
        };

        return map;
    }

    private static Dictionary<string, string[]> ClassifierCrud(StringComparer c) => new(c)
    {
        ["GetAll"] = [SettingsErpPermissionClaims.ClassifiersView],
        ["GetById"] = [SettingsErpPermissionClaims.ClassifiersView],
        ["Create"] = [SettingsErpPermissionClaims.ClassifiersEditing],
        ["Update"] = [SettingsErpPermissionClaims.ClassifiersEditing],
        ["Delete"] = [SettingsErpPermissionClaims.ClassifiersEditing],
    };

    private static Dictionary<string, string[]> OrganizationCrud(StringComparer c) => new(c)
    {
        ["GetAll"] = [SettingsErpPermissionClaims.OrganizationView],
        ["GetById"] = [SettingsErpPermissionClaims.OrganizationView],
        ["Create"] = [SettingsErpPermissionClaims.OrganizationEditing],
        ["Update"] = [SettingsErpPermissionClaims.OrganizationEditing],
        ["Delete"] = [SettingsErpPermissionClaims.OrganizationEditing],
    };

    private static Dictionary<string, string[]> QuoteSourcesCrud(StringComparer c) => new(c)
    {
        ["Create"] =
        [
            SettingsErpPermissionClaims.ClassifiersEditing,
            SettingsErpPermissionClaims.DataTransferViaApiRoleActivate,
        ],
        ["Update"] =
        [
            SettingsErpPermissionClaims.ClassifiersEditing,
            SettingsErpPermissionClaims.DataTransferViaApiRoleActivate,
        ],
        ["Delete"] =
        [
            SettingsErpPermissionClaims.ClassifiersEditing,
            SettingsErpPermissionClaims.DataTransferViaApiRoleActivate,
        ],
    };

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
