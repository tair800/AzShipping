using MediatR;
using Settings.Application.DTOs.GeneralSetting;

namespace Settings.Application.Features.GeneralSettings.Commands.Update;

public sealed record UpdateGeneralSettingCommand(UpdateGeneralSettingDto Dto) : IRequest<GeneralSettingDto>;
