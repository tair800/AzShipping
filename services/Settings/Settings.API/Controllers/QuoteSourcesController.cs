using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.QuoteSource;
using Settings.Application.Features.QuoteSources.Commands.Create;
using Settings.Application.Features.QuoteSources.Commands.Delete;
using Settings.Application.Features.QuoteSources.Commands.Update;
using Settings.Application.Features.QuoteSources.Queries.GetAll;
using Settings.Application.Features.QuoteSources.Queries.GetById;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/quotesources")]
[Route("api/quote-sources")]
public class QuoteSourcesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IReadOnlyList<QuoteSourceDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllQuoteSourcesQuery(), ct));

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<QuoteSourceDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetQuoteSourceByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<QuoteSourceDto>> Create([FromBody] CreateQuoteSourceDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateQuoteSourceCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<QuoteSourceDto>> Update(Guid id, [FromBody] UpdateQuoteSourceDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateQuoteSourceCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var found = await mediator.Send(new DeleteQuoteSourceCommand(id), ct);
        return found ? NoContent() : NotFound();
    }
}
