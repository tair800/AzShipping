namespace Settings.Domain.AggregatesModel.DrivingLicenceCategoryAggregate;

public class DrivingLicenceCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Code { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
