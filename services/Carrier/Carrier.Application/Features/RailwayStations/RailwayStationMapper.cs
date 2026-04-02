using Carrier.Application.DTOs.RailwayStation;
using Carrier.Domain.AggregatesModel.RailwayStationAggregate;

namespace Carrier.Application.Features.RailwayStations;

public static class RailwayStationMapper
{
    public static RailwayStationDto MapToDto(RailwayStation? entity)
    {
        if (entity == null) return null!;
        return new RailwayStationDto
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Code = entity.Code,
            Name = entity.Name,
            Railway = entity.Railway,
            LocalName = entity.LocalName,
            Prefix = entity.Prefix,
            Website = entity.Website,
            VatNo = entity.VatNo,
            Notes = entity.Notes,
            IsActive = entity.IsActive
        };
    }
}
