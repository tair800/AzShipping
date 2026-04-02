using Carrier.Application.DTOs.CarrierDirection;
using MediatR;

namespace Carrier.Application.Features.CarrierDirections.Queries.GetById;

public record GetCarrierDirectionByIdQuery(Guid Id) : IRequest<CarrierDirectionDto?>;
