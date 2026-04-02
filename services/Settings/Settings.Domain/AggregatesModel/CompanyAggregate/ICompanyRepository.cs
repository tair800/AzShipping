namespace Settings.Domain.AggregatesModel.CompanyAggregate;

public interface ICompanyRepository
{
    Task<Company?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Company>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Company> AddAsync(Company entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Company entity, CancellationToken cancellationToken = default);
    Task UpdateWithChildrenAsync(Company entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CompanySignature?> UpsertSignatureAsync(Guid companyId, string type, string? fileName, string? filePath, CancellationToken cancellationToken = default);
    Task DeleteSignatureAsync(Guid companyId, string type, CancellationToken cancellationToken = default);
}
