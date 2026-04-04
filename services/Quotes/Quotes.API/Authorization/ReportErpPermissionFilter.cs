using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Quotes.API.Authorization;

/// <summary>
/// Applies <see cref="ReportErpPermissionMap"/>. Skips <see cref="AllowAnonymous"/> actions/controllers.
/// </summary>
public sealed class ReportErpPermissionFilter : IAsyncAuthorizationFilter
{
    public Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context.ActionDescriptor is not ControllerActionDescriptor cad)
            return Task.CompletedTask;

        if (AllowsAnonymous(cad))
            return Task.CompletedTask;

        if (!ReportErpPermissionMap.TryGetRequiredPermissions(cad.ControllerName, cad.ActionName, out var anyOf))
            return Task.CompletedTask;

        return ReportErpPermissionEvaluator.ApplyAsync(context, anyOf);
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
