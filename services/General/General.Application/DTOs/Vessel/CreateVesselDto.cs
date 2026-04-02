namespace General.Application.DTOs.Vessel;

public class CreateVesselDto
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? ImoCode { get; set; }
    public string? LocalName { get; set; }
    public Guid? CountryId { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;
}
