using MediatR;
using Settings.Application.DTOs.PricingType;
using Settings.Application.Features.PricingTypes;
using Settings.Domain.AggregatesModel.PricingTypeAggregate;

namespace Settings.Application.Features.PricingTypes.Queries.GetById;

public sealed class GetPricingTypeByIdQueryHandler(IPricingTypeRepository repository) : IRequestHandler<GetPricingTypeByIdQuery, PricingTypeDto?>
{
    public async Task<PricingTypeDto?> Handle(GetPricingTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : PricingTypeMapper.MapToDto(entity);
    }
}
