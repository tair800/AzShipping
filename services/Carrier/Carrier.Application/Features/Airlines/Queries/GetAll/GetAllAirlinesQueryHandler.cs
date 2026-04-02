using Carrier.Application.DTOs.Airline;
using Carrier.Application.Features.Airlines;
using Carrier.Domain.AggregatesModel.AirlineAggregate;
using MediatR;

namespace Carrier.Application.Features.Airlines.Queries.GetAll;

public class GetAllAirlinesQueryHandler(IAirlineRepository repository)
    : IRequestHandler<GetAllAirlinesQuery, IReadOnlyList<AirlineDto>>
{
    public async Task<IReadOnlyList<AirlineDto>> Handle(GetAllAirlinesQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(request.IsActive, cancellationToken);
        return items.Select(AirlineMapper.MapToDto).ToList();
    }
}
