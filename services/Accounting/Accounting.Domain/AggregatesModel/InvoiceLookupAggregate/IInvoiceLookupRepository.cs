namespace Accounting.Domain.AggregatesModel.InvoiceLookupAggregate;

public interface IInvoiceLookupRepository
{
    Task<IReadOnlyList<InvoiceLookupOption>> GetActiveAsync(InvoiceLookupCategory? category,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsCodeAsync(InvoiceLookupCategory category, string code,
        CancellationToken cancellationToken = default);

    Task AddAsync(InvoiceLookupOption entity, CancellationToken cancellationToken = default);
}
