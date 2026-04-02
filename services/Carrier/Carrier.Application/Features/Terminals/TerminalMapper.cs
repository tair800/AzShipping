using Carrier.Application.DTOs.Terminal;
using Carrier.Domain.AggregatesModel.TerminalAggregate;

namespace Carrier.Application.Features.Terminals;

public static class TerminalMapper
{
    public static TerminalDto MapToDto(Terminal? entity)
    {
        if (entity == null) return new TerminalDto();
        var transportIds = string.IsNullOrEmpty(entity.TransportTypeIds)
            ? new List<Guid>()
            : entity.TransportTypeIds.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(s => Guid.TryParse(s, out var g) ? g : Guid.Empty)
                .Where(g => g != Guid.Empty)
                .ToList();
        return new TerminalDto
        {
            Id = entity.Id,
            Name = entity.Name,
            CountryId = entity.CountryId,
            CityId = entity.CityId,
            Address = entity.Address,
            PostCode = entity.PostCode,
            RailwayStation = entity.RailwayStation,
            TransportTypeIds = transportIds,
            Notes = entity.Notes,
            IsDeactive = entity.IsDeactive,
            DateOfCreation = entity.DateOfCreation,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }

    public static string ToTransportTypeIdsString(List<Guid> ids)
        => ids == null || ids.Count == 0 ? string.Empty : string.Join(",", ids.Select(g => g.ToString()));
}
