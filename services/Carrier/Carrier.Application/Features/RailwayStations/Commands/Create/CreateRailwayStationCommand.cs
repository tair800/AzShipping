using Carrier.Application.DTOs.RailwayStation;
using MediatR;

namespace Carrier.Application.Features.RailwayStations.Commands.Create;

public record CreateRailwayStationCommand(CreateRailwayStationDto Dto) : IRequest<RailwayStationDto>;
