using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.SalesFunnelStatus;
using Settings.Application.Features.SalesFunnelStatuses.Commands.Create;
using Settings.Application.Features.SalesFunnelStatuses.Commands.Delete;
using Settings.Application.Features.SalesFunnelStatuses.Commands.Update;
using Settings.Application.Features.SalesFunnelStatuses.Queries.GetAll;
using Settings.Application.Features.SalesFunnelStatuses.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/salesfunnelstatuses")]
public class SalesFunnelStatusesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SalesFunnelStatusDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllSalesFunnelStatusesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SalesFunnelStatusDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetSalesFunnelStatusByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<SalesFunnelStatusDto>> Create([FromBody] CreateSalesFunnelStatusDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateSalesFunnelStatusCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SalesFunnelStatusDto>> Update(Guid id, [FromBody] UpdateSalesFunnelStatusDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateSalesFunnelStatusCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteSalesFunnelStatusCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

