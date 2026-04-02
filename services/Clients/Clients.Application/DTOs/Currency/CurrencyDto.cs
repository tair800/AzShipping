namespace Clients.Application.DTOs.Currency;

public record CurrencyDto
{
    public Guid Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
}
