using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.LoadingMethod;
using Settings.Application.Features.LoadingMethods;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/loadingmethods")]
public class LoadingMethodsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<LoadingMethodDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllLoadingMethodsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<LoadingMethodDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetLoadingMethodByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<LoadingMethodDto>> Create([FromBody] CreateLoadingMethodDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateLoadingMethodCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<LoadingMethodDto>> Update(Guid id, [FromBody] UpdateLoadingMethodDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateLoadingMethodCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteLoadingMethodCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

