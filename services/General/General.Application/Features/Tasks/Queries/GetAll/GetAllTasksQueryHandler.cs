using General.Application.DTOs.Task;
using General.Application.Features.Tasks;
using General.Domain.AggregatesModel.TaskAggregate;
using MediatR;

namespace General.Application.Features.Tasks.Queries.GetAll;

public class GetAllTasksQueryHandler(ITaskRepository repository)
    : IRequestHandler<GetAllTasksQuery, IReadOnlyList<TaskDto>>
{
    public async System.Threading.Tasks.Task<IReadOnlyList<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(TaskMapper.MapToDto).ToList();
    }
}
