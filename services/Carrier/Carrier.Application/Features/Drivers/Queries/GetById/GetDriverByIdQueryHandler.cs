using Carrier.Application.DTOs.Driver;
using Carrier.Application.Features.Drivers;
using Carrier.Domain.AggregatesModel.DriverAggregate;
using MediatR;

namespace Carrier.Application.Features.Drivers.Queries.GetById;

public class GetDriverByIdQueryHandler(IDriverRepository repository) : IRequestHandler<GetDriverByIdQuery, DriverDto?>
{
    public async Task<DriverDto?> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : DriverMapper.MapToDto(entity);
    }
}
