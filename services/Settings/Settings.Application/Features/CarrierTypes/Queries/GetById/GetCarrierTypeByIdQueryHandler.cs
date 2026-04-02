using MediatR;
using Settings.Application.DTOs.CarrierType;
using Settings.Domain.AggregatesModel.CarrierTypeAggregate;

namespace Settings.Application.Features.CarrierTypes.Queries.GetById;

public sealed class GetCarrierTypeByIdQueryHandler(ICarrierTypeRepository repository) : IRequestHandler<GetCarrierTypeByIdQuery, CarrierTypeDto?>
{
    public async Task<CarrierTypeDto?> Handle(GetCarrierTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var c = await repository.GetByIdAsync(request.Id, cancellationToken);
        return c == null ? null : new CarrierTypeDto(c.Id, c.Name, c.IsActive, c.CreatedAt, c.UpdatedAt);
    }
}
