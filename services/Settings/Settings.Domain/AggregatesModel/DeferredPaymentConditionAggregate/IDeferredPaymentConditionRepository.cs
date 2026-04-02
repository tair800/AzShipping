namespace Settings.Domain.AggregatesModel.DeferredPaymentConditionAggregate;

public interface IDeferredPaymentConditionRepository
{
    Task<DeferredPaymentCondition?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<DeferredPaymentCondition>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DeferredPaymentCondition> AddAsync(DeferredPaymentCondition entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(DeferredPaymentCondition entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
