namespace Settings.Domain.AggregatesModel.AddressTypeAggregate;

public interface IAddressTypeRepository
{
    Task<IReadOnlyList<AddressType>> GetAllAsync(CancellationToken cancellationToken = default);
}
