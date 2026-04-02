using Carrier.Application.DTOs.Vehicle;
using MediatR;

namespace Carrier.Application.Features.Vehicles.Commands.Create;

public record CreateVehicleCommand(CreateVehicleDto Dto) : IRequest<VehicleDto>;
