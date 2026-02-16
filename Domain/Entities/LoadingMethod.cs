namespace AzShipping.Domain.Entities;

public class LoadingMethod
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<LoadingMethodTranslation> Translations { get; set; } = new List<LoadingMethodTranslation>();
}

