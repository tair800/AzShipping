using General.Application.DTOs.Vas;
using VasEntity = General.Domain.AggregatesModel.VasAggregate.Vas;

namespace General.Application.Features.Vas;

public static class VasMapper
{
    public static VasDto MapToDto(VasEntity? entity)
    {
        if (entity == null) return null!;
        return new VasDto
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Code = entity.Code,
            Name = entity.Name,
            OverWidth = entity.OverWidth,
            OverHeight = entity.OverHeight,
            OverWeight = entity.OverWeight,
            IsMandatory = entity.IsMandatory,
            ExecutionPlace = entity.ExecutionPlace,
            Uom = entity.Uom,
            IsAir = entity.IsAir,
            IsSea = entity.IsSea,
            IsRoad = entity.IsRoad,
            IsRail = entity.IsRail,
            Notes = entity.Notes,
            IsActive = entity.IsActive,
            IsDeleted = entity.IsDeleted,
            Amount = entity.Amount,
            CurrencyId = entity.CurrencyId,
            CurrencyCode = entity.Currency?.Code
        };
    }
}
