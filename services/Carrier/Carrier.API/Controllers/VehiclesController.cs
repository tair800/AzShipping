using Carrier.Application.DTOs.Vehicle;
using Carrier.Application.Features.Vehicles.Commands.Create;
using Carrier.Application.Features.Vehicles.Commands.Delete;
using Carrier.Application.Features.Vehicles.Commands.Update;
using Carrier.Application.Features.Vehicles.Queries.GetAll;
using Carrier.Application.Features.Vehicles.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrier.API.Controllers;

[ApiController]
[Route("api/vehicles")]
public class VehiclesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<VehicleDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllVehiclesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VehicleDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetVehicleByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<VehicleDto>> Create([FromBody] CreateVehicleDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateVehicleCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<VehicleDto>> Update(Guid id, [FromBody] UpdateVehicleDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateVehicleCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteVehicleCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

