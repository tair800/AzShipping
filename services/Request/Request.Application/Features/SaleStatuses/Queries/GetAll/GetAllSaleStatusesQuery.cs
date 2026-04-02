using MediatR;
using Request.Application.DTOs.SaleStatus;

namespace Request.Application.Features.SaleStatuses.Queries.GetAll;

public sealed record GetAllSaleStatusesQuery : IRequest<IReadOnlyList<SaleStatusDto>>;
