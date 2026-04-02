using MediatR;

namespace Settings.Application.Features.MeetingStatuses.Commands.Delete;

public sealed record DeleteMeetingStatusCommand(Guid Id) : IRequest<bool>;
