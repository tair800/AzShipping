namespace Settings.Domain.AggregatesModel.GeneralSettingAggregate;

public interface IGeneralSettingRepository
{
    Task<GeneralSetting?> GetAsync(CancellationToken cancellationToken = default);
    Task<GeneralSetting> GetOrCreateAsync(CancellationToken cancellationToken = default);
    Task SaveAsync(GeneralSetting entity, CancellationToken cancellationToken = default);
}
