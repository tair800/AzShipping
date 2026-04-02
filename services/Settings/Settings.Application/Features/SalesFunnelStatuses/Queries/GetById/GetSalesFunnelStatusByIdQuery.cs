using MediatR;
using Settings.Application.DTOs.SalesFunnelStatus;

namespace Settings.Application.Features.SalesFunnelStatuses.Queries.GetById;

public sealed record GetSalesFunnelStatusByIdQuery(Guid Id) : IRequest<SalesFunnelStatusDto?>;
