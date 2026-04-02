using Carrier.Application.DTOs.Vehicle;
using Carrier.Application.Features.Vehicles;
using Carrier.Domain.AggregatesModel.VehicleAggregate;
using MediatR;

namespace Carrier.Application.Features.Vehicles.Queries.GetAll;

public class GetAllVehiclesQueryHandler(IVehicleRepository repository) : IRequestHandler<GetAllVehiclesQuery, IReadOnlyList<VehicleDto>>
{
    public async Task<IReadOnlyList<VehicleDto>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(cancellationToken);
        return items.Select(VehicleMapper.MapToDto).ToList();
    }
}
