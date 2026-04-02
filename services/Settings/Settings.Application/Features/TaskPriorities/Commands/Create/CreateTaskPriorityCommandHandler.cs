using MediatR;
using Settings.Application.DTOs.TaskPriority;
using Settings.Application.Features.TaskPriorities;
using Settings.Domain.AggregatesModel.TaskPriorityAggregate;

namespace Settings.Application.Features.TaskPriorities.Commands.Create;

public sealed class CreateTaskPriorityCommandHandler(ITaskPriorityRepository repository) : IRequestHandler<CreateTaskPriorityCommand, TaskPriorityDto>
{
    public async Task<TaskPriorityDto> Handle(CreateTaskPriorityCommand request, CancellationToken cancellationToken)
    {
        var entity = new TaskPriority
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            PrimaryColor = request.Dto.PrimaryColor,
            SecondaryColor = request.Dto.SecondaryColor,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        return TaskPriorityMapper.MapToDto(entity);
    }
}
