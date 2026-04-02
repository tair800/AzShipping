using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.DrivingLicenceCategory;
using Settings.Application.Features.DrivingLicenceCategories;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/drivinglicencecategories")]
public class DrivingLicenceCategoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DrivingLicenceCategoryDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllDrivingLicenceCategoriesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DrivingLicenceCategoryDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetDrivingLicenceCategoryByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<DrivingLicenceCategoryDto>> Create([FromBody] CreateDrivingLicenceCategoryDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateDrivingLicenceCategoryCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<DrivingLicenceCategoryDto>> Update(Guid id, [FromBody] UpdateDrivingLicenceCategoryDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateDrivingLicenceCategoryCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteDrivingLicenceCategoryCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

