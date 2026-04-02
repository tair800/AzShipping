using Carrier.Application.DTOs.Terminal;
using Carrier.Application.Features.Terminals.Commands.Create;
using Carrier.Application.Features.Terminals.Commands.Delete;
using Carrier.Application.Features.Terminals.Commands.Update;
using Carrier.Application.Features.Terminals.Queries.GetAll;
using Carrier.Application.Features.Terminals.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrier.API.Controllers;

[ApiController]
[Route("api/terminals")]
public class TerminalsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TerminalDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllTerminalsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TerminalDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetTerminalByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TerminalDto>> Create([FromBody] CreateTerminalDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateTerminalCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TerminalDto>> Update(Guid id, [FromBody] UpdateTerminalDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateTerminalCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteTerminalCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

