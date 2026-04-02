using System.ComponentModel.DataAnnotations;

namespace AzShipping.Application.DTOs;

public class UpdateTransportTypeDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
    public string Name { get; set; } = string.Empty;

    public bool IsAir { get; set; }
    public bool IsSea { get; set; }
    public bool IsRoad { get; set; }
    public bool IsRail { get; set; }

    public bool IsActive { get; set; }

    public Dictionary<string, string> Translations { get; set; } = new(); // LanguageCode -> Name
}

