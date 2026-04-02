using Accounting.Application.DTOs.OperationInvoice;
using Accounting.Application.Features.OperationInvoices.Commands.Calculate;
using Accounting.Application.Features.OperationInvoices.Commands.Create;
using Accounting.Application.Features.OperationInvoices.Commands.Delete;
using Accounting.Application.Features.OperationInvoices.Commands.Update;
using Accounting.Application.Features.OperationInvoices.Queries.GetAllForList;
using Accounting.Application.Features.OperationInvoices.Queries.GetById;
using Accounting.Application.Features.OperationInvoices.Queries.GetByOperation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers;

[ApiController]
[Route("api/operation-invoices")]
public class OperationInvoicesController(IMediator mediator) : ControllerBase
{
    [HttpGet("list")]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyList<OperationInvoiceListItemDto>>> ListAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllOperationInvoicesForListQuery(), ct));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OperationInvoiceDto>>> GetByOperation(
        [FromQuery] Guid operationId,
        CancellationToken ct)
    {
        if (operationId == Guid.Empty)
            return BadRequest("operationId is required.");
        return Ok(await mediator.Send(new GetOperationInvoicesByOperationQuery(operationId), ct));
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<OperationInvoiceDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetOperationInvoiceByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<OperationInvoiceDto>> Create([FromBody] CreateOperationInvoiceDto dto, CancellationToken ct)
    {
        var created = await mediator.Send(new CreateOperationInvoiceCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<OperationInvoiceDto>> Update(Guid id, [FromBody] UpdateOperationInvoiceDto dto,
        CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateOperationInvoiceCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ok = await mediator.Send(new DeleteOperationInvoiceCommand(id), ct);
        return ok ? NoContent() : NotFound();
    }

    [HttpPost("calculate")]
    [AllowAnonymous]
    public async Task<ActionResult<CalculateOperationInvoiceResponseDto>> Calculate(
        [FromBody] CalculateOperationInvoiceRequestDto body,
        CancellationToken ct)
        => Ok(await mediator.Send(new CalculateOperationInvoiceCommand(body), ct));
}
