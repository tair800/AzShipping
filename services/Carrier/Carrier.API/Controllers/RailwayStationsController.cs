using Carrier.Application.DTOs.RailwayStation;
using Carrier.Application.Features.RailwayStations.Commands.Create;
using Carrier.Application.Features.RailwayStations.Commands.Delete;
using Carrier.Application.Features.RailwayStations.Commands.Update;
using Carrier.Application.Features.RailwayStations.Queries.GetAll;
using Carrier.Application.Features.RailwayStations.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrier.API.Controllers;

[ApiController]
[Route("api/railwaystations")]
public class RailwayStationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RailwayStationDto>>> GetAll([FromQuery] bool? isActive, CancellationToken ct)
        => Ok(await mediator.Send(new GetAllRailwayStationsQuery(isActive), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RailwayStationDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetRailwayStationByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<RailwayStationDto>> Create([FromBody] CreateRailwayStationDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateRailwayStationCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RailwayStationDto>> Update(Guid id, [FromBody] UpdateRailwayStationDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateRailwayStationCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteRailwayStationCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

