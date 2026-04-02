using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.WayOfNegotiation;
using Settings.Application.Features.WayOfNegotiations;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/wayofnegotiations")]
public class WayOfNegotiationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<WayOfNegotiationDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllWayOfNegotiationsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WayOfNegotiationDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetWayOfNegotiationByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<WayOfNegotiationDto>> Create([FromBody] CreateWayOfNegotiationDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateWayOfNegotiationCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<WayOfNegotiationDto>> Update(Guid id, [FromBody] UpdateWayOfNegotiationDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateWayOfNegotiationCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteWayOfNegotiationCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

