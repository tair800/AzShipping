using MediatR;
using Settings.Application.DTOs.CarrierType;

namespace Settings.Application.Features.CarrierTypes.Queries.GetAll;

public sealed record GetAllCarrierTypesQuery : IRequest<IReadOnlyList<CarrierTypeDto>>;
