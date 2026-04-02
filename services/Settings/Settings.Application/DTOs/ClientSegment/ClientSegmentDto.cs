namespace Settings.Application.DTOs.ClientSegment;

public record ClientSegmentDto
{
    public Guid Id { get; init; }
    public string SegmentName { get; init; } = string.Empty;
    public int SegmentPriority { get; init; }
    public bool IsActive { get; init; }
    public bool IsDefault { get; init; }
    public string PrimaryColor { get; init; } = string.Empty;
    public string SecondaryColor { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
