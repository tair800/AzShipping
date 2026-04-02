using MediatR;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.Features.SessionLogs.Queries.GetSessionLogs;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionLogsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<GetSessionLogsResult>> Get(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] Guid? employeeId,
        [FromQuery] string? employeeName,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetSessionLogsQuery(dateFrom, dateTo, employeeId, employeeName), ct);
        return Ok(result);
    }
}
