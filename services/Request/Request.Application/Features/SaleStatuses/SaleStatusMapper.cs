using Request.Application.DTOs.SaleStatus;
using Request.Domain.AggregatesModel.SaleStatusAggregate;

namespace Request.Application.Features.SaleStatuses;

public static class SaleStatusMapper
{
    public static SaleStatusDto MapToDto(SaleStatus? entity)
    {
        if (entity == null) return null!;
        return new SaleStatusDto(entity.Id, entity.Name, entity.SortOrder, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
