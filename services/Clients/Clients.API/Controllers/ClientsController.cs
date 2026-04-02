using Clients.Application.DTOs.Client;
using Clients.Application.Features.Clients.Commands.Create;
using Clients.Application.Features.Clients.Commands.Delete;
using Clients.Application.Features.Clients.Commands.Update;
using Clients.Application.Features.Clients.Commands.UpdateAdditionalField;
using Clients.Application.Features.Clients.Commands.UpdateStage;
using Clients.Application.Features.Clients.Queries.GetAll;
using Clients.Application.Features.Clients.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clients.API.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllClientsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClientDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetClientByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> Create([FromBody] CreateClientDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateClientCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ClientDto>> Update(Guid id, [FromBody] UpdateClientDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateClientCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPatch("{id:guid}/stage")]
    public async Task<ActionResult<ClientDto>> UpdateStage(Guid id, [FromBody] UpdateClientStageDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateClientStageCommand(id, dto.ClientStatusId), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPatch("{id:guid}/additional-field")]
    public async Task<ActionResult<ClientDto>> UpdateAdditionalField(Guid id, [FromBody] UpdateClientAdditionalFieldDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateClientAdditionalFieldCommand(id, dto.Comment), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteClientCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

