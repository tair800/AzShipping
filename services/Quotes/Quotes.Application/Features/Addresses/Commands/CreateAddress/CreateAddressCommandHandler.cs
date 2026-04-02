using MediatR;
using Quotes.Application.DTOs.Address;
using Quotes.Application.Features.Addresses.Queries.GetAddressById;
using Quotes.Application.Services;
using Quotes.Domain.AggregatesModel.AddressAggregate;

namespace Quotes.Application.Features.Addresses.Commands.CreateAddress;

public sealed class CreateAddressCommandHandler(IAddressRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<CreateAddressCommand, AddressDto>
{
    public async Task<AddressDto> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var d = request.Dto;
        var entity = new Address
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            AddressTypeId = d.AddressTypeId,
            AddressTypeName = d.AddressTypeName,
            Description = d.Description,
            Name = d.Name,
            Address1 = d.Address1,
            Address2 = d.Address2,
            CountryId = d.CountryId,
            CountryName = d.CountryName,
            Phone = d.Phone,
            StateId = d.StateId,
            StateName = d.StateName,
            Fax = d.Fax,
            CityId = d.CityId,
            CityName = d.CityName,
            Attn = d.Attn,
            ZipCode = d.ZipCode,
            Notes = d.Notes,
            FullAddressDisplay = BuildFullAddressDisplay(d)
        };
        await repository.AddAsync(entity, cancellationToken);
        var result = (await new GetAddressByIdQueryHandler(repository).Handle(new GetAddressByIdQuery(entity.Id), cancellationToken))!;
        await actionLogClient.LogAsync("Address created", $"address: {entity.Name} • id: {entity.Id}", null, null, cancellationToken);
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
