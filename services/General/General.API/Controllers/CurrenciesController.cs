using General.Application.DTOs.Currency;
using General.Application.Features.Currencies.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace General.API.Controllers;

[ApiController]
[Route("api/currencies")]
[Route("api/general/currencies")]
public class CurrenciesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CurrencyDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllCurrenciesQuery(), ct));
}

