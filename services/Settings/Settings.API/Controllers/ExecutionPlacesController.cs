using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.ExecutionPlace;
using Settings.Application.Features.ExecutionPlaces.Commands.Create;
using Settings.Application.Features.ExecutionPlaces.Commands.Delete;
using Settings.Application.Features.ExecutionPlaces.Commands.Update;
using Settings.Application.Features.ExecutionPlaces.Queries.GetAll;
using Settings.Application.Features.ExecutionPlaces.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/execution-places")]
public class ExecutionPlacesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ExecutionPlaceDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllExecutionPlacesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ExecutionPlaceDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetExecutionPlaceByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ExecutionPlaceDto>> Create([FromBody] CreateExecutionPlaceDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateExecutionPlaceCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ExecutionPlaceDto>> Update(Guid id, [FromBody] UpdateExecutionPlaceDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateExecutionPlaceCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteExecutionPlaceCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

