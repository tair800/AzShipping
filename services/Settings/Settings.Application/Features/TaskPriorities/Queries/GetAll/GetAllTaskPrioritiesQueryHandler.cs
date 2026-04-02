using MediatR;
using Settings.Application.DTOs.TaskPriority;
using Settings.Application.Features.TaskPriorities;
using Settings.Domain.AggregatesModel.TaskPriorityAggregate;

namespace Settings.Application.Features.TaskPriorities.Queries.GetAll;

public sealed class GetAllTaskPrioritiesQueryHandler(ITaskPriorityRepository repository) : IRequestHandler<GetAllTaskPrioritiesQuery, IReadOnlyList<TaskPriorityDto>>
{
    public async Task<IReadOnlyList<TaskPriorityDto>> Handle(GetAllTaskPrioritiesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(TaskPriorityMapper.MapToDto).ToList();
    }
}
