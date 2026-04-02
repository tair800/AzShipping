namespace Settings.Domain.AggregatesModel.LoadingMethodAggregate;

public class LoadingMethod
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<LoadingMethodTranslation> Translations { get; set; } = new List<LoadingMethodTranslation>();
}

public class LoadingMethodTranslation
{
    public Guid Id { get; set; }
    public Guid LoadingMethodId { get; set; }
    public LoadingMethod LoadingMethod { get; set; } = null!;
    public string LanguageCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
