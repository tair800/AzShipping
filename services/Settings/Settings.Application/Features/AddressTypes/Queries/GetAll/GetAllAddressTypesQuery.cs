using MediatR;
using Settings.Application.DTOs.AddressType;

namespace Settings.Application.Features.AddressTypes.Queries.GetAll;

public sealed record GetAllAddressTypesQuery : IRequest<IReadOnlyList<AddressTypeDto>>;
