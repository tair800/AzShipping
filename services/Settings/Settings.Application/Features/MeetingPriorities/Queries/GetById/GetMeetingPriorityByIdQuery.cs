using MediatR;
using Settings.Application.DTOs.MeetingPriority;

namespace Settings.Application.Features.MeetingPriorities.Queries.GetById;

public sealed record GetMeetingPriorityByIdQuery(Guid Id) : IRequest<MeetingPriorityDto?>;
