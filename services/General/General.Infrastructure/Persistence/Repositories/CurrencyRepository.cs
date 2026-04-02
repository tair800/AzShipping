using General.Domain.AggregatesModel.CurrencyAggregate;
using Microsoft.EntityFrameworkCore;

namespace General.Infrastructure.Persistence.Repositories;

public class CurrencyRepository(GeneralDbContext context) : ICurrencyRepository
{
    public async Task<IReadOnlyList<Currency>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Currencies.OrderBy(x => x.Code).ToListAsync(cancellationToken);
}
