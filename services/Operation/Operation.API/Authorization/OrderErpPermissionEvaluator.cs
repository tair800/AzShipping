using System.Net.Mime;
using System.Text.Json;
using AzShipping.ApiSecurity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Operation.API.Authorization;

internal static class OrderErpPermissionEvaluator
{
    private static readonly JsonSerializerOptions JsonErr = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public static Task ApplyAsync(AuthorizationFilterContext context, IReadOnlyList<string> anyOf)
    {
        var user = context.HttpContext.User;
        if (user?.Identity?.IsAuthenticated != true)
            return Task.CompletedTask;

        if (user.HasClaim(ErpClaimTypes.Unlimited, "1"))
            return Task.CompletedTask;

        var erp = user.FindAll(ErpClaimTypes.Permission).Select(c => c.Value).ToList();
        if (erp.Count == 0)
            return Task.CompletedTask;

        if (anyOf.Count == 0 || anyOf.Any(erp.Contains))
            return Task.CompletedTask;

        context.Result = new JsonResult(
            new { error = "erp_permission_forbidden", message = "Insufficient Orders or Warehouse module permissions for this action." },
            JsonErr)
        {
            StatusCode = StatusCodes.Status403Forbidden,
            ContentType = MediaTypeNames.Application.Json
        };
        return Task.CompletedTask;
    }
}
