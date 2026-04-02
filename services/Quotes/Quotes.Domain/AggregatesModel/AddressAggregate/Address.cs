namespace Quotes.Domain.AggregatesModel.AddressAggregate;

/// <summary>Full address for Pickup/Delivery on quotes. CountryId, StateId, CityId reference Settings (external).</summary>
public class Address
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Guid? AddressTypeId { get; set; }
    public string? AddressTypeName { get; set; }
    public string? Description { get; set; }
    public string? Name { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public Guid? CountryId { get; set; }
    public string? CountryName { get; set; }
    public string? Phone { get; set; }
    public Guid? StateId { get; set; }
    public string? StateName { get; set; }
    public string? Fax { get; set; }
    public Guid? CityId { get; set; }
    public string? CityName { get; set; }
    public string? Attn { get; set; }
    public string? ZipCode { get; set; }
    public string? Notes { get; set; }

    /// <summary>Formatted full address for display (e.g. Main Address block).</summary>
    public string? FullAddressDisplay { get; set; }
}
