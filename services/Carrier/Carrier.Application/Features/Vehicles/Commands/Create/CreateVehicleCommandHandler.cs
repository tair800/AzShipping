using Carrier.Application.DTOs.Vehicle;
using Carrier.Application.Features.Vehicles;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.VehicleAggregate;
using MediatR;

namespace Carrier.Application.Features.Vehicles.Commands.Create;

public class CreateVehicleCommandHandler(IVehicleRepository repository, IActionLogClient actionLogClient) : IRequestHandler<CreateVehicleCommand, VehicleDto>
{
    private static DateTime? ToUtc(DateTime? d) =>
        d == null ? null : d.Value.Kind == DateTimeKind.Utc ? d : DateTime.SpecifyKind(d.Value, DateTimeKind.Utc);

    public async Task<VehicleDto> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new Vehicle
        {
            Id = Guid.NewGuid(),
            VehicleNumber = dto.VehicleNumber,
            DateOfCreation = ToUtc(dto.DateOfCreation),
            CompanyId = dto.CompanyId,
            CarrierId = dto.CarrierId,
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
            ProductionDate = ToUtc(dto.ProductionDate),
            RegistrationDate = ToUtc(dto.RegistrationDate),
            TechPassportValidity = ToUtc(dto.TechPassportValidity),
            LicenceValidityDate = ToUtc(dto.LicenceValidityDate),
            OwnTransport = dto.OwnTransport,
            VehicleFullWeight = dto.VehicleFullWeight,
            VehicleEmptyWeight = dto.VehicleEmptyWeight,
            Length = dto.Length,
            Width = dto.Width,
            Height = dto.Height,
            VehicleAxles = dto.VehicleAxles,
            MaxWeight = dto.MaxWeight,
            MaxEuroPallets = dto.MaxEuroPallets,
            Status = dto.Status,
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        var result = VehicleMapper.MapToDto(created!);
        await actionLogClient.LogAsync("Vehicle created", $"vehicle: {entity.VehicleNumber} • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }
}
