using MediatR;
using Settings.Application.DTOs.ExecutionPlace;
using Settings.Application.Features.ExecutionPlaces;
using Settings.Domain.AggregatesModel.ExecutionPlaceAggregate;

namespace Settings.Application.Features.ExecutionPlaces.Commands.Create;

public sealed class CreateExecutionPlaceCommandHandler(IExecutionPlaceRepository repository) : IRequestHandler<CreateExecutionPlaceCommand, ExecutionPlaceDto>
{
    public async Task<ExecutionPlaceDto> Handle(CreateExecutionPlaceCommand request, CancellationToken cancellationToken)
    {
        var entity = new ExecutionPlace
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        return ExecutionPlaceMapper.MapToDto(entity);
    }
}
