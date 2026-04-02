using MediatR;
using Settings.Application.DTOs.TaskStatus;
using Settings.Application.Features.TaskStatuses;
using Settings.Domain.AggregatesModel.TaskStatusAggregate;
using TaskStatusEntity = Settings.Domain.AggregatesModel.TaskStatusAggregate.TaskStatus;

namespace Settings.Application.Features.TaskStatuses.Commands.Create;

public sealed class CreateTaskStatusCommandHandler(ITaskStatusRepository repository) : IRequestHandler<CreateTaskStatusCommand, TaskStatusDto>
{
    public async Task<TaskStatusDto> Handle(CreateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = new TaskStatusEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            PrimaryColor = request.Dto.PrimaryColor,
            SecondaryColor = request.Dto.SecondaryColor,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        return TaskStatusMapper.MapToDto(entity);
    }
}
