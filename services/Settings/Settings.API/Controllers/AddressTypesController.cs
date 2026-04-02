using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Settings.Application.DTOs.AddressType;
using Settings.Application.Features.AddressTypes.Queries.GetAll;

namespace Settings.API.Controllers;

[ApiController]
[Route("api/address-types")]
public class AddressTypesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AddressTypeDto>>> GetAll(CancellationToken ct)
        => Ok(await mediator.Send(new GetAllAddressTypesQuery(), ct));
}

