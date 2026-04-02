using Carrier.Application.DTOs.RailwayStation;
using MediatR;

namespace Carrier.Application.Features.RailwayStations.Queries.GetAll;

public record GetAllRailwayStationsQuery(bool? IsActive) : IRequest<IReadOnlyList<RailwayStationDto>>;
