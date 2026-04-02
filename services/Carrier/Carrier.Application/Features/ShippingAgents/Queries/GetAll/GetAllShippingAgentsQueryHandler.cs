using Carrier.Application.DTOs.ShippingAgent;
using Carrier.Application.Features.ShippingAgents;
using Carrier.Domain.AggregatesModel.ShippingAgentAggregate;
using MediatR;

namespace Carrier.Application.Features.ShippingAgents.Queries.GetAll;

public class GetAllShippingAgentsQueryHandler(IShippingAgentRepository repository)
    : IRequestHandler<GetAllShippingAgentsQuery, IReadOnlyList<ShippingAgentDto>>
{
    public async Task<IReadOnlyList<ShippingAgentDto>> Handle(GetAllShippingAgentsQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(request.IsActive, cancellationToken);
        return items.Select(ShippingAgentMapper.MapToDto).ToList();
    }
}
