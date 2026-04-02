using MediatR;
using Request.Application.DTOs.SaleStatus;
using Request.Application.Features.SaleStatuses;
using Request.Domain.AggregatesModel.SaleStatusAggregate;

namespace Request.Application.Features.SaleStatuses.Queries.GetAll;

public sealed class GetAllSaleStatusesQueryHandler(ISaleStatusRepository repository) : IRequestHandler<GetAllSaleStatusesQuery, IReadOnlyList<SaleStatusDto>>
{
    public async Task<IReadOnlyList<SaleStatusDto>> Handle(GetAllSaleStatusesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(SaleStatusMapper.MapToDto).ToList();
    }
}
