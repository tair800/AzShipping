using MediatR;

namespace Request.Application.Features.Sales.Commands.Delete;

public sealed record DeleteSaleCommand(Guid Id) : IRequest<bool>;
