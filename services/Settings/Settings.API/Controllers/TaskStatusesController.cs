using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.TaskStatus;
using Settings.Application.Features.TaskStatuses.Commands.Create;
using Settings.Application.Features.TaskStatuses.Commands.Delete;
using Settings.Application.Features.TaskStatuses.Commands.Update;
using Settings.Application.Features.TaskStatuses.Queries.GetAll;
using Settings.Application.Features.TaskStatuses.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/task-statuses")]
public class TaskStatusesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskStatusDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllTaskStatusesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskStatusDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetTaskStatusByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TaskStatusDto>> Create([FromBody] CreateTaskStatusDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateTaskStatusCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TaskStatusDto>> Update(Guid id, [FromBody] UpdateTaskStatusDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateTaskStatusCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteTaskStatusCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

