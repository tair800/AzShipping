using Microsoft.EntityFrameworkCore;
using Settings.Application.DTOs.GeneralSetting;
using Settings.Domain.AggregatesModel.GeneralSettingAggregate;

namespace Settings.Infrastructure.Persistence.Repositories;

public class GeneralSettingRepository(SettingsDbContext context) : IGeneralSettingRepository
{
    public async Task<GeneralSetting?> GetAsync(CancellationToken cancellationToken = default)
        => await context.GeneralSettings.FirstOrDefaultAsync(cancellationToken);

    public async Task<GeneralSetting> GetOrCreateAsync(CancellationToken cancellationToken = default)
    {
        var existing = await GetAsync(cancellationToken);
        if (existing != null) return existing;

        var entity = new GeneralSetting
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            DateFormat = "dd/mm/yyyy",
            PriceDisplayType = PriceDisplayTypeOptions.Freight,
            Timezone = "(UTC+04:00) Azerbaijan Time"
        };
        context.GeneralSettings.Add(entity);
        await context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task SaveAsync(GeneralSetting entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        context.GeneralSettings.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
