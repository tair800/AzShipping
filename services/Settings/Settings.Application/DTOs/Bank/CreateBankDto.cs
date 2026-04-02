namespace Settings.Application.DTOs.Bank;

public record CreateBankDto
{
    public string Name { get; init; } = string.Empty;
    public string? UnofficialName { get; init; }
    public string? Branch { get; init; }
    public string? Code { get; init; }
    public string? Swift { get; init; }
    public Guid? CountryId { get; init; }
    public Guid? CityId { get; init; }
    public string? Address { get; init; }
    public string? PostCode { get; init; }
}
