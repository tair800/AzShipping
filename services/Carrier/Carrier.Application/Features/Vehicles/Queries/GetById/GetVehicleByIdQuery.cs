using Carrier.Application.DTOs.Vehicle;
using MediatR;

namespace Carrier.Application.Features.Vehicles.Queries.GetById;

public record GetVehicleByIdQuery(Guid Id) : IRequest<VehicleDto?>;
