namespace Settings.Domain.AggregatesModel.TemplateAggregate;

public interface ITemplateRepository
{
    Task<Template?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Template>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Template> AddAsync(Template entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Template entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
