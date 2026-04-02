using Carrier.Application.DTOs.Vehicle;
using MediatR;

namespace Carrier.Application.Features.Vehicles.Commands.Update;

public record UpdateVehicleCommand(Guid Id, UpdateVehicleDto Dto) : IRequest<VehicleDto?>;
