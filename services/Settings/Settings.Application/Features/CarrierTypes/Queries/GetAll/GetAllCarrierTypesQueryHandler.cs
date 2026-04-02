using MediatR;
using Settings.Application.DTOs.CarrierType;
using Settings.Domain.AggregatesModel.CarrierTypeAggregate;

namespace Settings.Application.Features.CarrierTypes.Queries.GetAll;

public sealed class GetAllCarrierTypesQueryHandler(ICarrierTypeRepository repository) : IRequestHandler<GetAllCarrierTypesQuery, IReadOnlyList<CarrierTypeDto>>
{
    public async Task<IReadOnlyList<CarrierTypeDto>> Handle(GetAllCarrierTypesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(c => new CarrierTypeDto(c.Id, c.Name, c.IsActive, c.CreatedAt, c.UpdatedAt)).ToList();
    }
}
