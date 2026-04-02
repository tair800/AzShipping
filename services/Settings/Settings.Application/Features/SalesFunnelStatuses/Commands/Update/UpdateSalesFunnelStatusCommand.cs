using MediatR;
using Settings.Application.DTOs.SalesFunnelStatus;

namespace Settings.Application.Features.SalesFunnelStatuses.Commands.Update;

public sealed record UpdateSalesFunnelStatusCommand(Guid Id, UpdateSalesFunnelStatusDto Dto) : IRequest<SalesFunnelStatusDto?>;
