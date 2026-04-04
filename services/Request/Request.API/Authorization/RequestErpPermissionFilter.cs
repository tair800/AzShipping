using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Request.API.Authorization;

/// <summary>
/// Applies <see cref="RequestErpPermissionMap"/> to controller actions. Skips <see cref="AllowAnonymous"/> actions.
/// </summary>
public sealed class RequestErpPermissionFilter : IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context.ActionDescriptor is not ControllerActionDescriptor cad)
            return Task.CompletedTask;

        if (AllowsAnonymous(cad))
            return Task.CompletedTask;

        if (!RequestErpPermissionMap.TryGetRequiredPermissions(cad.ControllerName, cad.ActionName, out var anyOf))
            return Task.CompletedTask;

        return RequestErpPermissionEvaluator.ApplyAsync(context, anyOf);
    }

    private static bool AllowsAnonymous(ControllerActionDescriptor cad)
    {
        if (cad.MethodInfo.GetCustomAttribute<AllowAnonymousAttribute>(true) != null)
            return true;
        if (cad.ControllerTypeInfo.GetCustomAttribute<AllowAnonymousAttribute>(true) != null)
            return true;
        return false;
    }
}
