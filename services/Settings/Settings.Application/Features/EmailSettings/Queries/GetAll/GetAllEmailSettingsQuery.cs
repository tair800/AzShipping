using MediatR;
using Settings.Application.DTOs.EmailSetting;

namespace Settings.Application.Features.EmailSettings.Queries.GetAll;

public sealed record GetAllEmailSettingsQuery : IRequest<IReadOnlyList<EmailSettingListItemDto>>;
