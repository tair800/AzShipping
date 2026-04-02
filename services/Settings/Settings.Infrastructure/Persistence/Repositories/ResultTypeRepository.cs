using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.ResultTypeAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class ResultTypeRepository(SettingsDbContext context) : IResultTypeRepository
{
    public async Task<ResultType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.ResultTypes.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyList<ResultType>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.ResultTypes.Where(x => x.IsActive).OrderBy(x => x.Code).ToListAsync(cancellationToken);
}
