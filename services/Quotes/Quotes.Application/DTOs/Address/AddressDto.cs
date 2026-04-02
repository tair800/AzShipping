namespace Quotes.Application.DTOs.Address;

public record AddressDto(
    Guid Id,
    Guid? AddressTypeId,
    string? AddressTypeName,
    string? Description,
    string? Name,
    string? Address1,
    string? Address2,
    Guid? CountryId,
    string? CountryName,
    string? Phone,
    Guid? StateId,
    string? StateName,
    string? Fax,
    Guid? CityId,
    string? CityName,
    string? Attn,
    string? ZipCode,
    string? Notes,
    string? FullAddressDisplay);
