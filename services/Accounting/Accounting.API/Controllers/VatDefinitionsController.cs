using Accounting.Application.DTOs.VatDefinition;
using Accounting.Application.Features.VatDefinitions.Commands.Calculate;
using Accounting.Application.Features.VatDefinitions.Commands.Create;
using Accounting.Application.Features.VatDefinitions.Commands.Delete;
using Accounting.Application.Features.VatDefinitions.Commands.Update;
using Accounting.Application.Features.VatDefinitions.Queries.GetAll;
using Accounting.Application.Features.VatDefinitions.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers;

[ApiController]
[Route("api/vat-definitions")]
public class VatDefinitionsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<VatDefinitionDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllVatDefinitionsQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VatDefinitionDto>> GetById(Guid id, CancellationToken ct)
    {
        var r = await mediator.Send(new GetVatDefinitionByIdQuery(id), ct);
        return r == null ? NotFound() : Ok(r);
    }

    [HttpPost]
    public async Task<ActionResult<VatDefinitionDto>> Create([FromBody] CreateVatDefinitionDto dto, CancellationToken ct)
    {
        try
        {
            var result = await mediator.Send(new CreateVatDefinitionCommand(dto), ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<VatDefinitionDto>> Update(Guid id, [FromBody] UpdateVatDefinitionDto dto, CancellationToken ct)
    {
        try
        {
            var result = await mediator.Send(new UpdateVatDefinitionCommand(id, dto), ct);
            return result == null ? NotFound() : Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ok = await mediator.Send(new DeleteVatDefinitionCommand(id), ct);
        return ok ? NoContent() : NotFound();
    }

    /// <summary>Compute VAT from net amount using a stored definition (same rules as price proposals).</summary>
    [AllowAnonymous]
    [HttpPost("calculate-from-net")]
    public async Task<ActionResult<CalculateVatFromNetResultDto>> CalculateFromNet([FromBody] CalculateVatFromNetRequestDto body, CancellationToken ct)
    {
        var r = await mediator.Send(new CalculateVatFromNetCommand(body), ct);
        return r == null ? NotFound() : Ok(r);
    }
}
