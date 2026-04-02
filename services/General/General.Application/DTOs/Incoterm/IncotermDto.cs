namespace General.Application.DTOs.Incoterm;

public class IncotermDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? LocalName { get; set; }
    public string? Freight { get; set; }
    public string? OtherCharges { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
