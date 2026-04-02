using System.ComponentModel.DataAnnotations;

namespace AzShipping.Application.DTOs;

public class UpdateCarrierTypeDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    [RegularExpression(@"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage = "Colour must be a valid hex color code")]
    public string Colour { get; set; } = "#000000";

    public bool IsDefault { get; set; }
}

