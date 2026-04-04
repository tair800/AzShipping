using MediatR;
using Settings.Application.DTOs.EmailSetting;
using Settings.Application.Features.EmailSettings;
using Settings.Domain.AggregatesModel.EmailAccountAggregate;

namespace Settings.Application.Features.EmailSettings.Commands.LinkIdentityUser;

public sealed class LinkIdentityUserToEmailSettingCommandHandler(IEmailAccountSettingRepository repository)
    : IRequestHandler<LinkIdentityUserToEmailSettingCommand, EmailSettingDetailDto?>
{
    public async Task<EmailSettingDetailDto?> Handle(LinkIdentityUserToEmailSettingCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetForUpdateAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        var dto = request.Dto;
        entity.IdentityUserId = dto.IdentityUserId;
        entity.LinkedUserDisplayName = string.IsNullOrWhiteSpace(dto.LinkedUserDisplayName)
            ? null
            : dto.LinkedUserDisplayName.Trim();
        entity.UpdatedAtUtc = DateTime.UtcNow;

        await repository.UpdateAsync(entity, cancellationToken);
        var loaded = await repository.GetByIdAsync(entity.Id, cancellationToken);
        return EmailSettingMapper.ToDetail(loaded ?? entity);
    }
}
