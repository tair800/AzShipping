using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.Features.ActionLogs.Commands.Add;
using Settings.Application.Features.ActionLogs.Queries.GetActions;
using Settings.Application.Features.ActionLogs.Queries.GetPaged;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActionLogsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<long>> Add([FromBody] AddActionLogDto dto, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(dto.Action)) return BadRequest("Action is required.");
        // Use X-Session-Id from request header when not in body (e.g. from Action Log test form)
        var sessionId = dto.SessionId ?? HttpContext.Request.Headers["X-Session-Id"].FirstOrDefault();
        var dtoWithSession = string.IsNullOrEmpty(dto.SessionId) && !string.IsNullOrEmpty(sessionId)
            ? dto with { SessionId = sessionId }
            : dto;
        var id = await mediator.Send(new AddActionLogCommand(dtoWithSession), ct);
        return Ok(id);
    }
    [HttpGet]
    public async Task<ActionResult<GetActionLogsPagedResult>> GetPaged(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] Guid? employeeId,
        [FromQuery] string? employeeName,
        [FromQuery] string? action,
        [FromQuery] string? orderFilter,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetActionLogsPagedQuery(
            dateFrom, dateTo, employeeId, employeeName, action, orderFilter, page, pageSize), ct);
        return Ok(result);
    }

    [HttpGet("actions")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetActions(CancellationToken ct = default)
    {
        var actions = await mediator.Send(new GetActionLogActionsQuery(), ct);
        return Ok(actions);
    }
}
