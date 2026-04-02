using MediatR;
using Settings.Application.DTOs.CarrierType;
using Settings.Domain.AggregatesModel.CarrierTypeAggregate;

namespace Settings.Application.Features.CarrierTypes.Commands.Update;

public sealed class UpdateCarrierTypeCommandHandler(ICarrierTypeRepository repository) : IRequestHandler<UpdateCarrierTypeCommand, CarrierTypeDto?>
{
    public async Task<CarrierTypeDto?> Handle(UpdateCarrierTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        return new CarrierTypeDto(entity.Id, entity.Name, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
