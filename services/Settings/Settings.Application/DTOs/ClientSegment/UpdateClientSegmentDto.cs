namespace Settings.Application.DTOs.ClientSegment;

public record UpdateClientSegmentDto(string SegmentName, int SegmentPriority, bool IsActive, bool IsDefault, string PrimaryColor, string SecondaryColor);
