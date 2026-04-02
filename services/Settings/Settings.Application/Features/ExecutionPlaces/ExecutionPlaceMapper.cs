using Settings.Application.DTOs.ExecutionPlace;
using Settings.Domain.AggregatesModel.ExecutionPlaceAggregate;

namespace Settings.Application.Features.ExecutionPlaces;

public static class ExecutionPlaceMapper
{
    public static ExecutionPlaceDto MapToDto(ExecutionPlace? entity)
    {
        if (entity == null) return null!;
        return new ExecutionPlaceDto(entity.Id, entity.Name, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
