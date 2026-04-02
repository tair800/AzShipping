using MediatR;
using Settings.Application.DTOs.CarrierType;
using Settings.Domain.AggregatesModel.CarrierTypeAggregate;

namespace Settings.Application.Features.CarrierTypes.Commands.Create;

public sealed class CreateCarrierTypeCommandHandler(ICarrierTypeRepository repository) : IRequestHandler<CreateCarrierTypeCommand, CarrierTypeDto>
{
    public async Task<CarrierTypeDto> Handle(CreateCarrierTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = new CarrierType
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        return new CarrierTypeDto(entity.Id, entity.Name, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
