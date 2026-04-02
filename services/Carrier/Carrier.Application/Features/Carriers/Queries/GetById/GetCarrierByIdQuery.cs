using Carrier.Application.DTOs.Carrier;
using MediatR;

namespace Carrier.Application.Features.Carriers.Queries.GetById;

public sealed record GetCarrierByIdQuery(Guid Id) : IRequest<CarrierDto?>;
