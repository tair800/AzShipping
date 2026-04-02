namespace Operation.Domain.AggregatesModel.OperationAggregate;

public class OperationDimension
{
    public Guid Id { get; set; }
    public Guid OperationId { get; set; }
    public LogisticsOperation Operation { get; set; } = null!;
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public int Quantity { get; set; }
    public decimal? WeightKg { get; set; }
    public decimal? VolumeCbm { get; set; }
    public string? PackageType { get; set; }
}
