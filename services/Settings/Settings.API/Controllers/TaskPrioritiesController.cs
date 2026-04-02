using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.TaskPriority;
using Settings.Application.Features.TaskPriorities.Commands.Create;
using Settings.Application.Features.TaskPriorities.Commands.Delete;
using Settings.Application.Features.TaskPriorities.Commands.Update;
using Settings.Application.Features.TaskPriorities.Queries.GetAll;
using Settings.Application.Features.TaskPriorities.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/task-priorities")]
public class TaskPrioritiesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskPriorityDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllTaskPrioritiesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskPriorityDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetTaskPriorityByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TaskPriorityDto>> Create([FromBody] CreateTaskPriorityDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateTaskPriorityCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TaskPriorityDto>> Update(Guid id, [FromBody] UpdateTaskPriorityDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateTaskPriorityCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteTaskPriorityCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

