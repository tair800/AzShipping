using Carrier.Application.DTOs.Carrier;
using Carrier.Application.Features.Carriers;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.Carriers.Queries.GetById;

public sealed class GetCarrierByIdQueryHandler(ICarrierRepository repository) : IRequestHandler<GetCarrierByIdQuery, CarrierDto?>
{
    public async Task<CarrierDto?> Handle(GetCarrierByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : CarrierMapper.MapToDto(entity);
    }
}
