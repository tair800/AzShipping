using Carrier.Application.DTOs.RailwayStation;
using Carrier.Application.Features.RailwayStations;
using Carrier.Domain.AggregatesModel.RailwayStationAggregate;
using MediatR;

namespace Carrier.Application.Features.RailwayStations.Queries.GetById;

public class GetRailwayStationByIdQueryHandler(IRailwayStationRepository repository)
    : IRequestHandler<GetRailwayStationByIdQuery, RailwayStationDto?>
{
    public async Task<RailwayStationDto?> Handle(GetRailwayStationByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return RailwayStationMapper.MapToDto(entity);
    }
}
