namespace Request.Domain.AggregatesModel.PriceProposalAggregate;

public interface IPriceProposalRepository
{
    Task<PriceProposal?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PriceProposal>> GetByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default);
    Task<PriceProposal> AddAsync(PriceProposal entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(PriceProposal entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
