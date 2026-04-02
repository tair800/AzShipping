using Carrier.Application.DTOs.CarrierDirection;
using Carrier.Application.Features.CarrierDirections;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.CarrierDirections.Commands.Update;

public class UpdateCarrierDirectionCommandHandler(ICarrierDirectionRepository repository, IActionLogClient actionLogClient) : IRequestHandler<UpdateCarrierDirectionCommand, CarrierDirectionDto?>
{
    public async Task<CarrierDirectionDto?> Handle(UpdateCarrierDirectionCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return null;

        var dto = request.Dto;
        existing.DepartureCountryId = dto.DepartureCountryId;
        existing.DepartureGlobalZoneId = dto.DepartureGlobalZoneId;
        existing.DepartureCityId = dto.DepartureCityId;
        existing.ArrivalCountryId = dto.ArrivalCountryId;
        existing.ArrivalGlobalZoneId = dto.ArrivalGlobalZoneId;
        existing.ArrivalCityId = dto.ArrivalCityId;
        existing.CarrierLicences = dto.CarrierLicences;
        existing.Comments = dto.Comments;

        existing.TransportTypes.Clear();
        if (dto.TransportTypeIds is { Count: > 0 })
            foreach (var ttId in dto.TransportTypeIds)
                existing.TransportTypes.Add(new CarrierDirectionTransportType { CarrierDirectionId = existing.Id, TransportTypeId = ttId });

        await repository.UpdateAsync(existing, cancellationToken);
        var updated = await repository.GetByIdAsync(existing.Id, cancellationToken);
        await actionLogClient.LogAsync("Carrier direction updated", $"carrier direction: carrier {existing.CarrierId} • id: {existing.Id}", null, null, cancellationToken);
        return CarrierDirectionMapper.MapToDto(updated!);
    }
}
