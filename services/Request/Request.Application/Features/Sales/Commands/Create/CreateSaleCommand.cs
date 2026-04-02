using MediatR;
using Request.Application.DTOs.Sale;

namespace Request.Application.Features.Sales.Commands.Create;

public sealed record CreateSaleCommand(CreateSaleDto Dto) : IRequest<SaleDto>;
