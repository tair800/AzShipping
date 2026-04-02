using MediatR;
using Request.Application.DTOs.SaleStatus;

namespace Request.Application.Features.SaleStatuses.Commands.Update;

public sealed record UpdateSaleStatusCommand(Guid Id, UpdateSaleStatusDto Dto) : IRequest<SaleStatusDto?>;
