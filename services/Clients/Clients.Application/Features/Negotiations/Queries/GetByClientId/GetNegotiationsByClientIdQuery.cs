using Clients.Application.DTOs.Negotiation;
using MediatR;

namespace Clients.Application.Features.Negotiations.Queries.GetByClientId;

public sealed record GetNegotiationsByClientIdQuery(Guid ClientId) : IRequest<IReadOnlyList<NegotiationDto>>;
