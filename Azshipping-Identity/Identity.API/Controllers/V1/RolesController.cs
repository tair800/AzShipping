using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Identity.Application.DTOs.Role;
using Identity.Application.Features.Roles.Queries.GetAll;
using Identity.Application.Features.Roles.Queries.GetById;
using Identity.Application.Features.Roles.Commands.Create;
using Identity.Application.Features.Roles.Commands.Update;
using Identity.Application.Features.Roles.Commands.Delete;
using Identity.Application.Features.Roles.Queries.GetWhere;
using Identity.Application.Features.Roles.Queries.GetPaged;
using Identity.Application.Features.Roles.Queries.GetPagedWhere;
using MrStyx.API.Common;

namespace Identity.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("[controller]")]
public class RolesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("get/all")]
    public async Task<ApiResponse<RoleList>> GetAllRolesAsync()
    {
        var result = await _mediator.Send(new GetAllRolesQuery());
        return ApiResponse<RoleList>.Ok(result, "Roles retrieved successfully", traceId: HttpContext.TraceIdentifier);
    }

    [HttpGet("get/by/{id}")]
    public async Task<ApiResponse<RoleDto>> GetRoleByIdAsync(long id)
    {
        var result = await _mediator.Send(new GetRoleByIdQuery(id));
        return ApiResponse<RoleDto>.Ok(result, "Role retrieved successfully", traceId: HttpContext.TraceIdentifier);
    }

    [HttpPost("create")]
    public async Task<ApiResponse<RoleDto>> CreateRoleAsync(CreateRoleDto dto)
    {
        var result = await _mediator.Send(new CreateRoleCommand(dto));
        return ApiResponse<RoleDto>.Ok(result, "Role created successfully", traceId: HttpContext.TraceIdentifier);
    }

    [HttpPut("update")]
    public async Task<ApiResponse<RoleDto>> UpdateRoleAsync(UpdateRoleDto dto)
    {
        var result = await _mediator.Send(new UpdateRoleCommand(dto));
        return ApiResponse<RoleDto>.Ok(result, "Role updated successfully", traceId: HttpContext.TraceIdentifier);
    }

    [HttpDelete("delete/{id}")]
    public async Task<ApiResponse<bool>> DeleteRolesAsync(long id)
    {
        var result = await _mediator.Send(new DeleteRoleCommand(id));
        return ApiResponse<bool>.Ok(result, "Role deleted successfully", traceId: HttpContext.TraceIdentifier);
    }

    [HttpGet("get/where")]
    public async Task<ApiResponse<RoleList>> GetWhereAsync([FromQuery] SearchRoleDto dto)
    {
        var result = await _mediator.Send(new GetRolesWhereQuery(dto));
        return ApiResponse<RoleList>.Ok(result, "Roles retrieved successfully", traceId: HttpContext.TraceIdentifier);
    }

    [HttpGet("get/paged")]
    public async Task<ApiResponse<RoleList>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var result = await _mediator.Send(new GetPagedRolesQuery(pageNumber, pageSize));
        return ApiResponse<RoleList>.Ok(result.Items, "Roles retrieved successfully", traceId: HttpContext.TraceIdentifier, meta: result.Meta);
    }

    [HttpGet("get/paged/where")]
    public async Task<ApiResponse<RoleList>> GetPagedWhereAsync(int pageNumber, int pageSize, [FromQuery] SearchRoleDto dto)
    {
        var result = await _mediator.Send(new GetPagedRolesWhereQuery(pageNumber, pageSize, dto));
        return ApiResponse<RoleList>.Ok(result.Items, "Roles retrieved successfully", traceId: HttpContext.TraceIdentifier, meta: result.Meta);
    }
}