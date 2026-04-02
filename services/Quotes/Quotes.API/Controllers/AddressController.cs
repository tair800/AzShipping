using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quotes.Application.DTOs.Address;
using Quotes.Application.Features.Addresses.Commands.CreateAddress;
using Quotes.Application.Features.Addresses.Commands.UpdateAddress;
using Quotes.Application.Features.Addresses.Queries.GetAddressById;

namespace Quotes.API.Controllers;

[ApiController]
[Route("api/addresses")]
public class AddressController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AddressDto>> GetById(Guid id, CancellationToken ct)
    {
        var result = await mediator.Send(new GetAddressByIdQuery(id), ct);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<AddressDto>> Create([FromBody] CreateOrUpdateAddressDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new CreateAddressCommand(dto), ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<AddressDto>> Update(Guid id, [FromBody] CreateOrUpdateAddressDto dto, CancellationToken ct)
    {
        var result = await mediator.Send(new UpdateAddressCommand(id, dto), ct);
        return result == null ? NotFound() : Ok(result);
    }
}

