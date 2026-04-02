using MediatR;
using Settings.Application.DTOs.EmailSetting;
using Settings.Domain.AggregatesModel.EmailAccountAggregate;

namespace Settings.Application.Features.EmailSettings.Queries.GetById;

public sealed class GetEmailSettingByIdQueryHandler(IEmailAccountSettingRepository repository)
    : IRequestHandler<GetEmailSettingByIdQuery, EmailSettingDetailDto?>
{
    public async Task<EmailSettingDetailDto?> Handle(GetEmailSettingByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await repository.GetByIdAsync(request.Id, cancellationToken);
        return e == null ? null : EmailSettingMapper.ToDetail(e);
    }
}
