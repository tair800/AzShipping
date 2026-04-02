using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.WorkerPost;
using Settings.Application.Features.WorkerPosts;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/workerposts")]
public class WorkerPostsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<WorkerPostDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllWorkerPostsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WorkerPostDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetWorkerPostByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<WorkerPostDto>> Create([FromBody] CreateWorkerPostDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateWorkerPostCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<WorkerPostDto>> Update(Guid id, [FromBody] UpdateWorkerPostDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateWorkerPostCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteWorkerPostCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

