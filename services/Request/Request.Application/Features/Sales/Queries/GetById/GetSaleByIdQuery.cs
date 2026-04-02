using MediatR;
using Request.Application.DTOs.Sale;

namespace Request.Application.Features.Sales.Queries.GetById;

public sealed record GetSaleByIdQuery(Guid Id) : IRequest<SaleDto?>;
