using MediatR;
using Settings.Application.DTOs.EmailSetting;

namespace Settings.Application.Features.EmailSettings.Queries.GetById;

public sealed record GetEmailSettingByIdQuery(Guid Id) : IRequest<EmailSettingDetailDto?>;
