namespace General.Domain.AggregatesModel.CurrencyAggregate;

public class Currency
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    /// <summary>ISO 4217 code (e.g. USD, EUR)</summary>
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Symbol { get; set; }
    public int? NumericCode { get; set; }
}
