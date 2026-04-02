namespace Settings.Domain.AggregatesModel.ResultTypeAggregate;

public interface IResultTypeRepository
{
    Task<ResultType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ResultType>> GetAllAsync(CancellationToken cancellationToken = default);
}
