using MediatR;
using Settings.Application.DTOs.GeneralSetting;
using Settings.Application.Services;
using Settings.Domain.AggregatesModel.GeneralSettingAggregate;

namespace Settings.Application.Features.GeneralSettings.Commands.Update;

public sealed class UpdateGeneralSettingCommandHandler(IGeneralSettingRepository repository, IInternalActionLogService actionLog)
    : IRequestHandler<UpdateGeneralSettingCommand, GeneralSettingDto>
{
    public async Task<GeneralSettingDto> Handle(UpdateGeneralSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetOrCreateAsync(cancellationToken);
        var d = request.Dto;

        if (d.LogoPath != null) entity.LogoPath = d.LogoPath;
        if (d.CurrencyCode != null) entity.CurrencyCode = d.CurrencyCode;
        if (d.DateFormat != null) entity.DateFormat = d.DateFormat;
        if (d.PriceDisplayType != null) entity.PriceDisplayType = d.PriceDisplayType;
        if (d.DefaultLanguageCode != null) entity.DefaultLanguageCode = d.DefaultLanguageCode;
        if (d.NotificationLanguageCode != null) entity.NotificationLanguageCode = d.NotificationLanguageCode;
        if (d.BankCode != null) entity.BankCode = d.BankCode;
        if (d.Timezone != null) entity.Timezone = d.Timezone;
        if (d.UseCreditLimit.HasValue) entity.UseCreditLimit = d.UseCreditLimit.Value;

        await repository.SaveAsync(entity, cancellationToken);

        await actionLog.LogAsync("General settings updated", $"settings id: {entity.Id}", null, null, cancellationToken);

        return new GeneralSettingDto(
            entity.Id, entity.LogoPath, entity.CurrencyCode, entity.DateFormat, entity.PriceDisplayType,
            entity.DefaultLanguageCode, entity.NotificationLanguageCode, entity.BankCode, entity.Timezone,
            entity.UseCreditLimit, entity.CreatedAt, entity.UpdatedAt);
    }
}
