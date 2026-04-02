using Carrier.Application.DTOs.ShippingAgent;
using MediatR;

namespace Carrier.Application.Features.ShippingAgents.Commands.Update;

public record UpdateShippingAgentCommand(Guid Id, UpdateShippingAgentDto Dto) : IRequest<ShippingAgentDto?>;
