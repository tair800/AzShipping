using Clients.Application.DTOs.Negotiation;
using MediatR;

namespace Clients.Application.Features.Negotiations.Commands.Update;

public sealed record UpdateNegotiationCommand(Guid Id, UpdateNegotiationDto Dto) : IRequest<NegotiationDto?>;
