namespace Operation.Domain.AggregatesModel.OperationAggregate;

/// <summary>Sea FCL / breakbulk — quantity + package type rows.</summary>
public class OperationPackageLine
{
    public Guid Id { get; set; }
    public Guid OperationId { get; set; }
    public LogisticsOperation Operation { get; set; } = null!;
    public int Quantity { get; set; }
    public string? PackageType { get; set; }
    public int SortOrder { get; set; }
}
