namespace AzShipping.Domain.Entities;

public class WorkerPostTranslation
{
    public Guid Id { get; set; }
    public Guid WorkerPostId { get; set; }
    public WorkerPost WorkerPost { get; set; } = null!;
    public string LanguageCode { get; set; } = string.Empty; // EN, RU, PL, LT, UK, KA, AZ, ZH
    public string Name { get; set; } = string.Empty;
}

