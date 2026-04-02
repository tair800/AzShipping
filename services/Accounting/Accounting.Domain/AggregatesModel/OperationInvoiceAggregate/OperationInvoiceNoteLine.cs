namespace Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;

/// <summary>Figma Notes tab row.</summary>
public class OperationInvoiceNoteLine
{
    public Guid Id { get; set; }
    public Guid OperationInvoiceId { get; set; }
    public OperationInvoice OperationInvoice { get; set; } = null!;

    public int SortOrder { get; set; }
    public string? CreatorDisplayName { get; set; }
    public string? NoteTypeCode { get; set; }
    public string NoteText { get; set; } = string.Empty;
}
