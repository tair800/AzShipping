using System.ComponentModel.DataAnnotations;

namespace AzShipping.Application.DTOs;

public class CreateSalesFunnelStatusDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Status Position is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Status Position must be a positive number")]
    public int StatusPosition { get; set; }

    public Guid? ResponsibleManagerId { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Number of days must be 0 or greater")]
    public int NumberOfDays { get; set; }

    public bool SendToEmail { get; set; } = false;
    public bool SendNotification { get; set; } = false;
    public bool IsActive { get; set; } = true;

    public Dictionary<string, string> Translations { get; set; } = new(); // LanguageCode -> Name
}

