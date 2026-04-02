using MediatR;
using Request.Application.DTOs.SaleStatus;
using Request.Application.Features.SaleStatuses;
using Request.Domain.AggregatesModel.SaleStatusAggregate;

namespace Request.Application.Features.SaleStatuses.Queries.GetById;

public sealed class GetSaleStatusByIdQueryHandler(ISaleStatusRepository repository) : IRequestHandler<GetSaleStatusByIdQuery, SaleStatusDto?>
{
    public async Task<SaleStatusDto?> Handle(GetSaleStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return SaleStatusMapper.MapToDto(entity);
    }
}
