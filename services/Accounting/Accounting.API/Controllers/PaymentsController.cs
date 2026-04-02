using Accounting.Application.DTOs.Payment;
using Accounting.Application.Features.Payments.Commands.Create;
using Accounting.Application.Features.Payments.Queries.GetAll;
using Accounting.Application.Features.Payments.Queries.GetById;
using Accounting.Domain.AggregatesModel.PaymentAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers;

[ApiController]
[Route("api/payments")]
public class PaymentsController(IMediator mediator) : ControllerBase
{
    /// <summary>Outgoing payments to carriers/vendors (accounts payable — “payments made”).</summary>
    [HttpGet("made")]
    public async Task<ActionResult<IReadOnlyList<PaymentDto>>> GetPaymentsMade(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllPaymentsQuery(PaymentDirection.Outgoing), ct));

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PaymentDto>>> GetAll(
        [FromQuery] PaymentDirection? direction,
        CancellationToken ct)
        => Ok(await mediator.Send(new GetAllPaymentsQuery(direction), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PaymentDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetPaymentByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PaymentDto>> Create([FromBody] CreatePaymentDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreatePaymentCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
}
