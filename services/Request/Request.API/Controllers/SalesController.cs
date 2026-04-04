using MediatR;
using Microsoft.AspNetCore.Mvc;
using Request.Application.DTOs.Sale;
using Request.Application.Features.Sales.Commands.Create;
using Request.Application.Features.Sales.Commands.Delete;
using Request.Application.Features.Sales.Commands.Update;
using Request.Application.Features.Sales.Queries.GetAll;
using Request.Application.Features.Sales.Queries.GetById;

namespace Request.API.Controllers;

[ApiController]
[Route("api/sales")]
public class SalesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SaleDto>>> GetAll([FromQuery] string? listStatus, CancellationToken ct)
        => Ok(await mediator.Send(new GetAllSalesQuery(listStatus), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SaleDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetSaleByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<SaleDto>> Create([FromBody] CreateSaleDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateSaleCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SaleDto>> Update(Guid id, [FromBody] UpdateSaleDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateSaleCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteSaleCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

