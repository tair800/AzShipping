using System.ComponentModel.DataAnnotations;

namespace AzShipping.Application.DTOs;

public class UpdateDrivingLicenceCategoryDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(10, ErrorMessage = "Name cannot exceed 10 characters")]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}

