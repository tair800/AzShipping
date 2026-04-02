using MediatR;

namespace General.Application.Features.Meetings.Commands.Delete;

public record DeleteMeetingCommand(Guid Id) : IRequest<bool>;
