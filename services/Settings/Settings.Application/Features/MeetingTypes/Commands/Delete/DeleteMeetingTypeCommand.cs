using MediatR;

namespace Settings.Application.Features.MeetingTypes.Commands.Delete;

public sealed record DeleteMeetingTypeCommand(Guid Id) : IRequest<bool>;
