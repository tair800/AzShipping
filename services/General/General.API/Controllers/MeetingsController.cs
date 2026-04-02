using General.Application.DTOs.Meeting;
using General.Application.DTOs.MeetingHistory;
using General.Application.Features.MeetingHistories.Queries.GetByMeetingId;
using General.Application.Features.Meetings.Commands.Create;
using General.Application.Features.Meetings.Commands.Delete;
using General.Application.Features.Meetings.Commands.Update;
using General.Application.Features.Meetings.Commands.UpdateStatus;
using General.Application.Features.Meetings.Queries.GetAll;
using General.Application.Features.Meetings.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace General.API.Controllers;

public record UpdateMeetingStatusRequest(Guid? MeetingStatusId);

[ApiController]
[Route("api/meetings")]
public class MeetingsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MeetingDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllMeetingsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MeetingDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetMeetingByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("{id:guid}/histories")]
    public async Task<ActionResult<IReadOnlyList<MeetingHistoryDto>>> GetHistories(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new GetMeetingHistoriesByMeetingIdQuery(id), ct));

    [HttpPost]
    public async Task<ActionResult<MeetingDto>> Create([FromBody] CreateMeetingDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateMeetingCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MeetingDto>> Update(Guid id, [FromBody] UpdateMeetingDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateMeetingCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<ActionResult<MeetingDto>> UpdateStatus(Guid id, [FromBody] UpdateMeetingStatusRequest request, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateMeetingStatusCommand(id, request.MeetingStatusId), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteMeetingCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

