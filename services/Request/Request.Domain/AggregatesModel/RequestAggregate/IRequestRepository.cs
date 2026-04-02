namespace Request.Domain.AggregatesModel.RequestAggregate;

public interface IRequestRepository
{
    Task<RequestEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RequestEntity>> GetAllAsync(string? typeCode = null, string? mode = null, string? direction = null, string? subType = null, CancellationToken cancellationToken = default);
    Task<RequestEntity> AddAsync(RequestEntity entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(RequestEntity entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
