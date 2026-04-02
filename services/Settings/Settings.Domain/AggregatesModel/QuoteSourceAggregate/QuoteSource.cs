namespace Settings.Domain.AggregatesModel.QuoteSourceAggregate;

/// <summary>Quote workflow stage labels (used as quote status / stage dropdown in Quotes UI).</summary>
public class QuoteSource
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
