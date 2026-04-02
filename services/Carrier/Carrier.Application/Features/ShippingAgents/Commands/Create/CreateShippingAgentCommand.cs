using Carrier.Application.DTOs.ShippingAgent;
using MediatR;

namespace Carrier.Application.Features.ShippingAgents.Commands.Create;

public record CreateShippingAgentCommand(CreateShippingAgentDto Dto) : IRequest<ShippingAgentDto>;
