using Carrier.Application.DTOs.Project;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.Projects.Commands.Create;

public class CreateProjectCommandHandler(IProjectRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = new Project
        {
            Id = Guid.NewGuid(),
            CarrierId = request.CarrierId,
            Name = string.IsNullOrWhiteSpace(request.Dto.Name) ? "Project" : request.Dto.Name.Trim(),
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        await actionLogClient.LogAsync("Project created", $"project: {entity.Name} • id: {entity.Id}", null, null, cancellationToken);
        return new ProjectDto
        {
            Id = entity.Id,
            CarrierId = entity.CarrierId,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt
        };
    }
}
