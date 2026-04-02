using Microsoft.EntityFrameworkCore;
using Settings.Domain.AggregatesModel.TemplateAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class TemplateRepository(SettingsDbContext context) : ITemplateRepository
{
    public async Task<Template?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await context.Templates.Include(t => t.Translations).FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task<IReadOnlyList<Template>> GetAllAsync(CancellationToken cancellationToken = default)
        => await context.Templates.Include(t => t.Translations).OrderBy(t => t.Name).ToListAsync(cancellationToken);

    public async Task<Template> AddAsync(Template entity, CancellationToken cancellationToken = default)
    {
        context.Templates.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(Template entity, CancellationToken cancellationToken = default)
    {
        context.Templates.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var e = await context.Templates.Include(t => t.Translations).FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        if (e != null)
        {
            context.Templates.Remove(e);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
