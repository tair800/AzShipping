using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.Department;
using Settings.Application.Features.Departments.Commands.Create;
using Settings.Application.Features.Departments.Commands.Delete;
using Settings.Application.Features.Departments.Commands.Update;
using Settings.Application.Features.Departments.Queries.GetAll;
using Settings.Application.Features.Departments.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/departments")]
public class DepartmentsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DepartmentDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllDepartmentsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DepartmentDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetDepartmentByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentDto>> Create([FromBody] CreateDepartmentDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateDepartmentCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<DepartmentDto>> Update(Guid id, [FromBody] UpdateDepartmentDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateDepartmentCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteDepartmentCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

