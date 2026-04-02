using MediatR;

namespace Carrier.Application.Features.RailwayStations.Commands.Delete;

public record DeleteRailwayStationCommand(Guid Id) : IRequest<bool>;
