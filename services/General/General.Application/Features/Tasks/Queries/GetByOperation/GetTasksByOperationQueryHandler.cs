using General.Application.DTOs.Task;
using General.Application.Features.Tasks;
using General.Domain.AggregatesModel.TaskAggregate;
using MediatR;

namespace General.Application.Features.Tasks.Queries.GetByOperation;

public sealed class GetTasksByOperationQueryHandler(ITaskRepository repository)
    : IRequestHandler<GetTasksByOperationQuery, IReadOnlyList<TaskDto>>
{
    public async Task<IReadOnlyList<TaskDto>> Handle(GetTasksByOperationQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetByOperationIdAsync(request.OperationId, cancellationToken);
        return list.Select(TaskMapper.MapToDto).ToList();
    }
}
