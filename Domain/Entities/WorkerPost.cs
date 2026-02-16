namespace AzShipping.Domain.Entities;

public class WorkerPost
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<WorkerPostTranslation> Translations { get; set; } = new List<WorkerPostTranslation>();
}

