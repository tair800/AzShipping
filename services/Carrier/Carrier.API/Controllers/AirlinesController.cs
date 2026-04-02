using Carrier.Application.DTOs.Airline;
using Carrier.Application.Features.Airlines.Commands.Create;
using Carrier.Application.Features.Airlines.Commands.Delete;
using Carrier.Application.Features.Airlines.Commands.Update;
using Carrier.Application.Features.Airlines.Queries.GetAll;
using Carrier.Application.Features.Airlines.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrier.API.Controllers;

[ApiController]
[Route("api/airlines")]
public class AirlinesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AirlineDto>>> GetAll([FromQuery] bool? isActive, CancellationToken ct)
        => Ok(await mediator.Send(new GetAllAirlinesQuery(isActive), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AirlineDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetAirlineByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<AirlineDto>> Create([FromBody] CreateAirlineDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateAirlineCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<AirlineDto>> Update(Guid id, [FromBody] UpdateAirlineDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateAirlineCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteAirlineCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

