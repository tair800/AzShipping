using MediatR;
using Settings.Application.DTOs.PricingType;
using Settings.Application.Features.PricingTypes;
using Settings.Domain.AggregatesModel.PricingTypeAggregate;

namespace Settings.Application.Features.PricingTypes.Commands.Create;

public sealed class CreatePricingTypeCommandHandler(IPricingTypeRepository repository) : IRequestHandler<CreatePricingTypeCommand, PricingTypeDto>
{
    public async Task<PricingTypeDto> Handle(CreatePricingTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = new PricingType
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        return PricingTypeMapper.MapToDto(entity);
    }
}
