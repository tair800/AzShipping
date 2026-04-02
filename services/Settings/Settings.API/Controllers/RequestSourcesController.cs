using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.RequestSource;
using Settings.Application.Features.RequestSources.Commands.Create;
using Settings.Application.Features.RequestSources.Commands.Delete;
using Settings.Application.Features.RequestSources.Commands.Update;
using Settings.Application.Features.RequestSources.Queries.GetAll;
using Settings.Application.Features.RequestSources.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/requestsources")]
public class RequestSourcesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RequestSourceDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllRequestSourcesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestSourceDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetRequestSourceByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<RequestSourceDto>> Create([FromBody] CreateRequestSourceDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateRequestSourceCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RequestSourceDto>> Update(Guid id, [FromBody] UpdateRequestSourceDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateRequestSourceCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteRequestSourceCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

