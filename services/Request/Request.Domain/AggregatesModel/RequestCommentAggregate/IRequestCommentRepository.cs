namespace Request.Domain.AggregatesModel.RequestCommentAggregate;

public interface IRequestCommentRepository
{
    Task<RequestComment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RequestComment>> GetByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default);
    Task<RequestComment> AddAsync(RequestComment entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(RequestComment entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
