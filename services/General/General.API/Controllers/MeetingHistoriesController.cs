using General.Application.DTOs.MeetingHistory;
using General.Application.Features.MeetingHistories.Commands.Create;
using General.Application.Features.MeetingHistories.Queries.GetByMeetingId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace General.API.Controllers;

[ApiController]
[Route("api/meeting-histories")]
public class MeetingHistoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MeetingHistoryDto>>> GetByMeetingId([FromQuery] Guid meetingId, CancellationToken ct)
    {
        if (meetingId == Guid.Empty)
            return BadRequest("meetingId is required");
        return Ok(await mediator.Send(new GetMeetingHistoriesByMeetingIdQuery(meetingId), ct));
    }

    [HttpGet("by-meeting/{meetingId:guid}")]
    public async Task<ActionResult<IReadOnlyList<MeetingHistoryDto>>> GetByMeetingIdNested(Guid meetingId, CancellationToken ct)
        => Ok(await mediator.Send(new GetMeetingHistoriesByMeetingIdQuery(meetingId), ct));

    [HttpPost]
    public async Task<ActionResult<MeetingHistoryDto>> Create([FromBody] CreateMeetingHistoryDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateMeetingHistoryCommand(dto), ct);
        return Created($"/api/meeting-histories?meetingId={result.MeetingId}", result);
    }
}

