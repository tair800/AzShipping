using Carrier.Application.DTOs.CarrierDirection;
using Carrier.Application.Features.CarrierDirections;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.CarrierDirections.Queries.GetById;

public class GetCarrierDirectionByIdQueryHandler(ICarrierDirectionRepository repository) : IRequestHandler<GetCarrierDirectionByIdQuery, CarrierDirectionDto?>
{
    public async Task<CarrierDirectionDto?> Handle(GetCarrierDirectionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : CarrierDirectionMapper.MapToDto(entity);
    }
}
