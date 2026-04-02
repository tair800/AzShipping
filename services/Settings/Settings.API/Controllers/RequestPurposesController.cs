using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.RequestPurpose;
using Settings.Application.Features.RequestPurposes;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/requestpurposes")]
public class RequestPurposesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RequestPurposeDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllRequestPurposesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestPurposeDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetRequestPurposeByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<RequestPurposeDto>> Create([FromBody] CreateRequestPurposeDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateRequestPurposeCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RequestPurposeDto>> Update(Guid id, [FromBody] UpdateRequestPurposeDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateRequestPurposeCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteRequestPurposeCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

