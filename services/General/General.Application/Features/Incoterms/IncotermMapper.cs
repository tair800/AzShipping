using General.Application.DTOs.Incoterm;
using General.Domain.AggregatesModel.IncotermAggregate;

namespace General.Application.Features.Incoterms;

public static class IncotermMapper
{
    public static IncotermDto MapToDto(Incoterm? entity)
    {
        if (entity == null) return null!;
        return new IncotermDto
        {
            Id = entity.Id,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Code = entity.Code,
            Name = entity.Name,
            LocalName = entity.LocalName,
            Freight = entity.Freight,
            OtherCharges = entity.OtherCharges,
            IsActive = entity.IsActive,
            IsDeleted = entity.IsDeleted
        };
    }
}
