using Settings.Application.DTOs.PricingType;
using Settings.Domain.AggregatesModel.PricingTypeAggregate;

namespace Settings.Application.Features.PricingTypes;

public static class PricingTypeMapper
{
    public static PricingTypeDto MapToDto(PricingType? entity)
    {
        if (entity == null) return null!;
        return new PricingTypeDto(entity.Id, entity.Name, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
