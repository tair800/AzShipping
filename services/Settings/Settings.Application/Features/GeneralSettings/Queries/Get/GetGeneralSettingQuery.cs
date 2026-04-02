using MediatR;
using Settings.Application.DTOs.GeneralSetting;

namespace Settings.Application.Features.GeneralSettings.Queries.Get;

public sealed record GetGeneralSettingQuery : IRequest<GeneralSettingDto?>;
