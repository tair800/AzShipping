using MediatR;
using Settings.Application.DTOs.SalesFunnelStatus;

namespace Settings.Application.Features.SalesFunnelStatuses.Queries.GetAll;

public sealed record GetAllSalesFunnelStatusesQuery : IRequest<IReadOnlyList<SalesFunnelStatusDto>>;
