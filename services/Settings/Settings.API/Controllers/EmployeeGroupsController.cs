using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Settings.API.Options;
using Settings.Application.DTOs.EmployeeGroup;
using Settings.Application.Features.EmployeeGroups.Commands.Clone;
using Settings.Application.Features.EmployeeGroups.Commands.Create;
using Settings.Application.Features.EmployeeGroups.Commands.Delete;
using Settings.Application.Features.EmployeeGroups.Commands.Update;
using Settings.Application.Features.EmployeeGroups.Queries.GetAll;
using Settings.Application.Features.EmployeeGroups.Queries.GetById;
using Settings.Application.Features.EmployeeGroups.Queries.Resolve;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/employee-groups")]
public sealed class EmployeeGroupsController(IMediator mediator) : ControllerBase
{
    public const string ResolvePermissionsHeaderName = "X-AzShipping-Employee-Groups-Resolve-Key";

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] Guid? companyId, [FromQuery] string? search, CancellationToken ct)
        => Ok(await mediator.Send(new GetAllEmployeeGroupsQuery(companyId, search), ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetEmployeeGroupByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeGroupDto dto, CancellationToken ct)
    {
        try
        {
            var result = await mediator.Send(new CreateEmployeeGroupCommand(dto), ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeGroupDto dto, CancellationToken ct)
    {
        try
        {
            var result = await mediator.Send(new UpdateEmployeeGroupCommand(id, dto), ct);
            return result == null ? NotFound() : Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ok = await mediator.Send(new DeleteEmployeeGroupCommand(id), ct);
        return ok ? NoContent() : NotFound();
    }

    [HttpPost("{id:guid}/clone")]
    public async Task<IActionResult> Clone(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new CloneEmployeeGroupCommand(id), ct);
        return result == null ? NotFound() : CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>Merge employee-group permission JSON for the given ids into flattened claim strings (used by Identity at login).</summary>
    [HttpPost("resolve-permissions")]
    [AllowAnonymous]
    public async Task<IActionResult> ResolvePermissions(
        [FromBody] ResolveEmployeeGroupPermissionsRequestDto dto,
        [FromServices] IOptions<EmployeeGroupResolveOptions> resolveOptions,
        CancellationToken ct)
    {
        var expected = resolveOptions.Value.ApiKey;
        if (string.IsNullOrWhiteSpace(expected))
            return StatusCode(StatusCodes.Status503ServiceUnavailable, "EmployeeGroupResolve:ApiKey is not configured.");

        if (!Request.Headers.TryGetValue(ResolvePermissionsHeaderName, out var provided) || provided.ToString() != expected)
            return Unauthorized();

        var ids = dto.Ids ?? [];
        if (ids.Count == 0)
            return Ok(new ResolveEmployeeGroupPermissionsResponseDto([]));

        var result = await mediator.Send(new ResolveEmployeeGroupPermissionsQuery(ids.ToList()), ct);
        return Ok(result);
    }
}
