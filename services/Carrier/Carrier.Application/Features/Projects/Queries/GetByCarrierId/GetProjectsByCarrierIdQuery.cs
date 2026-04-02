using Carrier.Application.DTOs.Project;
using MediatR;

namespace Carrier.Application.Features.Projects.Queries.GetByCarrierId;

public sealed record GetProjectsByCarrierIdQuery(Guid CarrierId) : IRequest<IReadOnlyList<ProjectDto>>;
