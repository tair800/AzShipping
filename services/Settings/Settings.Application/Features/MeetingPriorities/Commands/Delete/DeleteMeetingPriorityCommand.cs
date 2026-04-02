using MediatR;

namespace Settings.Application.Features.MeetingPriorities.Commands.Delete;

public sealed record DeleteMeetingPriorityCommand(Guid Id) : IRequest<bool>;
