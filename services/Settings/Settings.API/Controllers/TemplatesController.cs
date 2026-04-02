using MediatR;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.Template;
using Settings.Application.Features.Templates;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/templates")]
public class TemplatesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TemplateDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllTemplatesQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TemplateDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetTemplateByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TemplateDto>> Create([FromBody] CreateTemplateDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateTemplateCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TemplateDto>> Update(Guid id, [FromBody] UpdateTemplateDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateTemplateCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteTemplateCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}
