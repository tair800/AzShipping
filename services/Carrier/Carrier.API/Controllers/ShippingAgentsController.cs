using Carrier.Application.DTOs.ShippingAgent;
using Carrier.Application.Features.ShippingAgents.Commands.Create;
using Carrier.Application.Features.ShippingAgents.Commands.Delete;
using Carrier.Application.Features.ShippingAgents.Commands.Update;
using Carrier.Application.Features.ShippingAgents.Queries.GetAll;
using Carrier.Application.Features.ShippingAgents.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrier.API.Controllers;

[ApiController]
[Route("api/shippingagents")]
public class ShippingAgentsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ShippingAgentDto>>> GetAll([FromQuery] bool? isActive, CancellationToken ct)
        => Ok(await mediator.Send(new GetAllShippingAgentsQuery(isActive), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ShippingAgentDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetShippingAgentByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ShippingAgentDto>> Create([FromBody] CreateShippingAgentDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateShippingAgentCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ShippingAgentDto>> Update(Guid id, [FromBody] UpdateShippingAgentDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateShippingAgentCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteShippingAgentCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

