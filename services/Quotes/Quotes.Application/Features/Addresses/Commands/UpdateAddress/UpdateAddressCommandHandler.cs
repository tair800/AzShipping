using MediatR;
using Quotes.Application.DTOs.Address;
using Quotes.Application.Features.Addresses.Queries.GetAddressById;
using Quotes.Application.Services;
using Quotes.Domain.AggregatesModel.AddressAggregate;

namespace Quotes.Application.Features.Addresses.Commands.UpdateAddress;

public sealed class UpdateAddressCommandHandler(IAddressRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<UpdateAddressCommand, AddressDto?>
{
    public async Task<AddressDto?> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var a = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (a == null) return null;
        var d = request.Dto;
        a.AddressTypeId = d.AddressTypeId;
        a.AddressTypeName = d.AddressTypeName;
        a.Description = d.Description;
        a.Name = d.Name;
        a.Address1 = d.Address1;
        a.Address2 = d.Address2;
        a.CountryId = d.CountryId;
        a.CountryName = d.CountryName;
        a.Phone = d.Phone;
        a.StateId = d.StateId;
        a.StateName = d.StateName;
        a.Fax = d.Fax;
        a.CityId = d.CityId;
        a.CityName = d.CityName;
        a.Attn = d.Attn;
        a.ZipCode = d.ZipCode;
        a.Notes = d.Notes;
        a.FullAddressDisplay = BuildFullAddressDisplay(d);
        a.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(a, cancellationToken);
        var result = await new GetAddressByIdQueryHandler(repository).Handle(new GetAddressByIdQuery(request.Id), cancellationToken);
        await actionLogClient.LogAsync("Address updated", $"address: {a.Name} • id: {a.Id}", null, null, cancellationToken);
        return result;
    }

    private static string? BuildFullAddressDisplay(CreateOrUpdateAddressDto d)
    {
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(d.Address1)) parts.Add(d.Address1);
        if (!string.IsNullOrWhiteSpace(d.Address2)) parts.Add(d.Address2);
        if (!string.IsNullOrWhiteSpace(d.CityName)) parts.Add(d.CityName);
        if (!string.IsNullOrWhiteSpace(d.StateName)) parts.Add(d.StateName);
        if (!string.IsNullOrWhiteSpace(d.ZipCode)) parts.Add(d.ZipCode);
        if (!string.IsNullOrWhiteSpace(d.CountryName)) parts.Add(d.CountryName);
        return parts.Count > 0 ? string.Join(", ", parts) : null;
    }
}
