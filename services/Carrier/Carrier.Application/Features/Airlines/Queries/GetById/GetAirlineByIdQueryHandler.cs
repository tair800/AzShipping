using Carrier.Application.DTOs.Airline;
using Carrier.Application.Features.Airlines;
using Carrier.Domain.AggregatesModel.AirlineAggregate;
using MediatR;

namespace Carrier.Application.Features.Airlines.Queries.GetById;

public class GetAirlineByIdQueryHandler(IAirlineRepository repository)
    : IRequestHandler<GetAirlineByIdQuery, AirlineDto?>
{
    public async Task<AirlineDto?> Handle(GetAirlineByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return AirlineMapper.MapToDto(entity);
    }
}
