using Settings.Domain.AggregatesModel.CityAggregate;
using Settings.Domain.AggregatesModel.CountryAggregate;

namespace Settings.Domain.AggregatesModel.BankAggregate;

public class Bank
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? UnofficialName { get; set; }
    public string? Branch { get; set; }
    public string? Code { get; set; }
    public string? Swift { get; set; }
    public Guid? CountryId { get; set; }
    public Country? Country { get; set; }
    public Guid? CityId { get; set; }
    public City? City { get; set; }
    public string? Address { get; set; }
    public string? PostCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
