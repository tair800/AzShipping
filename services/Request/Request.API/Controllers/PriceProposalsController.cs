using Accounting.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Request.Application.DTOs.PriceProposal;
using Request.Application.Features.PriceProposals.Commands.Create;
using Request.Application.Features.PriceProposals.Commands.Delete;
using Request.Application.Features.PriceProposals.Commands.Update;
using Request.Application.Features.PriceProposals.Queries.GetById;
using Request.Application.Features.PriceProposals.Queries.GetByRequestId;
using Request.Application.Services;

namespace Request.API.Controllers;

[ApiController]
[Route("api/price-proposals")]
public class PriceProposalsController(IMediator mediator, IVatRateLookupService vatRateLookup) : ControllerBase
{
    /// <summary>
    /// Calculates amount with VAT from base amount and VAT rate. Used for live preview when user changes rate or VAT.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("calculate-with-vat")]
    public async Task<ActionResult<decimal>> CalculateWithVat([FromBody] CalculateWithVatRequest req, CancellationToken ct)
    {
        var baseAmount = req?.BaseAmount ?? 0;
        if (req == null || !req.VatRateId.HasValue || req.VatRateId == Guid.Empty)
            return Ok(baseAmount);
        var vatPercent = await vatRateLookup.GetVatPercentAsync(req.VatRateId, ct);
        if (!vatPercent.HasValue)
            return Ok(baseAmount);
        return Ok(VatCalculation.GrossFromNet(baseAmount, vatPercent.Value));
    }

    [HttpGet("by-request/{requestId:guid}")]
    public async Task<ActionResult<IReadOnlyList<PriceProposalDto>>> GetByRequestId(Guid requestId, CancellationToken ct)
        => Ok(await mediator.Send(new GetPriceProposalsByRequestIdQuery(requestId), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PriceProposalDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetPriceProposalByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PriceProposalDto>> Create([FromBody] CreatePriceProposalDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreatePriceProposalCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PriceProposalDto>> Update(Guid id, [FromBody] UpdatePriceProposalDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdatePriceProposalCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeletePriceProposalCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

public record CalculateWithVatRequest(decimal BaseAmount, Guid? VatRateId);
