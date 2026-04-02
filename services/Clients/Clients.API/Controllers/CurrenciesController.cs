using Clients.Application.DTOs.Currency;
using Clients.Application.Features.Currencies.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clients.API.Controllers;

[ApiController]
[Route("api/currencies")]
public class CurrenciesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CurrencyDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllCurrenciesQuery(), ct));
}

