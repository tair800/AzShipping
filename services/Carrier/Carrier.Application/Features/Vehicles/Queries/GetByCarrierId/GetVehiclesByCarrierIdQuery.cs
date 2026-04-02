using Carrier.Application.DTOs.Vehicle;
using MediatR;

namespace Carrier.Application.Features.Vehicles.Queries.GetByCarrierId;

public record GetVehiclesByCarrierIdQuery(Guid CarrierId) : IRequest<IReadOnlyList<VehicleDto>>;
