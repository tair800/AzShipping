namespace General.Domain.AggregatesModel.CurrencyAggregate;

public interface ICurrencyRepository
{
    Task<IReadOnlyList<Currency>> GetAllAsync(CancellationToken cancellationToken = default);
}
