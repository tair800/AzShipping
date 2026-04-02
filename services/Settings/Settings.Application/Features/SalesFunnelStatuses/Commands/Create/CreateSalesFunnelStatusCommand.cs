using MediatR;
using Settings.Application.DTOs.SalesFunnelStatus;

namespace Settings.Application.Features.SalesFunnelStatuses.Commands.Create;

public sealed record CreateSalesFunnelStatusCommand(CreateSalesFunnelStatusDto Dto) : IRequest<SalesFunnelStatusDto>;
