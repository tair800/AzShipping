using MediatR;
using Settings.Application.DTOs.EmailSetting;

namespace Settings.Application.Features.EmailSettings.Commands.LinkIdentityUser;

public sealed record LinkIdentityUserToEmailSettingCommand(Guid Id, LinkIdentityUserToEmailSettingDto Dto)
    : IRequest<EmailSettingDetailDto?>;
