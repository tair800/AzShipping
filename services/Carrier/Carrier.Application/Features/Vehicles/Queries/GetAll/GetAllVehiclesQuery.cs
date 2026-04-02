using Carrier.Application.DTOs.Vehicle;
using MediatR;

namespace Carrier.Application.Features.Vehicles.Queries.GetAll;

public record GetAllVehiclesQuery : IRequest<IReadOnlyList<VehicleDto>>;
