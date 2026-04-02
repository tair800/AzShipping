using MediatR;
using Settings.Application.DTOs.PricingType;
using Settings.Application.Features.PricingTypes;
using Settings.Domain.AggregatesModel.PricingTypeAggregate;

namespace Settings.Application.Features.PricingTypes.Queries.GetAll;

public sealed class GetAllPricingTypesQueryHandler(IPricingTypeRepository repository) : IRequestHandler<GetAllPricingTypesQuery, IReadOnlyList<PricingTypeDto>>
{
    public async Task<IReadOnlyList<PricingTypeDto>> Handle(GetAllPricingTypesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(PricingTypeMapper.MapToDto).ToList();
    }
}
