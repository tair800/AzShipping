using MediatR;
using Request.Application.DTOs.SaleStatus;

namespace Request.Application.Features.SaleStatuses.Commands.Create;

public sealed record CreateSaleStatusCommand(CreateSaleStatusDto Dto) : IRequest<SaleStatusDto>;
