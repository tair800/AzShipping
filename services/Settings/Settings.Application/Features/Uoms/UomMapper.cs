using Settings.Application.DTOs.Uom;
using Settings.Domain.AggregatesModel.UomAggregate;

namespace Settings.Application.Features.Uoms;

public static class UomMapper
{
    public static UomDto MapToDto(Uom? entity)
    {
        if (entity == null) return null!;
        return new UomDto(entity.Id, entity.Name, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
