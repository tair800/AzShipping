using General.Application.DTOs.Vessel;
using General.Application.Features.Vessels.Commands.Create;
using General.Application.Features.Vessels.Commands.Delete;
using General.Application.Features.Vessels.Commands.Update;
using General.Application.Features.Vessels.Queries.GetAll;
using General.Application.Features.Vessels.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace General.API.Controllers;

[ApiController]
[Route("api/vessels")]
public class VesselsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<VesselDto>>> GetAll(
        [FromQuery] bool? isActive,
        [FromQuery] bool? isDeleted,
        [FromQuery] string? status,
        CancellationToken ct)
    {
        bool? active = isActive;
        bool? deleted = isDeleted;
        if (!string.IsNullOrEmpty(status))
        {
            var s = status.ToLowerInvariant();
            if (s == "active") { active = true; deleted = false; }
            else if (s == "deactive") { active = false; deleted = false; }
            else if (s == "deleted") { deleted = true; }
        }
        return Ok(await mediator.Send(new GetAllVesselsQuery(active, deleted), ct));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VesselDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetVesselByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<VesselDto>> Create([FromBody] CreateVesselDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateVesselCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<VesselDto>> Update(Guid id, [FromBody] UpdateVesselDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateVesselCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, [FromQuery] bool softDelete, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteVesselCommand(id, softDelete), ct);
        return found ? NoContent() : NotFound();
    }
}

