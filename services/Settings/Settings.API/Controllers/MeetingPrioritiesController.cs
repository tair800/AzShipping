using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.MeetingPriority;
using Settings.Application.Features.MeetingPriorities.Commands.Create;
using Settings.Application.Features.MeetingPriorities.Commands.Delete;
using Settings.Application.Features.MeetingPriorities.Commands.Update;
using Settings.Application.Features.MeetingPriorities.Queries.GetAll;
using Settings.Application.Features.MeetingPriorities.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/meeting-priorities")]
public class MeetingPrioritiesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MeetingPriorityDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllMeetingPrioritiesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MeetingPriorityDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetMeetingPriorityByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<MeetingPriorityDto>> Create([FromBody] CreateMeetingPriorityDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateMeetingPriorityCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MeetingPriorityDto>> Update(Guid id, [FromBody] UpdateMeetingPriorityDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateMeetingPriorityCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteMeetingPriorityCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

