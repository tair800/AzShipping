using MediatR;
using Settings.Application.DTOs.EmailSetting;
using Settings.Domain.AggregatesModel.EmailAccountAggregate;

namespace Settings.Application.Features.EmailSettings.Queries.GetAll;

public sealed class GetAllEmailSettingsQueryHandler(IEmailAccountSettingRepository repository)
    : IRequestHandler<GetAllEmailSettingsQuery, IReadOnlyList<EmailSettingListItemDto>>
{
    public async Task<IReadOnlyList<EmailSettingListItemDto>> Handle(GetAllEmailSettingsQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(EmailSettingMapper.ToListItem).ToList();
    }
}
