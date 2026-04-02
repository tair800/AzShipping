using MediatR;
using Settings.Application.DTOs.ExecutionPlace;
using Settings.Application.Features.ExecutionPlaces;
using Settings.Domain.AggregatesModel.ExecutionPlaceAggregate;

namespace Settings.Application.Features.ExecutionPlaces.Commands.Update;

public sealed class UpdateExecutionPlaceCommandHandler(IExecutionPlaceRepository repository) : IRequestHandler<UpdateExecutionPlaceCommand, ExecutionPlaceDto?>
{
    public async Task<ExecutionPlaceDto?> Handle(UpdateExecutionPlaceCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        return ExecutionPlaceMapper.MapToDto(entity);
    }
}
