namespace Request.Domain.AggregatesModel.RequestAggregate;

public class RequestDimension
{
    public Guid Id { get; set; }
    public Guid RequestId { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public int Quantity { get; set; }
    public decimal? WeightKg { get; set; }
    public decimal? VolumeCbm { get; set; }
    /// <summary>Optional package/container type e.g. for FCL: "20' DV", "40' HC".</summary>
    public string? PackageType { get; set; }
}
