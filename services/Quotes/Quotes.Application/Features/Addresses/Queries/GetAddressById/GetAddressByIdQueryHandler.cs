using MediatR;
using Quotes.Application.DTOs.Address;
using Quotes.Domain.AggregatesModel.AddressAggregate;

namespace Quotes.Application.Features.Addresses.Queries.GetAddressById;

public sealed class GetAddressByIdQueryHandler(IAddressRepository repository)
    : IRequestHandler<GetAddressByIdQuery, AddressDto?>
{
    public async Task<AddressDto?> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
    {
        var a = await repository.GetByIdAsync(request.Id, cancellationToken);
        return a == null ? null : Map(a);
    }

    private static AddressDto Map(Address a) => new(
        a.Id, a.AddressTypeId, a.AddressTypeName, a.Description, a.Name, a.Address1, a.Address2,
        a.CountryId, a.CountryName, a.Phone, a.StateId, a.StateName, a.Fax, a.CityId, a.CityName,
        a.Attn, a.ZipCode, a.Notes, a.FullAddressDisplay);
}
