using MediatR;
using Settings.Application.DTOs.TaskPriority;
using Settings.Application.Features.TaskPriorities;
using Settings.Domain.AggregatesModel.TaskPriorityAggregate;

namespace Settings.Application.Features.TaskPriorities.Queries.GetById;

public sealed class GetTaskPriorityByIdQueryHandler(ITaskPriorityRepository repository) : IRequestHandler<GetTaskPriorityByIdQuery, TaskPriorityDto?>
{
    public async Task<TaskPriorityDto?> Handle(GetTaskPriorityByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : TaskPriorityMapper.MapToDto(entity);
    }
}
