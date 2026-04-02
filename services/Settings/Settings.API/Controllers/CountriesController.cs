using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.Country;
using Settings.Application.Features.Countries;
using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status, CancellationToken ct)
    {
        EntityStatus? statusFilter = null;
        if (!string.IsNullOrEmpty(status) && Enum.TryParse<EntityStatus>(status, true, out var s))
            statusFilter = s;
        var result = await mediator.Send(new GetAllCountriesQuery(statusFilter), ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetCountryByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCountryDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateCountryCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCountryDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateCountryCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var success = await mediator.Send(new DeleteCountryCommand(id), ct);
        return success ? NoContent() : NotFound();
    }
}

