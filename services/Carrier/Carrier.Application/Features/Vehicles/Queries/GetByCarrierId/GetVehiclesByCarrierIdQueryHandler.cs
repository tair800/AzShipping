using Carrier.Application.DTOs.Vehicle;
using Carrier.Application.Features.Vehicles;
using Carrier.Domain.AggregatesModel.VehicleAggregate;
using MediatR;

namespace Carrier.Application.Features.Vehicles.Queries.GetByCarrierId;

public class GetVehiclesByCarrierIdQueryHandler(IVehicleRepository repository)
    : IRequestHandler<GetVehiclesByCarrierIdQuery, IReadOnlyList<VehicleDto>>
{
    public async Task<IReadOnlyList<VehicleDto>> Handle(GetVehiclesByCarrierIdQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetByCarrierIdAsync(request.CarrierId, cancellationToken);
        return items.Select(VehicleMapper.MapToDto).ToList();
    }
}
