using MediatR;
using Microsoft.AspNetCore.Mvc;
using Request.Application.DTOs.RequestComment;
using Request.Application.Features.RequestComments.Commands.Create;
using Request.Application.Features.RequestComments.Commands.Delete;
using Request.Application.Features.RequestComments.Commands.Update;
using Request.Application.Features.RequestComments.Queries.GetById;
using Request.Application.Features.RequestComments.Queries.GetByRequestId;

namespace Request.API.Controllers;

[ApiController]
[Route("api/request-comments")]
public class RequestCommentsController(IMediator mediator) : ControllerBase
{
    [HttpGet("by-request/{requestId:guid}")]
    public async Task<ActionResult<IReadOnlyList<RequestCommentDto>>> GetByRequestId(Guid requestId, CancellationToken ct)
        => Ok(await mediator.Send(new GetRequestCommentsByRequestIdQuery(requestId), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestCommentDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetRequestCommentByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<RequestCommentDto>> Create([FromBody] CreateRequestCommentDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateRequestCommentCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RequestCommentDto>> Update(Guid id, [FromBody] UpdateRequestCommentDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateRequestCommentCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteRequestCommentCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}
