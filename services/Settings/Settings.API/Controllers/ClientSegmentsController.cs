using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.ClientSegment;
using Settings.Application.Features.ClientSegments.Commands.Create;
using Settings.Application.Features.ClientSegments.Commands.Delete;
using Settings.Application.Features.ClientSegments.Commands.Update;
using Settings.Application.Features.ClientSegments.Queries.GetAll;
using Settings.Application.Features.ClientSegments.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/clientsegments")]
public class ClientSegmentsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ClientSegmentDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllClientSegmentsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientSegmentDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetClientSegmentByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ClientSegmentDto>> Create([FromBody] CreateClientSegmentDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateClientSegmentCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ClientSegmentDto>> Update(Guid id, [FromBody] UpdateClientSegmentDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateClientSegmentCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteClientSegmentCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

