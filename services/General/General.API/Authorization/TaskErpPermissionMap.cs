using System.Diagnostics.CodeAnalysis;
using General.Domain.ErpPermissions;

namespace General.API.Authorization;

/// <summary>
/// Maps <see cref="Controllers.TasksController"/> actions to Task ERP claims.
/// </summary>
public static class TaskErpPermissionMap
{
    private static readonly Dictionary<string, Dictionary<string, string[]>> Map = CreateMap();

    private static Dictionary<string, Dictionary<string, string[]>> CreateMap()
    {
        var c = StringComparer.OrdinalIgnoreCase;
        string[] P(params string[] x) => x;

        return new Dictionary<string, Dictionary<string, string[]>>(c)
        {
            ["Tasks"] = new Dictionary<string, string[]>(c)
            {
                ["GetAll"] = P(TaskErpPermissionClaims.ViewTasks),
                ["GetById"] = P(TaskErpPermissionClaims.ViewTasks),
                ["Create"] = P(TaskErpPermissionClaims.EditingTasks),
                ["Update"] = P(TaskErpPermissionClaims.EditingTasks),
                ["Delete"] = P(TaskErpPermissionClaims.DeleteTasks),
                ["UploadDocument"] = P(TaskErpPermissionClaims.EditingTasks),
                ["DownloadDocument"] = P(TaskErpPermissionClaims.ViewTasks),
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
