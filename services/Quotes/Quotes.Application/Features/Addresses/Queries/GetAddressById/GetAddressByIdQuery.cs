using MediatR;
using Quotes.Application.DTOs.Address;

namespace Quotes.Application.Features.Addresses.Queries.GetAddressById;

public sealed record GetAddressByIdQuery(Guid Id) : IRequest<AddressDto?>;
