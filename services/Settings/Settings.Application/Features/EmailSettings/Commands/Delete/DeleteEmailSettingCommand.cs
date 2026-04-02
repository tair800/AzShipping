using MediatR;

namespace Settings.Application.Features.EmailSettings.Commands.Delete;

public sealed record DeleteEmailSettingCommand(Guid Id) : IRequest<bool>;
