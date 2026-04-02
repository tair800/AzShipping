using Accounting.Application.DTOs.VatDefinition;
using Accounting.Application.Features.VatDefinitions.Queries.Legacy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers;

/// <summary>Backward-compatible routes previously served by Settings.API (Request service + frontend).</summary>
[ApiController]
[Route("api/vatrates")]
[AllowAnonymous]
public class VatRatesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<VatRateLegacyDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllVatRatesLegacyQuery(), ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VatRateLegacyDto>> GetById(Guid id, CancellationToken ct)
    {
        var r = await mediator.Send(new GetVatRateLegacyByIdQuery(id), ct);
        return r == null ? NotFound() : Ok(r);
    }
}
