using MediatR;

namespace Carrier.Application.Features.Vehicles.Commands.Delete;

public record DeleteVehicleCommand(Guid Id) : IRequest<bool>;
