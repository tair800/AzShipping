using Carrier.Application.DTOs.Carrier;
using MediatR;

namespace Carrier.Application.Features.Carriers.Commands.Create;

public sealed record CreateCarrierCommand(CreateCarrierDto Dto) : IRequest<CarrierDto>;
