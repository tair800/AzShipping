using MediatR;
using Quotes.Application.DTOs.Address;

namespace Quotes.Application.Features.Addresses.Commands.UpdateAddress;

public sealed record UpdateAddressCommand(Guid Id, CreateOrUpdateAddressDto Dto) : IRequest<AddressDto?>;
