using MediatR;
using Request.Application.DTOs.Sale;
using Request.Application.Features.Sales;
using Request.Domain.AggregatesModel.SaleAggregate;

namespace Request.Application.Features.Sales.Queries.GetAll;

public sealed class GetAllSalesQueryHandler(ISaleRepository repository) : IRequestHandler<GetAllSalesQuery, IReadOnlyList<SaleDto>>
{
    public async Task<IReadOnlyList<SaleDto>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(request.ListStatusFilter, cancellationToken);
        return list.Select(SaleMapper.MapToDto).ToList();
    }
}
