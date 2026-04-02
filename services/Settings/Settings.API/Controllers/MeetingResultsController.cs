using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.MeetingResult;
using Settings.Application.Features.MeetingResults.Commands.Create;
using Settings.Application.Features.MeetingResults.Commands.Delete;
using Settings.Application.Features.MeetingResults.Commands.Update;
using Settings.Application.Features.MeetingResults.Queries.GetAll;
using Settings.Application.Features.MeetingResults.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/meeting-results")]
public class MeetingResultsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MeetingResultDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllMeetingResultsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MeetingResultDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetMeetingResultByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<MeetingResultDto>> Create([FromBody] CreateMeetingResultDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateMeetingResultCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MeetingResultDto>> Update(Guid id, [FromBody] UpdateMeetingResultDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateMeetingResultCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteMeetingResultCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

