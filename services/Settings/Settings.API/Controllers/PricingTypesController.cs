using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.PricingType;
using Settings.Application.Features.PricingTypes.Commands.Create;
using Settings.Application.Features.PricingTypes.Commands.Delete;
using Settings.Application.Features.PricingTypes.Commands.Update;
using Settings.Application.Features.PricingTypes.Queries.GetAll;
using Settings.Application.Features.PricingTypes.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/pricingtypes")]
public class PricingTypesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PricingTypeDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllPricingTypesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PricingTypeDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetPricingTypeByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PricingTypeDto>> Create([FromBody] CreatePricingTypeDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreatePricingTypeCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PricingTypeDto>> Update(Guid id, [FromBody] UpdatePricingTypeDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdatePricingTypeCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeletePricingTypeCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

