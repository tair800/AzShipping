using MediatR;
using Request.Application.DTOs.SaleStatus;

namespace Request.Application.Features.SaleStatuses.Queries.GetById;

public sealed record GetSaleStatusByIdQuery(Guid Id) : IRequest<SaleStatusDto?>;
