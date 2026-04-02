using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.Features.MessageLogs.Commands.Add;
using Settings.Application.Features.MessageLogs.Queries.GetPaged;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageLogsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<long>> Add([FromBody] AddMessageLogDto dto, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(dto.Sender)) return BadRequest("Sender is required.");
        if (string.IsNullOrWhiteSpace(dto.Receiver)) return BadRequest("Receiver is required.");
        if (string.IsNullOrWhiteSpace(dto.Theme)) return BadRequest("Theme is required.");
        var id = await mediator.Send(new AddMessageLogCommand(dto), ct);
        return Ok(id);
    }

    [HttpGet]
    public async Task<ActionResult<GetMessageLogsPagedResult>> GetPaged(
        [FromQuery] string? companyName,
        [FromQuery] string? receiver,
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken ct = default)
    {
        var result = await mediator.Send(new GetMessageLogsPagedQuery(
            companyName, receiver, dateFrom, dateTo, page, pageSize), ct);
        return Ok(result);
    }
}
