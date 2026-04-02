using System.ComponentModel.DataAnnotations;

namespace AzShipping.Application.DTOs;

public class CreateBankDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters")]
    public string Name { get; set; } = string.Empty;

    [StringLength(200, ErrorMessage = "Unofficial name cannot exceed 200 characters")]
    public string? UnofficialName { get; set; }

    [StringLength(200, ErrorMessage = "Branch cannot exceed 200 characters")]
    public string? Branch { get; set; }

    [StringLength(50, ErrorMessage = "Code cannot exceed 50 characters")]
    public string? Code { get; set; }

    [StringLength(50, ErrorMessage = "SWIFT cannot exceed 50 characters")]
    public string? Swift { get; set; }

    [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters")]
    public string? Country { get; set; }

    [StringLength(100, ErrorMessage = "City cannot exceed 100 characters")]
    public string? City { get; set; }

    [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
    public string? Address { get; set; }

    [StringLength(20, ErrorMessage = "Post code cannot exceed 20 characters")]
    public string? PostCode { get; set; }
}

