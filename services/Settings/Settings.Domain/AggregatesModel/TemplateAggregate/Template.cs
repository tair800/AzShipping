namespace Settings.Domain.AggregatesModel.TemplateAggregate;

public class Template
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<TemplateTranslation> Translations { get; set; } = new List<TemplateTranslation>();
}

public class TemplateTranslation
{
    public Guid Id { get; set; }
    public Guid TemplateId { get; set; }
    public Template Template { get; set; } = null!;
    public string LanguageCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
