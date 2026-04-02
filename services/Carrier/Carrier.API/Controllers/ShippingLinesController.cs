using Carrier.Application.DTOs.ShippingLine;
using Carrier.Application.Features.ShippingLines.Commands.Create;
using Carrier.Application.Features.ShippingLines.Commands.Delete;
using Carrier.Application.Features.ShippingLines.Commands.Update;
using Carrier.Application.Features.ShippingLines.Queries.GetAll;
using Carrier.Application.Features.ShippingLines.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrier.API.Controllers;

[ApiController]
[Route("api/shippinglines")]
public class ShippingLinesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ShippingLineDto>>> GetAll([FromQuery] bool? isActive, CancellationToken ct)
        => Ok(await mediator.Send(new GetAllShippingLinesQuery(isActive), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ShippingLineDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetShippingLineByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ShippingLineDto>> Create([FromBody] CreateShippingLineDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateShippingLineCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ShippingLineDto>> Update(Guid id, [FromBody] UpdateShippingLineDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateShippingLineCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteShippingLineCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

