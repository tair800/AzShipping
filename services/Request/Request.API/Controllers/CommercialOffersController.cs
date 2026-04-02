using MediatR;
using Microsoft.AspNetCore.Mvc;
using Request.Application.DTOs.CommercialOffer;
using Request.Application.Features.CommercialOffers.Commands.Create;
using Request.Application.Features.CommercialOffers.Commands.Delete;
using Request.Application.Features.CommercialOffers.Commands.Update;
using Request.Application.Features.CommercialOffers.Queries.GetById;
using Request.Application.Features.CommercialOffers.Queries.GetByRequestId;

namespace Request.API.Controllers;

[ApiController]
[Route("api/commercial-offers")]
public class CommercialOffersController(IMediator mediator) : ControllerBase
{
    [HttpGet("by-request/{requestId:guid}")]
    public async Task<ActionResult<IReadOnlyList<CommercialOfferDto>>> GetByRequestId(Guid requestId, CancellationToken ct)
        => Ok(await mediator.Send(new GetCommercialOffersByRequestIdQuery(requestId), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CommercialOfferDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetCommercialOfferByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CommercialOfferDto>> Create([FromBody] CreateCommercialOfferDto dto, CancellationToken ct)
    {
        try
        {
            var result = await mediator.Send(new CreateCommercialOfferCommand(dto), ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CommercialOfferDto>> Update(Guid id, [FromBody] UpdateCommercialOfferDto dto, CancellationToken ct)
    {
        try
        {
            var result = await mediator.Send(new UpdateCommercialOfferCommand(id, dto), ct);
            return result == null ? NotFound() : Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteCommercialOfferCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}
