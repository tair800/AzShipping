using Clients.Application.DTOs.Direction;
using MediatR;

namespace Clients.Application.Features.Directions.Queries.GetByClientId;

public sealed record GetDirectionsByClientIdQuery(Guid ClientId) : IRequest<IReadOnlyList<DirectionDto>>;
