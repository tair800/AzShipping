namespace Accounting.Domain.AggregatesModel.OperationInvoiceAggregate;

public interface IOperationInvoiceRepository
{
    Task<OperationInvoice?> GetByIdWithLinesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<OperationInvoice?> GetByIdWithLinesTrackedAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<OperationInvoice>> GetByOperationIdAsync(Guid operationId, CancellationToken cancellationToken = default);
    /// <summary>All invoices, header fields only (no lines) for global list views.</summary>
    Task<IReadOnlyList<OperationInvoice>> GetAllForListAsync(CancellationToken cancellationToken = default);
    Task<OperationInvoice> AddAsync(OperationInvoice entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(OperationInvoice entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
