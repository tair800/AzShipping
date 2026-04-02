using Carrier.Application.DTOs.ShippingAgent;
using MediatR;

namespace Carrier.Application.Features.ShippingAgents.Queries.GetAll;

public record GetAllShippingAgentsQuery(bool? IsActive) : IRequest<IReadOnlyList<ShippingAgentDto>>;
