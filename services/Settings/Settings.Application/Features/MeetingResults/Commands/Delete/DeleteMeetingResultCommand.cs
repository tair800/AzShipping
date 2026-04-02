using MediatR;

namespace Settings.Application.Features.MeetingResults.Commands.Delete;

public sealed record DeleteMeetingResultCommand(Guid Id) : IRequest<bool>;
