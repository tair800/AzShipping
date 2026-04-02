using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.TransportType;
using Settings.Application.Features.TransportTypes;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/transporttypes")]
public class TransportTypesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TransportTypeDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllTransportTypesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TransportTypeDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetTransportTypeByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TransportTypeDto>> Create([FromBody] CreateTransportTypeDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateTransportTypeCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TransportTypeDto>> Update(Guid id, [FromBody] UpdateTransportTypeDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateTransportTypeCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteTransportTypeCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

