using General.Application.DTOs.Vas;
using General.Application.Features.Vas.Commands.Create;
using General.Application.Features.Vas.Commands.Delete;
using General.Application.Features.Vas.Commands.Update;
using General.Application.Features.Vas.Queries.GetAll;
using General.Application.Features.Vas.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace General.API.Controllers;

[ApiController]
[Route("api/vas")]
public class VasController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<VasDto>>> GetAll(
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
        return Ok(await mediator.Send(new GetAllVasQuery(active, deleted), ct));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VasDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetVasByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<VasDto>> Create([FromBody] CreateVasDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateVasCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<VasDto>> Update(Guid id, [FromBody] UpdateVasDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateVasCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, [FromQuery] bool softDelete, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteVasCommand(id, softDelete), ct);
        return found ? NoContent() : NotFound();
    }
}

