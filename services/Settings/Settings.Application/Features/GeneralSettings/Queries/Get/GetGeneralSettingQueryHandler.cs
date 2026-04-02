using MediatR;
using Settings.Application.DTOs.GeneralSetting;
using Settings.Domain.AggregatesModel.GeneralSettingAggregate;

namespace Settings.Application.Features.GeneralSettings.Queries.Get;

public sealed class GetGeneralSettingQueryHandler(IGeneralSettingRepository repository)
    : IRequestHandler<GetGeneralSettingQuery, GeneralSettingDto?>
{
    public async Task<GeneralSettingDto?> Handle(GetGeneralSettingQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetAsync(cancellationToken);
        return entity == null ? null : Map(entity);
    }

    private static GeneralSettingDto Map(GeneralSetting e) => new(
        e.Id, e.LogoPath, e.CurrencyCode, e.DateFormat, e.PriceDisplayType,
        e.DefaultLanguageCode, e.NotificationLanguageCode, e.BankCode, e.Timezone,
        e.UseCreditLimit, e.CreatedAt, e.UpdatedAt);
}
