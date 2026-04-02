using MediatR;
using Settings.Application.DTOs.MeetingPriority;

namespace Settings.Application.Features.MeetingPriorities.Commands.Create;

public sealed record CreateMeetingPriorityCommand(CreateMeetingPriorityDto Dto) : IRequest<MeetingPriorityDto>;
