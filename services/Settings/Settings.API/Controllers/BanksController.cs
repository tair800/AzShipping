using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.Bank;
using Settings.Application.Features.Banks.Commands.Create;
using Settings.Application.Features.Banks.Commands.Delete;
using Settings.Application.Features.Banks.Commands.Update;
using Settings.Application.Features.Banks.Queries.GetAll;
using Settings.Application.Features.Banks.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/banks")]
public class BanksController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BankDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllBanksQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<BankDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetBankByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<BankDto>> Create([FromBody] CreateBankDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateBankCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<BankDto>> Update(Guid id, [FromBody] UpdateBankDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateBankCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteBankCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}

