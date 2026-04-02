namespace General.Application.DTOs.Incoterm;

public class CreateIncotermDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? LocalName { get; set; }
    public string? Freight { get; set; }
    public string? OtherCharges { get; set; }
    public bool IsActive { get; set; } = true;
}
