using MediatR;
using Settings.Application.DTOs.CarrierType;

namespace Settings.Application.Features.CarrierTypes.Queries.GetById;

public sealed record GetCarrierTypeByIdQuery(Guid Id) : IRequest<CarrierTypeDto?>;
