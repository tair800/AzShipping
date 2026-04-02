using Carrier.Application.DTOs.ShippingAgent;
using Carrier.Application.Features.ShippingAgents;
using Carrier.Domain.AggregatesModel.ShippingAgentAggregate;
using MediatR;

namespace Carrier.Application.Features.ShippingAgents.Queries.GetById;

public class GetShippingAgentByIdQueryHandler(IShippingAgentRepository repository)
    : IRequestHandler<GetShippingAgentByIdQuery, ShippingAgentDto?>
{
    public async Task<ShippingAgentDto?> Handle(GetShippingAgentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return ShippingAgentMapper.MapToDto(entity);
    }
}
