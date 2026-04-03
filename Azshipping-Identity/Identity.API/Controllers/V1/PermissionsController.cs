using Asp.Versioning;
using Identity.Application.DTOs.Permission;
using Identity.Application.Features.Permissions.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Azshipping.Auth;
using MrStyx.API.Auth;
using MrStyx.API.Common;

namespace Identity.API.Controllers.V1;


[ApiController]
[ApiVersion("1.0")]
[Route("[controller]")]
[RoleOrPermissionAuthorize([Roles.Admin, Roles.HR], [Permissions.PermissionsView])]
public class PermissionsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("get/all")]
    public async Task<ApiResponse<PermissionList>> GetAllPermissionsAsync()
    {
        var result = await _mediator.Send(new GetAllPermissionsQuery());
        return ApiResponse<PermissionList>.Ok(result, "Permissions retrieved successfully", traceId: HttpContext.TraceIdentifier);
    }
}