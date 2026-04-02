using Carrier.Application.DTOs.Carrier;
using Carrier.Application.Features.Carriers;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.Carriers.Queries.GetAll;

public sealed class GetAllCarriersQueryHandler(ICarrierRepository repository) : IRequestHandler<GetAllCarriersQuery, IReadOnlyList<CarrierDto>>
{
    public async Task<IReadOnlyList<CarrierDto>> Handle(GetAllCarriersQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(CarrierMapper.MapToDto).ToList();
    }
}
