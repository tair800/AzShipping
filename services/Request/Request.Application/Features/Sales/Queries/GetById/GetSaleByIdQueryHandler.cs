using MediatR;
using Request.Application.DTOs.Sale;
using Request.Application.Features.Sales;
using Request.Domain.AggregatesModel.SaleAggregate;

namespace Request.Application.Features.Sales.Queries.GetById;

public sealed class GetSaleByIdQueryHandler(ISaleRepository repository) : IRequestHandler<GetSaleByIdQuery, SaleDto?>
{
    public async Task<SaleDto?> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return SaleMapper.MapToDto(entity);
    }
}
