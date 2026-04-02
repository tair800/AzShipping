using System.ComponentModel.DataAnnotations;

namespace AzShipping.Application.DTOs;

public class CreateRequestSourceDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}

