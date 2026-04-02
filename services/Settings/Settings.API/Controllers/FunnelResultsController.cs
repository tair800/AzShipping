using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.FunnelResult;
using Settings.Application.Features.FunnelResults;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/funnelresults")]
public class FunnelResultsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<FunnelResultDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllFunnelResultsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FunnelResultDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetFunnelResultByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<FunnelResultDto>> Create([FromBody] CreateFunnelResultDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateFunnelResultCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<FunnelResultDto>> Update(Guid id, [FromBody] UpdateFunnelResultDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateFunnelResultCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteFunnelResultCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

