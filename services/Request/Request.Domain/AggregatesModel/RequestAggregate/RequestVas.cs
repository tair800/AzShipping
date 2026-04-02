namespace Request.Domain.AggregatesModel.RequestAggregate;

/// <summary>VAS (Value Added Service) line for a request. Used for Sea Breakbulk, LCL, FCL.</summary>
public class RequestVas
{
    public Guid Id { get; set; }
    public Guid RequestId { get; set; }
    /// <summary>References VAS in General service.</summary>
    public Guid VasId { get; set; }
    public string? VasName { get; set; }
    public string? ExecutionPlace { get; set; }
    public decimal Quantity { get; set; }
    /// <summary>Auto from VAS when selected.</summary>
    public string? Uom { get; set; }
    public Guid? CurrencyId { get; set; }
    public string? CurrencyCode { get; set; }
    public decimal? Total { get; set; }
    public string? Notes { get; set; }
}
