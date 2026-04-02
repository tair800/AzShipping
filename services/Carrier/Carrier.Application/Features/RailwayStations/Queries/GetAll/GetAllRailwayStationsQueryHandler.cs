using Carrier.Application.DTOs.RailwayStation;
using Carrier.Application.Features.RailwayStations;
using Carrier.Domain.AggregatesModel.RailwayStationAggregate;
using MediatR;

namespace Carrier.Application.Features.RailwayStations.Queries.GetAll;

public class GetAllRailwayStationsQueryHandler(IRailwayStationRepository repository)
    : IRequestHandler<GetAllRailwayStationsQuery, IReadOnlyList<RailwayStationDto>>
{
    public async Task<IReadOnlyList<RailwayStationDto>> Handle(GetAllRailwayStationsQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(request.IsActive, cancellationToken);
        return items.Select(RailwayStationMapper.MapToDto).ToList();
    }
}
