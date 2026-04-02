using Carrier.Application.DTOs.Vehicle;
using Carrier.Domain.AggregatesModel.VehicleAggregate;

namespace Carrier.Application.Features.Vehicles;

public static class VehicleMapper
{
    public static VehicleDto MapToDto(Vehicle? entity)
    {
        if (entity == null) return new VehicleDto();
        return new VehicleDto
        {
            Id = entity.Id,
            VehicleNumber = entity.VehicleNumber,
            DateOfCreation = entity.DateOfCreation,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            CompanyId = entity.CompanyId,
            CarrierId = entity.CarrierId,
            BrandId = entity.BrandId,
            BrandName = entity.BrandName,
            ModelId = entity.ModelId,
            ModelName = entity.ModelName,
            EuroEmissionClassId = entity.EuroEmissionClassId,
            TransportTypeId = entity.TransportTypeId,
            GroupId = entity.GroupId,
            TrailerNumber = entity.TrailerNumber,
            BodyNumber = entity.BodyNumber,
            LicenceNumber = entity.LicenceNumber,
            Drivers = entity.Drivers,
            FuelCard = entity.FuelCard,
            TransportInformation = entity.TransportInformation,
            ProductionDate = entity.ProductionDate,
            RegistrationDate = entity.RegistrationDate,
            TechPassportValidity = entity.TechPassportValidity,
            LicenceValidityDate = entity.LicenceValidityDate,
            OwnTransport = entity.OwnTransport,
            VehicleFullWeight = entity.VehicleFullWeight,
            VehicleEmptyWeight = entity.VehicleEmptyWeight,
            Length = entity.Length,
            Width = entity.Width,
            Height = entity.Height,
            VehicleAxles = entity.VehicleAxles,
            MaxWeight = entity.MaxWeight,
            MaxEuroPallets = entity.MaxEuroPallets,
            Status = entity.Status
        };
    }
}
