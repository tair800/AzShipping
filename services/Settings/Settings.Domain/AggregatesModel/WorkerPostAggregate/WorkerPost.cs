namespace Settings.Domain.AggregatesModel.WorkerPostAggregate;

public class WorkerPost
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<WorkerPostTranslation> Translations { get; set; } = new List<WorkerPostTranslation>();
}

public class WorkerPostTranslation
{
    public Guid Id { get; set; }
    public Guid WorkerPostId { get; set; }
    public WorkerPost WorkerPost { get; set; } = null!;
    public string LanguageCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
