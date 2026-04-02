using MediatR;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.Features.SystemLogs.Queries.GetPaged;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/system-logs")]
public class SystemLogsController(IMediator mediator) : ControllerBase
{
    [HttpGet("levels")]
    public ActionResult<string[]> GetLevels() => Ok(new[] { "Information", "Warning", "Error", "Debug" });

    [HttpGet]
    public async Task<ActionResult<GetSystemLogsPagedResult>> GetPaged(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? name,
        [FromQuery] string? level,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken ct = default)
    {
        if (page < 1) page = 1;
        if (pageSize < 1 || pageSize > 500) pageSize = 50;
        var result = await mediator.Send(new GetSystemLogsPagedQuery(dateFrom, dateTo, name, level, page, pageSize), ct);
        return Ok(result);
    }
}
