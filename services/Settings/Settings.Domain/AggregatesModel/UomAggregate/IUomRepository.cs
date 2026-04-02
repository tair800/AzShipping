namespace Settings.Domain.AggregatesModel.UomAggregate;

public interface IUomRepository
{
    Task<Uom?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Uom>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Uom> AddAsync(Uom entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Uom entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
