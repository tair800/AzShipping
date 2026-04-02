using Carrier.Application.DTOs.CarrierDirection;
using Carrier.Domain.AggregatesModel.CarrierAggregate;

namespace Carrier.Application.Features.CarrierDirections;

public static class CarrierDirectionMapper
{
    public static CarrierDirectionDto MapToDto(CarrierDirection? entity)
    {
        if (entity == null) return new CarrierDirectionDto();
        return new CarrierDirectionDto
        {
            Id = entity.Id,
            CarrierId = entity.CarrierId,
            DepartureCountryId = entity.DepartureCountryId,
            DepartureGlobalZoneId = entity.DepartureGlobalZoneId,
            DepartureCityId = entity.DepartureCityId,
            ArrivalCountryId = entity.ArrivalCountryId,
            ArrivalGlobalZoneId = entity.ArrivalGlobalZoneId,
            ArrivalCityId = entity.ArrivalCityId,
            CarrierLicences = entity.CarrierLicences,
            Comments = entity.Comments,
            TransportTypeIds = entity.TransportTypes?.Select(t => t.TransportTypeId).ToList() ?? new List<Guid>()
        };
    }
}
