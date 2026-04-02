namespace Carrier.Domain.AggregatesModel.ShippingLineAggregate;

public interface IShippingLineRepository
{
    Task<ShippingLine?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<ShippingLine>> GetAllAsync(bool? isActive, CancellationToken ct = default);
    Task<ShippingLine> AddAsync(ShippingLine entity, CancellationToken ct = default);
    Task UpdateAsync(ShippingLine entity, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
