using Carrier.Application.DTOs.CarrierDirection;
using Carrier.Application.Features.CarrierDirections;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.CarrierDirections.Commands.Create;

public class CreateCarrierDirectionCommandHandler(ICarrierDirectionRepository repository, IActionLogClient actionLogClient) : IRequestHandler<CreateCarrierDirectionCommand, CarrierDirectionDto>
{
    public async Task<CarrierDirectionDto> Handle(CreateCarrierDirectionCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new CarrierDirection
        {
            Id = Guid.NewGuid(),
            CarrierId = request.CarrierId,
            DepartureCountryId = dto.DepartureCountryId,
            DepartureGlobalZoneId = dto.DepartureGlobalZoneId,
            DepartureCityId = dto.DepartureCityId,
            ArrivalCountryId = dto.ArrivalCountryId,
            ArrivalGlobalZoneId = dto.ArrivalGlobalZoneId,
            ArrivalCityId = dto.ArrivalCityId,
            CarrierLicences = dto.CarrierLicences,
            Comments = dto.Comments
        };
        if (dto.TransportTypeIds is { Count: > 0 })
            foreach (var ttId in dto.TransportTypeIds)
                entity.TransportTypes.Add(new CarrierDirectionTransportType { CarrierDirectionId = entity.Id, TransportTypeId = ttId });

        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        await actionLogClient.LogAsync("Carrier direction created", $"carrier direction: carrier {entity.CarrierId} • id: {entity.Id}", null, null, cancellationToken);
        return CarrierDirectionMapper.MapToDto(created!);
    }
}
