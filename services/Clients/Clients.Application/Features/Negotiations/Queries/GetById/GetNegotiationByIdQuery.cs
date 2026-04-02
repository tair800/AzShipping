using Clients.Application.DTOs.Negotiation;
using MediatR;

namespace Clients.Application.Features.Negotiations.Queries.GetById;

public sealed record GetNegotiationByIdQuery(Guid Id) : IRequest<NegotiationDto?>;
