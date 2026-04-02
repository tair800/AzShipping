using Carrier.Application.DTOs.CarrierTask;
using Carrier.Application.Features.CarrierTasks.Commands.Create;
using Carrier.Application.Features.CarrierTasks.Commands.Delete;
using Carrier.Application.Features.CarrierTasks.Commands.Update;
using Carrier.Application.Features.CarrierTasks.Queries.GetById;
using Carrier.Application.Features.CarrierTasks.Queries.GetByCarrierId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrier.API.Controllers;

[ApiController]
[Route("api/carriers/{carrierId:guid}/tasks")]
public class CarrierTasksController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CarrierTaskDto>>> GetByCarrierId(Guid carrierId, CancellationToken ct)
        => Ok(await mediator.Send(new GetCarrierTasksQuery(carrierId), ct));

    [HttpGet("{id:guid}", Name = nameof(GetCarrierTaskById))]
    public async Task<ActionResult<CarrierTaskDto>> GetCarrierTaskById(Guid carrierId, Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetCarrierTaskByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CarrierTaskDto>> Create(Guid carrierId, [FromBody] CreateCarrierTaskDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateCarrierTaskCommand(carrierId, dto), ct);
        return CreatedAtRoute(nameof(GetCarrierTaskById), new { carrierId, id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CarrierTaskDto>> Update(Guid carrierId, Guid id, [FromBody] UpdateCarrierTaskDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateCarrierTaskCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid carrierId, Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteCarrierTaskCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

