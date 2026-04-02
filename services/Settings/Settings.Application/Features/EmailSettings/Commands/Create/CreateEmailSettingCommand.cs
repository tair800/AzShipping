using MediatR;
using Settings.Application.DTOs.EmailSetting;

namespace Settings.Application.Features.EmailSettings.Commands.Create;

public sealed record CreateEmailSettingCommand(CreateEmailSettingDto Dto) : IRequest<EmailSettingDetailDto>;
