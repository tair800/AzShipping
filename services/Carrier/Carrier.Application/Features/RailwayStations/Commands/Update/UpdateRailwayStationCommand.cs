using Carrier.Application.DTOs.RailwayStation;
using MediatR;

namespace Carrier.Application.Features.RailwayStations.Commands.Update;

public record UpdateRailwayStationCommand(Guid Id, UpdateRailwayStationDto Dto) : IRequest<RailwayStationDto?>;
