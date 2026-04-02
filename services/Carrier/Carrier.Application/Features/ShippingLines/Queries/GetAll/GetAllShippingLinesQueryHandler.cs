using Carrier.Application.DTOs.ShippingLine;
using Carrier.Application.Features.ShippingLines;
using Carrier.Domain.AggregatesModel.ShippingLineAggregate;
using MediatR;

namespace Carrier.Application.Features.ShippingLines.Queries.GetAll;

public class GetAllShippingLinesQueryHandler(IShippingLineRepository repository)
    : IRequestHandler<GetAllShippingLinesQuery, IReadOnlyList<ShippingLineDto>>
{
    public async Task<IReadOnlyList<ShippingLineDto>> Handle(GetAllShippingLinesQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(request.IsActive, cancellationToken);
        return items.Select(ShippingLineMapper.MapToDto).ToList();
    }
}
