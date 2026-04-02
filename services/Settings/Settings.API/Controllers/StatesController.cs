using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.State;
using Settings.Application.Features.States;
using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<StateDto>>> GetAll([FromQuery] string? status = null)
    {
        EntityStatus? statusEnum = null;
        if (!string.IsNullOrEmpty(status) && Enum.TryParse<EntityStatus>(status, true, out var s))
            statusEnum = s;
        var result = await mediator.Send(new GetAllStatesQuery(statusEnum));
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StateDto>> GetById(Guid id)
    {
        var result = await mediator.Send(new GetStateByIdQuery(id));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<StateDto>> Create([FromBody] CreateStateDto dto)
    {
        var result = await mediator.Send(new CreateStateCommand(dto));
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<StateDto>> Update(Guid id, [FromBody] UpdateStateDto dto)
    {
        var result = await mediator.Send(new UpdateStateCommand(id, dto));
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteStateCommand(id));
        if (!result) return NotFound();
        return NoContent();
    }
}

