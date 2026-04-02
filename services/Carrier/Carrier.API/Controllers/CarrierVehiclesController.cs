using Carrier.Application.DTOs.Vehicle;
using Carrier.Application.Features.Vehicles.Commands.Create;
using Carrier.Application.Features.Vehicles.Queries.GetByCarrierId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carrier.API.Controllers;

[ApiController]
[Route("api/carriers/{carrierId:guid}/vehicles")]
public class CarrierVehiclesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<VehicleDto>>> GetByCarrierId(Guid carrierId, CancellationToken ct)
        => Ok(await mediator.Send(new GetVehiclesByCarrierIdQuery(carrierId), ct));

    [HttpPost]
    public async Task<ActionResult<VehicleDto>> Create(Guid carrierId, [FromBody] CreateVehicleDto dto, CancellationToken ct)
    {
        var dtoWithCarrier = new CreateVehicleDto
        {
            VehicleNumber = dto.VehicleNumber,
            DateOfCreation = dto.DateOfCreation,
            CompanyId = dto.CompanyId,
            CarrierId = dto.CarrierId ?? carrierId,
            BrandId = dto.BrandId,
            BrandName = dto.BrandName,
            ModelId = dto.ModelId,
            ModelName = dto.ModelName,
            EuroEmissionClassId = dto.EuroEmissionClassId,
            TransportTypeId = dto.TransportTypeId,
            GroupId = dto.GroupId,
            TrailerNumber = dto.TrailerNumber,
            BodyNumber = dto.BodyNumber,
            LicenceNumber = dto.LicenceNumber,
            Drivers = dto.Drivers,
            FuelCard = dto.FuelCard,
            TransportInformation = dto.TransportInformation,
            ProductionDate = dto.ProductionDate,
            RegistrationDate = dto.RegistrationDate,
            TechPassportValidity = dto.TechPassportValidity,
            LicenceValidityDate = dto.LicenceValidityDate,
            OwnTransport = dto.OwnTransport,
            VehicleFullWeight = dto.VehicleFullWeight,
            VehicleEmptyWeight = dto.VehicleEmptyWeight,
            Length = dto.Length,
            Width = dto.Width,
            Height = dto.Height,
            VehicleAxles = dto.VehicleAxles,
            MaxWeight = dto.MaxWeight,
            MaxEuroPallets = dto.MaxEuroPallets,
            Status = dto.Status
        };
        var result = await mediator.Send(new CreateVehicleCommand(dtoWithCarrier), ct);
        return CreatedAtAction(nameof(GetByCarrierId), new { carrierId }, result);
    }
}

