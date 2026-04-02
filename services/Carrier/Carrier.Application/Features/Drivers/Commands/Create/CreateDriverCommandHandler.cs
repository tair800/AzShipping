using Carrier.Application.DTOs.Driver;
using Carrier.Application.Features.Drivers;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.DriverAggregate;
using MediatR;

namespace Carrier.Application.Features.Drivers.Commands.Create;

public class CreateDriverCommandHandler(IDriverRepository repository, IActionLogClient actionLogClient) : IRequestHandler<CreateDriverCommand, DriverDto>
{
    public async Task<DriverDto> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new Driver
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Surname = dto.Surname,
            MiddleName = dto.MiddleName,
            Passport = dto.Passport,
            DrivingLicenceNumber = dto.DrivingLicenceNumber,
            PhoneNumber = dto.PhoneNumber,
            BankAccount = dto.BankAccount,
            FuelCard = dto.FuelCard,
            Notes = dto.Notes,
            DateOfEmployment = dto.DateOfEmployment,
            IsDeactive = dto.IsDeactive,
            CreatedAt = DateTime.UtcNow
        };
        if (dto.CarrierIds is { Count: > 0 })
            foreach (var cid in dto.CarrierIds)
                entity.DriverCarriers.Add(new DriverCarrier { DriverId = entity.Id, CarrierId = cid });
        if (dto.DrivingLicenceCategoryIds is { Count: > 0 })
            foreach (var catId in dto.DrivingLicenceCategoryIds)
                entity.DrivingLicenceCategories.Add(new DriverDrivingLicenceCategory { DriverId = entity.Id, DrivingLicenceCategoryId = catId });

        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        var result = DriverMapper.MapToDto(created!);
        await actionLogClient.LogAsync("Driver created", $"driver: {entity.Name} {entity.Surname} • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }
}
