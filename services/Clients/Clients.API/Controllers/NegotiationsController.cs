using Clients.Application.DTOs.Negotiation;
using Clients.Application.Features.Negotiations.Commands.Create;
using Clients.Application.Features.Negotiations.Commands.Delete;
using Clients.Application.Features.Negotiations.Commands.Update;
using Clients.Application.Features.Negotiations.Queries.GetByClientId;
using Clients.Application.Features.Negotiations.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clients.API.Controllers;

[ApiController]
[Route("api/negotiations")]
public class NegotiationsController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NegotiationDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetNegotiationByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("by-client/{clientId:guid}")]
    public async Task<ActionResult<IReadOnlyList<NegotiationDto>>> GetByClientId(Guid clientId, CancellationToken ct)
        => Ok(await mediator.Send(new GetNegotiationsByClientIdQuery(clientId), ct));

    [HttpPost]
    public async Task<ActionResult<NegotiationDto>> Create([FromBody] CreateNegotiationDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateNegotiationCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<NegotiationDto>> Update(Guid id, [FromBody] UpdateNegotiationDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateNegotiationCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteNegotiationCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

