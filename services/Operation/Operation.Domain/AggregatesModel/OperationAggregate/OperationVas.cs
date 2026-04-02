namespace Operation.Domain.AggregatesModel.OperationAggregate;

/// <summary>VAS line for sea breakbulk operations (mirrors request VAS).</summary>
public class OperationVas
{
    public Guid Id { get; set; }
    public Guid OperationId { get; set; }
    public LogisticsOperation Operation { get; set; } = null!;
    public Guid VasId { get; set; }
    public string? VasName { get; set; }
    public string? ExecutionPlace { get; set; }
    public decimal Quantity { get; set; }
    public string? Uom { get; set; }
    public Guid? CurrencyId { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal? Total { get; set; }
    public string? Notes { get; set; }
}
