using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.GlobalZone;
using Settings.Application.Features.GlobalZones;
using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GlobalZonesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<GlobalZoneDto>>> GetAll([FromQuery] string? status = null)
    {
        EntityStatus? statusEnum = null;
        if (!string.IsNullOrEmpty(status) && Enum.TryParse<EntityStatus>(status, true, out var s))
            statusEnum = s;
        var result = await mediator.Send(new GetAllGlobalZonesQuery(statusEnum));
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GlobalZoneDto>> GetById(Guid id)
    {
        var result = await mediator.Send(new GetGlobalZoneByIdQuery(id));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GlobalZoneDto>> Create([FromBody] CreateGlobalZoneDto dto)
    {
        var result = await mediator.Send(new CreateGlobalZoneCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<GlobalZoneDto>> Update(Guid id, [FromBody] UpdateGlobalZoneDto dto)
    {
        var result = await mediator.Send(new UpdateGlobalZoneCommand(id, dto));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteGlobalZoneCommand(id));
        if (!result) return NotFound();
        return NoContent();
    }
}

