using General.Application.DTOs.Employee;
using General.Application.Features.Employees.Commands.Create;
using General.Application.Features.Employees.Commands.Delete;
using General.Application.Features.Employees.Commands.Update;
using General.Application.Features.Employees.Queries.GetAll;
using General.Application.Features.Employees.Queries.GetById;
using General.Application.Features.Employees.Queries.GetByUserId;
using General.Application.Features.Employees.Queries.GetSummaries;
using General.Application.Features.Employees.Commands.CreateNote;
using General.Application.Features.Employees.Queries.GetNotes;
using General.Application.Features.Employees.Queries.GetTaskStatistics;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace General.API.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController(IMediator mediator) : ControllerBase
{
    /// <summary>Full employee rows (detail page).</summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EmployeeDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllEmployeesQuery(), ct));

    /// <summary>Minimal list for responsible-person picker; use <c>userId</c> as task <c>responsibleUserId</c>.</summary>
    [HttpGet("summaries")]
    public async Task<ActionResult<IReadOnlyList<EmployeeSummaryDto>>> GetSummaries(CancellationToken ct)
        => Ok(await mediator.Send(new GetEmployeeSummariesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<EmployeeDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetEmployeeByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("by-user/{userId:long}")]
    public async Task<ActionResult<EmployeeDto>> GetByUserId(long userId, CancellationToken ct)
    {
        var result = await mediator.Send(new GetEmployeeByUserIdQuery(userId), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("{employeeId:guid}/notes")]
    public async Task<ActionResult<IReadOnlyList<EmployeeNoteDto>>> GetNotes(Guid employeeId, CancellationToken ct)
    {
        var result = await mediator.Send(new GetEmployeeNotesQuery(employeeId), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("{employeeId:guid}/notes")]
    public async Task<ActionResult<EmployeeNoteDto>> CreateNote(Guid employeeId, [FromBody] CreateEmployeeNoteDto dto, CancellationToken ct)
    {
        try
        {
            var result = await mediator.Send(new CreateEmployeeNoteCommand(employeeId, dto), ct);
            return result == null ? NotFound() : CreatedAtAction(nameof(GetNotes), new { employeeId }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>Task counts for the employee’s Identity <c>userId</c> as responsible user. Week is Monday–Sunday UTC.</summary>
    [HttpGet("{id:guid}/task-statistics")]
    public async Task<ActionResult<EmployeeTaskStatisticsDto>> GetTaskStatistics(
        Guid id,
        [FromQuery] DateTime? weekStartUtc,
        [FromQuery] Guid[]? completedStatusId,
        CancellationToken ct)
    {
        var completed = (IReadOnlyList<Guid>)(completedStatusId ?? []).ToList();
        var result = await mediator.Send(new GetEmployeeTaskStatisticsQuery(id, weekStartUtc, completed), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> Create([FromBody] CreateEmployeeDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateEmployeeCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<EmployeeDto>> Update(Guid id, [FromBody] UpdateEmployeeDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateEmployeeCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ok = await mediator.Send(new DeleteEmployeeCommand(id), ct);
        return ok ? NoContent() : NotFound();
    }
}
