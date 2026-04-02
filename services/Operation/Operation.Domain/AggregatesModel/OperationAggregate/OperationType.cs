namespace Operation.Domain.AggregatesModel.OperationAggregate;

/// <summary>Catalog row: direction + mode (Air / Sea / Road) + sub type. Drives carrier API path, label, and operation number prefix.</summary>
public class OperationType
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
    public string Mode { get; set; } = string.Empty;
    public string? SubType { get; set; }
    public string OperationNumberPrefix { get; set; } = string.Empty;
    public string CarrierApiPath { get; set; } = string.Empty;
    public string CarrierLabel { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
