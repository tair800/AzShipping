namespace AzShipping.Application.DTOs;

public class CarrierTypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string Colour { get; set; } = "#000000";
    public bool IsDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

