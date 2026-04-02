using General.Application.DTOs.Vessel;
using General.Domain.AggregatesModel.VesselAggregate;

namespace General.Application.Features.Vessels;

public static class VesselMapper
{
    public static VesselDto MapToDto(Vessel? entity)
    {
        if (entity == null) return null!;
        return new VesselDto
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Code = entity.Code,
            Name = entity.Name,
            ImoCode = entity.ImoCode,
            LocalName = entity.LocalName,
            CountryId = entity.CountryId,
            Notes = entity.Notes,
            IsActive = entity.IsActive,
            IsDeleted = entity.IsDeleted
        };
    }
}
