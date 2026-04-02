using Carrier.Application.DTOs.Project;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.Projects.Queries.GetByCarrierId;

public sealed class GetProjectsByCarrierIdQueryHandler(IProjectRepository repository)
    : IRequestHandler<GetProjectsByCarrierIdQuery, IReadOnlyList<ProjectDto>>
{
    public async Task<IReadOnlyList<ProjectDto>> Handle(GetProjectsByCarrierIdQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetByCarrierIdAsync(request.CarrierId, cancellationToken);
        return list.Select(p => new ProjectDto
        {
            Id = p.Id,
            CarrierId = p.CarrierId,
            Name = p.Name,
            CreatedAt = p.CreatedAt
        }).ToList();
    }
}
