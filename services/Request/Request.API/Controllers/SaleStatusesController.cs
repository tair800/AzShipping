using MediatR;
using Microsoft.AspNetCore.Mvc;
using Request.Application.DTOs.SaleStatus;
using Request.Application.Features.SaleStatuses.Commands.Create;
using Request.Application.Features.SaleStatuses.Commands.Delete;
using Request.Application.Features.SaleStatuses.Commands.Update;
using Request.Application.Features.SaleStatuses.Queries.GetAll;
using Request.Application.Features.SaleStatuses.Queries.GetById;

namespace Request.API.Controllers;

[ApiController]
[Route("api/salestatuses")]
public class SaleStatusesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SaleStatusDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllSaleStatusesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SaleStatusDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetSaleStatusByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<SaleStatusDto>> Create([FromBody] CreateSaleStatusDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateSaleStatusCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SaleStatusDto>> Update(Guid id, [FromBody] UpdateSaleStatusDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateSaleStatusCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteSaleStatusCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

