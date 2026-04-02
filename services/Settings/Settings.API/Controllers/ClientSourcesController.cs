using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.ClientSource;
using Settings.Application.Features.ClientSources;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientSourcesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await mediator.Send(new GetAllClientSourcesQuery(), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetClientSourceByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateClientSourceDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateClientSourceCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClientSourceDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateClientSourceCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var success = await mediator.Send(new DeleteClientSourceCommand(id), ct);
        return success ? NoContent() : NotFound();
    }
}

