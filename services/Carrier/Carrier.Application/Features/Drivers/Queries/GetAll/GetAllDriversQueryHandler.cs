using Carrier.Application.DTOs.Driver;
using Carrier.Application.Features.Drivers;
using Carrier.Domain.AggregatesModel.DriverAggregate;
using MediatR;

namespace Carrier.Application.Features.Drivers.Queries.GetAll;

public class GetAllDriversQueryHandler(IDriverRepository repository) : IRequestHandler<GetAllDriversQuery, IReadOnlyList<DriverDto>>
{
    public async Task<IReadOnlyList<DriverDto>> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(cancellationToken);
        return items.Select(DriverMapper.MapToDto).ToList();
    }
}
