using MediatR;
using Settings.Application.DTOs.TaskStatus;
using Settings.Application.Features.TaskStatuses;
using Settings.Domain.AggregatesModel.TaskStatusAggregate;

namespace Settings.Application.Features.TaskStatuses.Queries.GetById;

public sealed class GetTaskStatusByIdQueryHandler(ITaskStatusRepository repository) : IRequestHandler<GetTaskStatusByIdQuery, TaskStatusDto?>
{
    public async Task<TaskStatusDto?> Handle(GetTaskStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : TaskStatusMapper.MapToDto(entity);
    }
}
