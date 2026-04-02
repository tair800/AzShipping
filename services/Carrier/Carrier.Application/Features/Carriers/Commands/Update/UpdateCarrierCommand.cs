using Carrier.Application.DTOs.Carrier;
using MediatR;

namespace Carrier.Application.Features.Carriers.Commands.Update;

public sealed record UpdateCarrierCommand(Guid Id, UpdateCarrierDto Dto) : IRequest<CarrierDto?>;
