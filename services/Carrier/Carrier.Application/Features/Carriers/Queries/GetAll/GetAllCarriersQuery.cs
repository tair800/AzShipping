using Carrier.Application.DTOs.Carrier;
using MediatR;

namespace Carrier.Application.Features.Carriers.Queries.GetAll;

public sealed record GetAllCarriersQuery : IRequest<IReadOnlyList<CarrierDto>>;
