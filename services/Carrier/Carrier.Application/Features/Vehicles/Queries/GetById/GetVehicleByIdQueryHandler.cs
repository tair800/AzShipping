using Carrier.Application.DTOs.Vehicle;
using Carrier.Application.Features.Vehicles;
using Carrier.Domain.AggregatesModel.VehicleAggregate;
using MediatR;

namespace Carrier.Application.Features.Vehicles.Queries.GetById;

public class GetVehicleByIdQueryHandler(IVehicleRepository repository) : IRequestHandler<GetVehicleByIdQuery, VehicleDto?>
{
    public async Task<VehicleDto?> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return VehicleMapper.MapToDto(entity);
    }
}
