using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.Packaging;
using Settings.Application.Features.Packagings;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/packagings")]
public class PackagingsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PackagingDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllPackagingsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PackagingDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetPackagingByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PackagingDto>> Create([FromBody] CreatePackagingDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreatePackagingCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PackagingDto>> Update(Guid id, [FromBody] UpdatePackagingDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdatePackagingCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeletePackagingCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

