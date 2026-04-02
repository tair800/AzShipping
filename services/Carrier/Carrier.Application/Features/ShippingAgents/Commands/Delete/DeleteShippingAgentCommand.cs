using MediatR;

namespace Carrier.Application.Features.ShippingAgents.Commands.Delete;

public record DeleteShippingAgentCommand(Guid Id) : IRequest<bool>;
