using Clients.Application.DTOs.Negotiation;
using MediatR;

namespace Clients.Application.Features.Negotiations.Commands.Create;

public sealed record CreateNegotiationCommand(CreateNegotiationDto Dto) : IRequest<NegotiationDto>;
