using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.Uom;
using Settings.Application.Features.Uoms.Commands.Create;
using Settings.Application.Features.Uoms.Commands.Delete;
using Settings.Application.Features.Uoms.Commands.Update;
using Settings.Application.Features.Uoms.Queries.GetAll;
using Settings.Application.Features.Uoms.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/uoms")]
public class UomsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UomDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllUomsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UomDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetUomByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<UomDto>> Create([FromBody] CreateUomDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateUomCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UomDto>> Update(Guid id, [FromBody] UpdateUomDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateUomCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteUomCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

