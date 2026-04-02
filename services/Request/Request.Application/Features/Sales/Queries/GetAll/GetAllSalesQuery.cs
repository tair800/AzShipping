using MediatR;
using Request.Application.DTOs.Sale;

namespace Request.Application.Features.Sales.Queries.GetAll;

public sealed record GetAllSalesQuery(string? ListStatusFilter = null) : IRequest<IReadOnlyList<SaleDto>>;
