namespace AzShipping.Domain.Entities;

public class ClientSegment
{
    public Guid Id { get; set; }
    public string SegmentName { get; set; } = string.Empty;
    public int SegmentPriority { get; set; }
    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }
    public string PrimaryColor { get; set; } = string.Empty;
    public string SecondaryColor { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

