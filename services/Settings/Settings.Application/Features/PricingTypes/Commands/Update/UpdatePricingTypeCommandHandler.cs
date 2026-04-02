using MediatR;
using Settings.Application.DTOs.PricingType;
using Settings.Application.Features.PricingTypes;
using Settings.Domain.AggregatesModel.PricingTypeAggregate;

namespace Settings.Application.Features.PricingTypes.Commands.Update;

public sealed class UpdatePricingTypeCommandHandler(IPricingTypeRepository repository) : IRequestHandler<UpdatePricingTypeCommand, PricingTypeDto?>
{
    public async Task<PricingTypeDto?> Handle(UpdatePricingTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        return PricingTypeMapper.MapToDto(entity);
    }
}
