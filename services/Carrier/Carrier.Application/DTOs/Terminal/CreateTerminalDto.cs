namespace Carrier.Application.DTOs.Terminal;

public class CreateTerminalDto
{
    public string Name { get; set; } = string.Empty;
    public Guid? CountryId { get; set; }
    public Guid? CityId { get; set; }
    public string? Address { get; set; }
    public string? PostCode { get; set; }
    public string? RailwayStation { get; set; }
    public List<Guid> TransportTypeIds { get; set; } = [];
    public string? Notes { get; set; }
    public bool IsDeactive { get; set; }
    public DateTime? DateOfCreation { get; set; }
}
