using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.MeetingType;
using Settings.Application.Features.MeetingTypes.Commands.Create;
using Settings.Application.Features.MeetingTypes.Commands.Delete;
using Settings.Application.Features.MeetingTypes.Commands.Update;
using Settings.Application.Features.MeetingTypes.Queries.GetAll;
using Settings.Application.Features.MeetingTypes.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/meeting-types")]
public class MeetingTypesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MeetingTypeDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllMeetingTypesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MeetingTypeDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetMeetingTypeByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<MeetingTypeDto>> Create([FromBody] CreateMeetingTypeDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateMeetingTypeCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MeetingTypeDto>> Update(Guid id, [FromBody] UpdateMeetingTypeDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateMeetingTypeCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteMeetingTypeCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

