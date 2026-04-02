using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace AzShipping.ApiSecurity;

public sealed class ErpModuleAccessMiddleware(RequestDelegate next, IOptions<ErpModuleAccessOptions> options)
{
    private static readonly JsonSerializerOptions JsonErr = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public async Task InvokeAsync(HttpContext context)
    {
        var opt = options.Value;
        if (!opt.Enabled || opt.ModulePrefixes is not { Length: > 0 })
        {
            await next(context);
            return;
        }

        if (HttpMethods.IsOptions(context.Request.Method))
        {
            await next(context);
            return;
        }

        var path = context.Request.Path.Value ?? "";
        if (ShouldSkipPath(path))
        {
            await next(context);
            return;
        }

        if (context.User.Identity?.IsAuthenticated != true)
        {
            await next(context);
            return;
        }

        if (context.User.HasClaim(ErpClaimTypes.Unlimited, "1"))
        {
            await next(context);
            return;
        }

        var erpClaims = context.User.FindAll(ErpClaimTypes.Permission).Select(c => c.Value).ToList();
        if (erpClaims.Count == 0)
        {
            await next(context);
            return;
        }

        var prefixes = opt.ModulePrefixes;
        var allowed = erpClaims.Any(v =>
            prefixes.Any(p =>
                v.Equals(p, StringComparison.Ordinal)
                || v.StartsWith(p + ".", StringComparison.Ordinal)
                || v.StartsWith(p + "=", StringComparison.Ordinal)));

        if (!allowed)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = MediaTypeNames.Application.Json;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                error = "erp_module_forbidden",
                message = "You do not have employee-group access to this service."
            }, JsonErr));
            return;
        }

        await next(context);
    }

    private static bool ShouldSkipPath(string path)
    {
        if (path.Length == 0 || path == "/")
            return true;
        if (path.Equals("/health", StringComparison.OrdinalIgnoreCase))
            return true;
        return path.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase);
    }
}
