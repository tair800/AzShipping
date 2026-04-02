using Carrier.Application.DTOs.Driver;
using Carrier.Application.Features.Drivers;
using Carrier.Domain.AggregatesModel.DriverAggregate;
using MediatR;

namespace Carrier.Application.Features.Drivers.Queries.GetByCarrierId;

public class GetDriversByCarrierIdQueryHandler(IDriverRepository repository)
    : IRequestHandler<GetDriversByCarrierIdQuery, IReadOnlyList<DriverDto>>
{
    public async Task<IReadOnlyList<DriverDto>> Handle(GetDriversByCarrierIdQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetByCarrierIdAsync(request.CarrierId, cancellationToken);
        return items.Select(DriverMapper.MapToDto).ToList();
    }
}
