using Clients.Application.DTOs.Direction;
using Clients.Application.Features.Directions.Commands.Create;
using Clients.Application.Features.Directions.Commands.Delete;
using Clients.Application.Features.Directions.Commands.Update;
using Clients.Application.Features.Directions.Queries.GetByClientId;
using Clients.Application.Features.Directions.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clients.API.Controllers;

[ApiController]
[Route("api/directions")]
public class DirectionsController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DirectionDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetDirectionByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("by-client/{clientId:guid}")]
    public async Task<ActionResult<IReadOnlyList<DirectionDto>>> GetByClientId(Guid clientId, CancellationToken ct)
        => Ok(await mediator.Send(new GetDirectionsByClientIdQuery(clientId), ct));

    [HttpPost]
    public async Task<ActionResult<DirectionDto>> Create([FromBody] CreateDirectionDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateDirectionCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<DirectionDto>> Update(Guid id, [FromBody] UpdateDirectionDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateDirectionCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteDirectionCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

