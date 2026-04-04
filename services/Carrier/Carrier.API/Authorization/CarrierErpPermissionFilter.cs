using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Carrier.API.Authorization;

/// <summary>
/// Applies <see cref="CarrierErpPermissionMap"/>. Skips <see cref="AllowAnonymous"/> actions/controllers.
/// </summary>
public sealed class CarrierErpPermissionFilter : IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context.ActionDescriptor is not ControllerActionDescriptor cad)
            return Task.CompletedTask;

        if (AllowsAnonymous(cad))
            return Task.CompletedTask;

        if (!CarrierErpPermissionMap.TryGetRequiredPermissions(cad.ControllerName, cad.ActionName, out var anyOf))
            return Task.CompletedTask;

        return CarrierErpPermissionEvaluator.ApplyAsync(context, anyOf);
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
