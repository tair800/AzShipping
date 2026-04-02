namespace Settings.Domain.AggregatesModel.WayOfNegotiationAggregate;

public class WayOfNegotiation
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<WayOfNegotiationTranslation> Translations { get; set; } = new List<WayOfNegotiationTranslation>();
}

public class WayOfNegotiationTranslation
{
    public Guid Id { get; set; }
    public Guid WayOfNegotiationId { get; set; }
    public WayOfNegotiation WayOfNegotiation { get; set; } = null!;
    public string LanguageCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
