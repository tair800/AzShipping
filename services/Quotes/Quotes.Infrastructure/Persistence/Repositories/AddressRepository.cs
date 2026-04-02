using Microsoft.EntityFrameworkCore;
using Quotes.Domain.AggregatesModel.AddressAggregate;

namespace Quotes.Infrastructure.Persistence.Repositories;

public class AddressRepository(QuotesDbContext context) : IAddressRepository
{
    public async Task<Address?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Addresses.FindAsync([id], cancellationToken);

    public async Task<Address> AddAsync(Address entity, CancellationToken cancellationToken = default)
    {
        context.Addresses.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Address entity, CancellationToken cancellationToken = default)
    {
        context.Addresses.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
