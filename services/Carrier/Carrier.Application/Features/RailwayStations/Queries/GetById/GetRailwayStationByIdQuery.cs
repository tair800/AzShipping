using Carrier.Application.DTOs.RailwayStation;
using MediatR;

namespace Carrier.Application.Features.RailwayStations.Queries.GetById;

public record GetRailwayStationByIdQuery(Guid Id) : IRequest<RailwayStationDto?>;
