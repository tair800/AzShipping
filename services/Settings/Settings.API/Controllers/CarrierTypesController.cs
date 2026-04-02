using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.CarrierType;
using Settings.Application.Features.CarrierTypes.Commands.Create;
using Settings.Application.Features.CarrierTypes.Commands.Delete;
using Settings.Application.Features.CarrierTypes.Commands.Update;
using Settings.Application.Features.CarrierTypes.Queries.GetAll;
using Settings.Application.Features.CarrierTypes.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/carriertypes")]
public class CarrierTypesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CarrierTypeDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllCarrierTypesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CarrierTypeDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetCarrierTypeByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CarrierTypeDto>> Create([FromBody] CreateCarrierTypeDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateCarrierTypeCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CarrierTypeDto>> Update(Guid id, [FromBody] UpdateCarrierTypeDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateCarrierTypeCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteCarrierTypeCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

