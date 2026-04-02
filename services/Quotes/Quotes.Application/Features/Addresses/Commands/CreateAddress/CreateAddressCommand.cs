using MediatR;
using Quotes.Application.DTOs.Address;

namespace Quotes.Application.Features.Addresses.Commands.CreateAddress;

public sealed record CreateAddressCommand(CreateOrUpdateAddressDto Dto) : IRequest<AddressDto>;
