namespace Clients.Domain.AggregatesModel.DocumentAggregate;

public class Document
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid? CompanyId { get; set; }
    public string DocumentType { get; set; } = "upload"; // "template" | "upload"
    public Guid? TemplateId { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
    public DateTime DocumentDate { get; set; }
    public string DocumentName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidUntil { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public Guid? NotifyUserId { get; set; }
    public bool ProhibitOnExpiry { get; set; }
    public bool IsDefault { get; set; }
    public string? Comments { get; set; }
    public bool AvailableForClient { get; set; }
    public bool IsSent { get; set; }
    public string? FilePath { get; set; }
}
