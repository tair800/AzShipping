using Carrier.Application.DTOs.ShippingLine;
using Carrier.Application.Features.ShippingLines;
using Carrier.Domain.AggregatesModel.ShippingLineAggregate;
using MediatR;

namespace Carrier.Application.Features.ShippingLines.Queries.GetById;

public class GetShippingLineByIdQueryHandler(IShippingLineRepository repository)
    : IRequestHandler<GetShippingLineByIdQuery, ShippingLineDto?>
{
    public async Task<ShippingLineDto?> Handle(GetShippingLineByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return ShippingLineMapper.MapToDto(entity);
    }
}
