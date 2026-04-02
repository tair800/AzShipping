using Carrier.Application.DTOs.CarrierDirection;
using Carrier.Application.Features.CarrierDirections;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.CarrierDirections.Queries.GetByCarrierId;

public class GetCarrierDirectionsQueryHandler(ICarrierDirectionRepository repository) : IRequestHandler<GetCarrierDirectionsQuery, IReadOnlyList<CarrierDirectionDto>>
{
    public async Task<IReadOnlyList<CarrierDirectionDto>> Handle(GetCarrierDirectionsQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetByCarrierIdAsync(request.CarrierId, cancellationToken);
        return items.Select(CarrierDirectionMapper.MapToDto).ToList();
    }
}
