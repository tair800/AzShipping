using General.Application.DTOs.Project;
using General.Domain.AggregatesModel.ProjectAggregate;
using MediatR;

namespace General.Application.Features.Projects.Queries.GetAll;

public class GetAllProjectsQueryHandler(IProjectRepository repository)
    : IRequestHandler<GetAllProjectsQuery, IReadOnlyList<ProjectDto>>
{
    public async System.Threading.Tasks.Task<IReadOnlyList<ProjectDto>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(p => new ProjectDto
        {
            Id = p.Id,
            Name = p.Name,
            CreatedAt = p.CreatedAt
        }).ToList();
    }
}
