using Carrier.Application.DTOs.Driver;
using Carrier.Application.Features.Drivers.Commands.Create;
using Carrier.Application.Features.Drivers.Queries.GetByCarrierId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrier.API.Controllers;

[ApiController]
[Route("api/carriers/{carrierId:guid}/drivers")]
public class CarrierDriversController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DriverDto>>> GetByCarrierId(Guid carrierId, CancellationToken ct)
        => Ok(await mediator.Send(new GetDriversByCarrierIdQuery(carrierId), ct));

    [HttpPost]
    public async Task<ActionResult<DriverDto>> Create(Guid carrierId, [FromBody] CreateDriverDto dto, CancellationToken ct)
    {
        var dtoWithCarrier = new CreateDriverDto
        {
            Name = dto.Name,
            Surname = dto.Surname,
            MiddleName = dto.MiddleName,
            Passport = dto.Passport,
            DrivingLicenceNumber = dto.DrivingLicenceNumber,
            PhoneNumber = dto.PhoneNumber,
            BankAccount = dto.BankAccount,
            FuelCard = dto.FuelCard,
            Notes = dto.Notes,
            DrivingLicenceCategoryIds = dto.DrivingLicenceCategoryIds ?? new List<Guid>(),
            CarrierIds = (dto.CarrierIds ?? new List<Guid>()).Contains(carrierId) ? (dto.CarrierIds ?? new List<Guid>()) : [carrierId, ..(dto.CarrierIds ?? [])],
            DateOfEmployment = dto.DateOfEmployment,
            IsDeactive = dto.IsDeactive
        };
        var result = await mediator.Send(new CreateDriverCommand(dtoWithCarrier), ct);
        return CreatedAtAction(nameof(GetByCarrierId), new { carrierId }, result);
    }
}

