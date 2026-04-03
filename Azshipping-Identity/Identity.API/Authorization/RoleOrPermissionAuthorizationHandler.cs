using Identity.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using MrStyx.API.Auth.Requirements;

namespace Identity.API.Authorization;

public sealed class RoleOrPermissionAuthorizationHandler(IPermissionReadService permissionReadService) : AuthorizationHandler<RoleOrPermissionRequirement>
{
    private readonly IPermissionReadService _permissionReadService = permissionReadService;

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleOrPermissionRequirement requirement)
    {
        //Role
        if (requirement.Roles.Any() && requirement.Roles.Any(context.User.IsInRole))
        {
            context.Succeed(requirement);
            return;
        }

        //Permission in Token
        if (requirement.Permissions.Any() && context.User.Claims.Any(c => c.Type == "permission" && requirement.Permissions.Contains(c.Value)))
        {
            context.Succeed(requirement);
            return;
        }

        var uid = context.User.FindFirst("uid")?.Value;

        //Permission from database
        if (requirement.Permissions.Any() && long.TryParse(uid, out var userId))
        {
            foreach (var permission in requirement.Permissions)
            {
                if (await _permissionReadService.UserHasPermissionAsync(userId, permission))
                {
                    context.Succeed(requirement);
                    return;
                }
            }
        }
    }
}