using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Request.Application.DTOs.RequestNegotiation;
using Request.Application.Features.RequestNegotiations.Commands.Create;
using Request.Application.Features.RequestNegotiations.Commands.Delete;
using Request.Application.Features.RequestNegotiations.Commands.Update;
using Request.Application.Features.RequestNegotiations.Queries.GetAll;
using Request.Application.Features.RequestNegotiations.Queries.GetById;
namespace Request.API.Controllers;

[ApiController]
[Route("api/request-negotiations")]
public class RequestNegotiationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RequestNegotiationDto>>> GetAll([FromQuery] Guid? clientId, CancellationToken ct)
        => Ok(await mediator.Send(new GetAllRequestNegotiationsQuery(clientId), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestNegotiationDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetRequestNegotiationByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<RequestNegotiationDto>> Create([FromBody] CreateRequestNegotiationDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateRequestNegotiationCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RequestNegotiationDto>> Update(Guid id, [FromBody] UpdateRequestNegotiationDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateRequestNegotiationCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteRequestNegotiationCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

