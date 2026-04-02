using General.Application.DTOs.Task;
using General.Application.Features.Tasks.Commands.Create;
using General.Application.Features.Tasks.Commands.Delete;
using General.Application.Features.Tasks.Commands.Update;
using General.Application.Features.Tasks.Commands.UploadDocument;
using General.Application.Features.Tasks.Queries.GetAll;
using General.Application.Features.Tasks.Queries.GetById;
using General.Application.Features.Tasks.Queries.GetByOperation;
using General.Application.Services;
using General.Domain.AggregatesModel.TaskAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace General.API.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskDto>>> GetAll([FromQuery] Guid? operationId, CancellationToken ct)
    {
        if (operationId.HasValue)
            return Ok(await mediator.Send(new GetTasksByOperationQuery(operationId.Value), ct));
        return Ok(await mediator.Send(new GetAllTasksQuery(), ct));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetTaskByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> Create([FromBody] CreateTaskDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateTaskCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TaskDto>> Update(Guid id, [FromBody] UpdateTaskDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateTaskCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteTaskCommand(id), ct);
        return found ? NoContent() : NotFound();
    }

    [HttpPost("{taskId:guid}/documents")]
    [RequestSizeLimit(52_428_800)]
    public async Task<ActionResult<TaskDocumentDto>> UploadDocument(Guid taskId, IFormFile? file, CancellationToken ct)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new { message = "File is required." });
        await using var ms = new MemoryStream();
        await file.CopyToAsync(ms, ct);
        var result = await mediator.Send(new UploadTaskDocumentCommand(taskId, file.FileName, ms.ToArray()), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("{taskId:guid}/documents/{docId:guid}/file")]
    public async Task<IActionResult> DownloadDocument(
        Guid taskId,
        Guid docId,
        [FromServices] ITaskDocumentRepository documentRepository,
        [FromServices] ITaskDocumentStorage storage,
        CancellationToken ct)
    {
        var doc = await documentRepository.GetByIdAsync(docId, ct);
        if (doc == null || doc.TaskId != taskId)
            return NotFound();
        string full;
        try
        {
            full = storage.GetFullPath(doc.FilePath);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }

        if (!System.IO.File.Exists(full))
            return NotFound();
        return PhysicalFile(full, "application/octet-stream", doc.DocumentName ?? Path.GetFileName(full));
    }
}

