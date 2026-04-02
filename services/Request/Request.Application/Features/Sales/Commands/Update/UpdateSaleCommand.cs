using MediatR;
using Request.Application.DTOs.Sale;

namespace Request.Application.Features.Sales.Commands.Update;

public sealed record UpdateSaleCommand(Guid Id, UpdateSaleDto Dto) : IRequest<SaleDto?>;
