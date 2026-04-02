using System.ComponentModel.DataAnnotations;

namespace AzShipping.Application.DTOs;

public class CreateTransportTypeDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
    public string Name { get; set; } = string.Empty;

    public bool IsAir { get; set; } = false;
    public bool IsSea { get; set; } = false;
    public bool IsRoad { get; set; } = false;
    public bool IsRail { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public Dictionary<string, string> Translations { get; set; } = new(); // LanguageCode -> Name
}

