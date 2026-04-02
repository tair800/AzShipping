using General.Application.DTOs.Project;
using MediatR;

namespace General.Application.Features.Projects.Queries.GetAll;

public record GetAllProjectsQuery : IRequest<IReadOnlyList<ProjectDto>>;
