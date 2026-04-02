using Carrier.Application.DTOs.Project;
using MediatR;

namespace Carrier.Application.Features.Projects.Commands.Create;

public record CreateProjectCommand(Guid CarrierId, CreateProjectDto Dto) : IRequest<ProjectDto>;
