using MediatR;
using Settings.Application.DTOs.CarrierType;

namespace Settings.Application.Features.CarrierTypes.Commands.Create;

public sealed record CreateCarrierTypeCommand(CreateCarrierTypeDto Dto) : IRequest<CarrierTypeDto>;
