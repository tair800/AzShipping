using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.DeferredPaymentCondition;
using Settings.Application.Features.DeferredPaymentConditions;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/deferredpaymentconditions")]
public class DeferredPaymentConditionsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DeferredPaymentConditionDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllDeferredPaymentConditionsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DeferredPaymentConditionDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetDeferredPaymentConditionByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<DeferredPaymentConditionDto>> Create([FromBody] CreateDeferredPaymentConditionDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateDeferredPaymentConditionCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<DeferredPaymentConditionDto>> Update(Guid id, [FromBody] UpdateDeferredPaymentConditionDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateDeferredPaymentConditionCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteDeferredPaymentConditionCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

