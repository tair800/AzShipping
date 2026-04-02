using MediatR;
using Settings.Application.DTOs.TaskStatus;
using Settings.Application.Features.TaskStatuses;
using Settings.Domain.AggregatesModel.TaskStatusAggregate;

namespace Settings.Application.Features.TaskStatuses.Queries.GetAll;

public sealed class GetAllTaskStatusesQueryHandler(ITaskStatusRepository repository) : IRequestHandler<GetAllTaskStatusesQuery, IReadOnlyList<TaskStatusDto>>
{
    public async Task<IReadOnlyList<TaskStatusDto>> Handle(GetAllTaskStatusesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(TaskStatusMapper.MapToDto).ToList();
    }
}
