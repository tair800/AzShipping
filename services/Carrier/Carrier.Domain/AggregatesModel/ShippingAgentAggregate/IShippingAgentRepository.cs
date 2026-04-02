namespace Carrier.Domain.AggregatesModel.ShippingAgentAggregate;

public interface IShippingAgentRepository
{
    Task<ShippingAgent?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<ShippingAgent>> GetAllAsync(bool? isActive, CancellationToken ct = default);
    Task<ShippingAgent> AddAsync(ShippingAgent entity, CancellationToken ct = default);
    Task UpdateAsync(ShippingAgent entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
