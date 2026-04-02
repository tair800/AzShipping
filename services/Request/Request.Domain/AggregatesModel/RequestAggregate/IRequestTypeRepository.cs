namespace Request.Domain.AggregatesModel.RequestAggregate;

public interface IRequestTypeRepository
{
    Task<IReadOnlyList<RequestType>> GetAllActiveAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RequestType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RequestType?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<RequestType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<RequestType> AddAsync(RequestType entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(RequestType entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
