using Accounting.Application.DTOs.InvoiceLookup;
using Accounting.Application.Features.InvoiceLookups.Commands.CreateInvoiceLookupOption;
using Accounting.Application.Features.InvoiceLookups.Queries.GetInvoiceLookups;
using Accounting.Application.InvoiceLookups;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers;

[ApiController]
[Route("api/invoice-lookups")]
public class InvoiceLookupsController(IMediator mediator) : ControllerBase
{
    /// <summary>Flat list of lookup rows; optional filter by category (e.g. invoiceType, warehouse).</summary>
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<InvoiceLookupOptionDto>>> GetAll(
        [FromQuery] string? category,
        CancellationToken ct)
    {
        if (!string.IsNullOrWhiteSpace(category) &&
            !InvoiceLookupCategoryKeys.TryParseApiKey(category, out _))
            return BadRequest("Unknown category. Use camelCase keys such as invoiceType, expenseCenter.");

        try
        {
            return Ok(await mediator.Send(new GetInvoiceLookupsQuery(category), ct));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>Add option for expense center, special code, or warehouse (invoice screen "+").</summary>
    [HttpPost]
    public async Task<ActionResult<InvoiceLookupOptionDto>> Create([FromBody] CreateInvoiceLookupOptionDto dto,
        CancellationToken ct)
    {
        var result = await mediator.Send(new CreateInvoiceLookupOptionCommand(dto), ct);
        if (!result.Success)
            return BadRequest(result.Error);
        return Ok(result.Data);
    }
}
