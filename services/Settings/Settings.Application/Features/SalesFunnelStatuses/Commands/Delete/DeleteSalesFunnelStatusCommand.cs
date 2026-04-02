using MediatR;

namespace Settings.Application.Features.SalesFunnelStatuses.Commands.Delete;

public sealed record DeleteSalesFunnelStatusCommand(Guid Id) : IRequest<bool>;
