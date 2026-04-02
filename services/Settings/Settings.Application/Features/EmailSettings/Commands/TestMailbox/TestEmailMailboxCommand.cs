using MediatR;
using Settings.Application.DTOs.EmailSetting;

namespace Settings.Application.Features.EmailSettings.Commands.TestMailbox;

public sealed record TestEmailMailboxCommand(Guid Id, TestEmailMailboxDto Dto) : IRequest<bool>;
