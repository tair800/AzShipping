namespace Carrier.Domain.AggregatesModel.TerminalAggregate;

public class Terminal
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;       // Place/Company name
    public Guid? CountryId { get; set; }                   // From settings Countries
    public Guid? CityId { get; set; }                      // From settings Cities
    public string? Address { get; set; }
    public string? PostCode { get; set; }
    public string? RailwayStation { get; set; }            // From settings or manual
    public string? TransportTypeIds { get; set; }          // Comma-separated GUIDs (Air, Sea, Road, Rail)
    public string? Notes { get; set; }
    public bool IsDeactive { get; set; }
    public DateTime? DateOfCreation { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
