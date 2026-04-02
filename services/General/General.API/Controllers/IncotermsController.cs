using General.Application.DTOs.Incoterm;
using General.Application.Features.Incoterms.Commands.Create;
using General.Application.Features.Incoterms.Commands.Delete;
using General.Application.Features.Incoterms.Commands.Update;
using General.Application.Features.Incoterms.Queries.GetAll;
using General.Application.Features.Incoterms.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace General.API.Controllers;

[ApiController]
[Route("api/incoterms")]
public class IncotermsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<IncotermDto>>> GetAll(
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
        return Ok(await mediator.Send(new GetAllIncotermsQuery(active, deleted), ct));
    }

    [HttpGet("freight-options")]
    public ActionResult<string[]> GetFreightOptions() => Ok(new[] { "Collect", "Prepaid" });

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<IncotermDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetIncotermByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<IncotermDto>> Create([FromBody] CreateIncotermDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateIncotermCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<IncotermDto>> Update(Guid id, [FromBody] UpdateIncotermDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateIncotermCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, [FromQuery] bool softDelete, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteIncotermCommand(id, softDelete), ct);
        return found ? NoContent() : NotFound();
    }
}

