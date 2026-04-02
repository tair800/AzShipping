namespace Quotes.Domain.AggregatesModel.AddressAggregate;

public interface IAddressRepository
{
    Task<Address?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Address> AddAsync(Address entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Address entity, CancellationToken cancellationToken = default);
}
