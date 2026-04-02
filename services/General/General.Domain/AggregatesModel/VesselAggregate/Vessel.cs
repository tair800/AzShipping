namespace General.Domain.AggregatesModel.VesselAggregate;

public class Vessel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? ImoCode { get; set; }
    public string? LocalName { get; set; }
    public Guid? CountryId { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; }
}
