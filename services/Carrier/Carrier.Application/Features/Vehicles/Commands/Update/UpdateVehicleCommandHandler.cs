using Carrier.Application.DTOs.Vehicle;
using Carrier.Application.Features.Vehicles;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.VehicleAggregate;
using MediatR;

namespace Carrier.Application.Features.Vehicles.Commands.Update;

public class UpdateVehicleCommandHandler(IVehicleRepository repository, IActionLogClient actionLogClient) : IRequestHandler<UpdateVehicleCommand, VehicleDto?>
{
    private static DateTime? ToUtc(DateTime? d) =>
        d == null ? null : d.Value.Kind == DateTimeKind.Utc ? d : DateTime.SpecifyKind(d.Value, DateTimeKind.Utc);

    public async Task<VehicleDto?> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return null;

        var dto = request.Dto;
        existing.VehicleNumber = dto.VehicleNumber;
        existing.DateOfCreation = ToUtc(dto.DateOfCreation);
        existing.CompanyId = dto.CompanyId;
        existing.CarrierId = dto.CarrierId;
        existing.BrandId = dto.BrandId;
        existing.BrandName = dto.BrandName;
        existing.ModelId = dto.ModelId;
        existing.ModelName = dto.ModelName;
        existing.EuroEmissionClassId = dto.EuroEmissionClassId;
        existing.TransportTypeId = dto.TransportTypeId;
        existing.GroupId = dto.GroupId;
        existing.TrailerNumber = dto.TrailerNumber;
        existing.BodyNumber = dto.BodyNumber;
        existing.LicenceNumber = dto.LicenceNumber;
        existing.Drivers = dto.Drivers;
        existing.FuelCard = dto.FuelCard;
        existing.TransportInformation = dto.TransportInformation;
        existing.ProductionDate = ToUtc(dto.ProductionDate);
        existing.RegistrationDate = ToUtc(dto.RegistrationDate);
        existing.TechPassportValidity = ToUtc(dto.TechPassportValidity);
        existing.LicenceValidityDate = ToUtc(dto.LicenceValidityDate);
        existing.OwnTransport = dto.OwnTransport;
        existing.VehicleFullWeight = dto.VehicleFullWeight;
        existing.VehicleEmptyWeight = dto.VehicleEmptyWeight;
        existing.Length = dto.Length;
        existing.Width = dto.Width;
        existing.Height = dto.Height;
        existing.VehicleAxles = dto.VehicleAxles;
        existing.MaxWeight = dto.MaxWeight;
        existing.MaxEuroPallets = dto.MaxEuroPallets;
        existing.Status = dto.Status;
        existing.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(existing, cancellationToken);
        var updated = await repository.GetByIdAsync(existing.Id, cancellationToken);
        var result = VehicleMapper.MapToDto(updated!);
        await actionLogClient.LogAsync("Vehicle updated", $"vehicle: {existing.VehicleNumber} • id: {existing.Id}", null, null, cancellationToken);
        return result;
    }
}
