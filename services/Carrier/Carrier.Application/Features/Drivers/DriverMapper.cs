using Carrier.Application.DTOs.Driver;
using Carrier.Domain.AggregatesModel.DriverAggregate;

namespace Carrier.Application.Features.Drivers;

public static class DriverMapper
{
    public static DriverDto MapToDto(Driver? entity)
    {
        if (entity == null) return new DriverDto();
        return new DriverDto
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Name = entity.Name,
            Surname = entity.Surname,
            MiddleName = entity.MiddleName,
            Passport = entity.Passport,
            DrivingLicenceNumber = entity.DrivingLicenceNumber,
            PhoneNumber = entity.PhoneNumber,
            BankAccount = entity.BankAccount,
            FuelCard = entity.FuelCard,
            Notes = entity.Notes,
            PassportFilePath = entity.PassportFilePath,
            DrivingLicenceFilePath = entity.DrivingLicenceFilePath,
            DrivingLicenceCategoryIds = entity.DrivingLicenceCategories?.Select(c => c.DrivingLicenceCategoryId).ToList() ?? new List<Guid>(),
            CarrierIds = entity.DriverCarriers?.Select(c => c.CarrierId).ToList() ?? new List<Guid>(),
            DateOfEmployment = entity.DateOfEmployment,
            IsDeactive = entity.IsDeactive
        };
    }
}
