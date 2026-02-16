using System.ComponentModel.DataAnnotations;

namespace AzShipping.Application.DTOs;

public class UpdateRequestPurposeDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}

