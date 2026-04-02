using Carrier.Application.DTOs.Airline;
using Carrier.Domain.AggregatesModel.AirlineAggregate;

namespace Carrier.Application.Features.Airlines;

public static class AirlineMapper
{
    public static AirlineDto MapToDto(Airline? entity)
    {
        if (entity == null) return null!;
        return new AirlineDto
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Code = entity.Code,
            Icao = entity.Icao,
            Name = entity.Name,
            LocalName = entity.LocalName,
            Prefix = entity.Prefix,
            Website = entity.Website,
            VatNo = entity.VatNo,
            Notes = entity.Notes,
            IsActive = entity.IsActive
        };
    }
}
