using System.ComponentModel.DataAnnotations;

namespace AzShipping.Application.DTOs;

public class UpdateDeferredPaymentConditionDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Full text cannot exceed 1000 characters")]
    public string FullText { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}

