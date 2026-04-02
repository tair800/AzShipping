using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.City;
using Settings.Application.Features.Cities;
using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CityDto>>> GetAll([FromQuery] string? status = null)
    {
        EntityStatus? statusEnum = null;
        if (!string.IsNullOrEmpty(status) && Enum.TryParse<EntityStatus>(status, true, out var s))
            statusEnum = s;
        var result = await mediator.Send(new GetAllCitiesQuery(statusEnum));
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CityDto>> GetById(Guid id)
    {
        var result = await mediator.Send(new GetCityByIdQuery(id));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CityDto>> Create([FromBody] CreateCityDto dto)
    {
        var result = await mediator.Send(new CreateCityCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CityDto>> Update(Guid id, [FromBody] UpdateCityDto dto)
    {
        var result = await mediator.Send(new UpdateCityCommand(id, dto));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteCityCommand(id));
        if (!result) return NotFound();
        return NoContent();
    }
}

