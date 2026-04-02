namespace Carrier.Domain.AggregatesModel.DriverAggregate;

/// <summary>
/// Stores driver's driving licence category IDs (from Settings service; no FK to Settings DB).
/// </summary>
public class DriverDrivingLicenceCategory
{
    public Guid DriverId { get; set; }
    public Guid DrivingLicenceCategoryId { get; set; }
    public Driver Driver { get; set; } = null!;
}
