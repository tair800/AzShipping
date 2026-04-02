using Carrier.Application.DTOs.ShippingAgent;
using MediatR;

namespace Carrier.Application.Features.ShippingAgents.Queries.GetById;

public record GetShippingAgentByIdQuery(Guid Id) : IRequest<ShippingAgentDto?>;
