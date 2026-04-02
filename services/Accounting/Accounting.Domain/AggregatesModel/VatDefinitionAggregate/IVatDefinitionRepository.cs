namespace Accounting.Domain.AggregatesModel.VatDefinitionAggregate;

public interface IVatDefinitionRepository
{
    Task<VatDefinition?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<VatDefinition?> GetByIdTrackedAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<VatDefinition>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<VatDefinition> AddAsync(VatDefinition entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(VatDefinition entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
