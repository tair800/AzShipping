using Clients.Domain.AggregatesModel.CurrencyAggregate;
using Microsoft.EntityFrameworkCore;

namespace Clients.Infrastructure.Persistence.Repositories;

public class CurrencyRepository(ClientsDbContext context) : ICurrencyRepository
{
    public async Task<IReadOnlyList<Currency>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Currencies.OrderBy(c => c.Code).ToListAsync(cancellationToken);
}
