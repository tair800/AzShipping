using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.AddressTypeAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class AddressTypeRepository(SettingsDbContext context) : IAddressTypeRepository
{
    public async Task<IReadOnlyList<AddressType>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.AddressTypes.OrderBy(x => x.Code).ToListAsync(cancellationToken);
}
