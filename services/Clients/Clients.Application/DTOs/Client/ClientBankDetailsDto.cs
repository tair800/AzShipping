namespace Clients.Application.DTOs.Client;

/// <summary>Snapshot from Settings <c>Banks</c> for display / auto-filled fields (read-only on client responses).</summary>
public record ClientBankDetailsDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? UnofficialName { get; init; }
    public string? Branch { get; init; }
    public string? Code { get; init; }
    public string? Swift { get; init; }
    public Guid? CountryId { get; init; }
    public string? CountryName { get; init; }
    public Guid? CityId { get; init; }
    public string? CityName { get; init; }
    public string? Address { get; init; }
    public string? PostCode { get; init; }
}
