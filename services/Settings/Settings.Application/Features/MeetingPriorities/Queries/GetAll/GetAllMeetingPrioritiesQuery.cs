using MediatR;
using Settings.Application.DTOs.MeetingPriority;

namespace Settings.Application.Features.MeetingPriorities.Queries.GetAll;

public sealed record GetAllMeetingPrioritiesQuery : IRequest<IReadOnlyList<MeetingPriorityDto>>;
