using MediatR;

namespace Request.Application.Features.SaleStatuses.Commands.Delete;

public sealed record DeleteSaleStatusCommand(Guid Id) : IRequest<bool>;
