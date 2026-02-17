namespace AzShipping.Application.DTOs;

public class BankDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? UnofficialName { get; set; }
    public string? Branch { get; set; }
    public string? Code { get; set; }
    public string? Swift { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public string? PostCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

