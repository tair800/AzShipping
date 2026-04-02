using Carrier.Application.DTOs.Carrier;
using Carrier.Application.Features.Carriers.Commands.Create;
using Carrier.Application.Features.Carriers.Commands.Delete;
using Carrier.Application.Features.Carriers.Commands.Update;
using Carrier.Application.Features.Carriers.Queries.GetAll;
using Carrier.Application.Features.Carriers.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrier.API.Controllers;

[ApiController]
[Route("api/carriers")]
public class CarriersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CarrierDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllCarriersQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CarrierDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetCarrierByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CarrierDto>> Create([FromBody] CreateCarrierDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateCarrierCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CarrierDto>> Update(Guid id, [FromBody] UpdateCarrierDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateCarrierCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteCarrierCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

