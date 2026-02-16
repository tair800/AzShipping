using System.ComponentModel.DataAnnotations;

namespace AzShipping.Application.DTOs;

public class UpdateClientSegmentDto
{
    [Required(ErrorMessage = "Segment name is required")]
    [StringLength(100, ErrorMessage = "Segment name cannot exceed 100 characters")]
    public string SegmentName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Segment priority is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Segment priority must be greater than 0")]
    public int SegmentPriority { get; set; }

    public bool IsActive { get; set; }
    public bool IsDefault { get; set; }

    [Required(ErrorMessage = "Primary color is required")]
    [StringLength(7, MinimumLength = 4, ErrorMessage = "Primary color must be a valid hex color")]
    public string PrimaryColor { get; set; } = string.Empty;

    [Required(ErrorMessage = "Secondary color is required")]
    [StringLength(7, MinimumLength = 4, ErrorMessage = "Secondary color must be a valid hex color")]
    public string SecondaryColor { get; set; } = string.Empty;
}

