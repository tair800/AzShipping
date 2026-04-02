using Carrier.Application.DTOs.Driver;
using Carrier.Application.Features.Drivers;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.DriverAggregate;
using MediatR;

namespace Carrier.Application.Features.Drivers.Commands.Update;

public class UpdateDriverCommandHandler(IDriverRepository repository, IActionLogClient actionLogClient) : IRequestHandler<UpdateDriverCommand, DriverDto?>
{
    public async Task<DriverDto?> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return null;

        var dto = request.Dto;
        existing.Name = dto.Name;
        existing.Surname = dto.Surname;
        existing.MiddleName = dto.MiddleName;
        existing.Passport = dto.Passport;
        existing.DrivingLicenceNumber = dto.DrivingLicenceNumber;
        existing.PhoneNumber = dto.PhoneNumber;
        existing.BankAccount = dto.BankAccount;
        existing.FuelCard = dto.FuelCard;
        existing.Notes = dto.Notes;
        existing.DateOfEmployment = dto.DateOfEmployment;
        existing.IsDeactive = dto.IsDeactive;
        existing.UpdatedAt = DateTime.UtcNow;

        existing.DriverCarriers.Clear();
        if (dto.CarrierIds is { Count: > 0 })
            foreach (var cid in dto.CarrierIds)
                existing.DriverCarriers.Add(new DriverCarrier { DriverId = existing.Id, CarrierId = cid });

        existing.DrivingLicenceCategories.Clear();
        if (dto.DrivingLicenceCategoryIds is { Count: > 0 })
            foreach (var catId in dto.DrivingLicenceCategoryIds)
                existing.DrivingLicenceCategories.Add(new DriverDrivingLicenceCategory { DriverId = existing.Id, DrivingLicenceCategoryId = catId });

        await repository.UpdateAsync(existing, cancellationToken);
        var updated = await repository.GetByIdAsync(existing.Id, cancellationToken);
        var result = DriverMapper.MapToDto(updated!);
        await actionLogClient.LogAsync("Driver updated", $"driver: {existing.Name} {existing.Surname} • id: {existing.Id}", null, null, cancellationToken);
        return result;
    }
}
