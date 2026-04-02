using MediatR;
using Settings.Application.DTOs.MeetingPriority;

namespace Settings.Application.Features.MeetingPriorities.Commands.Update;

public sealed record UpdateMeetingPriorityCommand(Guid Id, UpdateMeetingPriorityDto Dto) : IRequest<MeetingPriorityDto?>;
