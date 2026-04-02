using Carrier.Application.DTOs.CarrierDirection;
using Carrier.Application.Features.CarrierDirections.Commands.Create;
using Carrier.Application.Features.CarrierDirections.Commands.Delete;
using Carrier.Application.Features.CarrierDirections.Commands.Update;
using Carrier.Application.Features.CarrierDirections.Queries.GetByCarrierId;
using Carrier.Application.Features.CarrierDirections.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrier.API.Controllers;

[ApiController]
[Route("api/carriers/{carrierId:guid}/directions")]
public class CarrierDirectionsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CarrierDirectionDto>>> GetByCarrierId(Guid carrierId, CancellationToken ct)
        => Ok(await mediator.Send(new GetCarrierDirectionsQuery(carrierId), ct));

    [HttpGet("{id:guid}", Name = nameof(GetCarrierDirectionById))]
    public async Task<ActionResult<CarrierDirectionDto>> GetCarrierDirectionById(Guid carrierId, Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetCarrierDirectionByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CarrierDirectionDto>> Create(Guid carrierId, [FromBody] CreateCarrierDirectionDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateCarrierDirectionCommand(carrierId, dto), ct);
        return CreatedAtRoute(nameof(GetCarrierDirectionById), new { carrierId, id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CarrierDirectionDto>> Update(Guid carrierId, Guid id, [FromBody] UpdateCarrierDirectionDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateCarrierDirectionCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid carrierId, Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteCarrierDirectionCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

