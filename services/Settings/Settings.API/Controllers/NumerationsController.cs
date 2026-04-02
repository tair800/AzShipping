using MediatR;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.Numeration;
using Settings.Application.Features.Numerations.Commands.Create;
using Settings.Application.Features.Numerations.Commands.Delete;
using Settings.Application.Features.Numerations.Commands.Generate;
using Settings.Application.Features.Numerations.Commands.Update;
using Settings.Application.Features.Numerations.Queries.GetAll;
using Settings.Application.Features.Numerations.Queries.GetById;
using Settings.Application.Features.Numerations.Queries.Preview;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/numerations")]
public class NumerationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<NumerationDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllNumerationsQuery(), ct));

    [HttpGet("numeration-for-options")]
    public ActionResult<IReadOnlyList<object>> GetNumerationForOptions()
        => Ok(NumerationForTypeOptions.All.Select(x => new { code = x.Code, name = x.Name }));

    [HttpGet("formula-elements")]
    public ActionResult<string[]> GetFormulaElements() => Ok(FormulaElementOptions.All);

    [HttpPost("preview")]
    public async Task<ActionResult<NumerationGenerateResponseDto>> Preview([FromBody] NumerationGenerateRequestDto dto,
        CancellationToken ct)
    {
        try
        {
            return Ok(await mediator.Send(new PreviewNumerationQuery(dto), ct));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("generate")]
    public async Task<ActionResult<NumerationGenerateResponseDto>> Generate([FromBody] NumerationGenerateRequestDto dto,
        CancellationToken ct)
    {
        try
        {
            return Ok(await mediator.Send(new GenerateNumerationCommand(dto), ct));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NumerationDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetNumerationByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<NumerationDto>> Create([FromBody] CreateNumerationDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateNumerationCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<NumerationDto>> Update(Guid id, [FromBody] UpdateNumerationDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateNumerationCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteNumerationCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}
