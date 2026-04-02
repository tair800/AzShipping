using MediatR;
using Settings.Application.DTOs.EmailSetting;

namespace Settings.Application.Features.EmailSettings.Commands.Update;

public sealed record UpdateEmailSettingCommand(Guid Id, UpdateEmailSettingDto Dto) : IRequest<EmailSettingDetailDto?>;
