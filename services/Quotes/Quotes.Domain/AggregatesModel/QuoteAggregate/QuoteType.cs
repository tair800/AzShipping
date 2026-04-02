namespace Quotes.Domain.AggregatesModel.QuoteAggregate;

/// <summary>
/// Defines a quote type (Export Air, Import Sea, etc.). Mirrors RequestType structure.
/// </summary>
public class QuoteType
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
    public string Mode { get; set; } = string.Empty;
    public string? SubType { get; set; }
    public string QuoteNumberPrefix { get; set; } = string.Empty;
    public string CarrierApiPath { get; set; } = string.Empty;
    public string CarrierLabel { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
