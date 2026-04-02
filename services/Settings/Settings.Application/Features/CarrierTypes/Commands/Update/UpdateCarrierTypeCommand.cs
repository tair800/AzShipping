using MediatR;
using Settings.Application.DTOs.CarrierType;

namespace Settings.Application.Features.CarrierTypes.Commands.Update;

public sealed record UpdateCarrierTypeCommand(Guid Id, UpdateCarrierTypeDto Dto) : IRequest<CarrierTypeDto?>;
