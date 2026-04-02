namespace Accounting.Domain.AggregatesModel.InvoiceLookupAggregate;

public class InvoiceLookupOption
{
    public Guid Id { get; set; }
    public InvoiceLookupCategory Category { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAtUtc { get; set; }
}
