using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.MeetingStatus;
using Settings.Application.Features.MeetingStatuses.Commands.Create;
using Settings.Application.Features.MeetingStatuses.Commands.Delete;
using Settings.Application.Features.MeetingStatuses.Commands.Update;
using Settings.Application.Features.MeetingStatuses.Queries.GetAll;
using Settings.Application.Features.MeetingStatuses.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/meeting-statuses")]
public class MeetingStatusesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MeetingStatusDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllMeetingStatusesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MeetingStatusDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetMeetingStatusByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<MeetingStatusDto>> Create([FromBody] CreateMeetingStatusDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateMeetingStatusCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MeetingStatusDto>> Update(Guid id, [FromBody] UpdateMeetingStatusDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateMeetingStatusCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteMeetingStatusCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

