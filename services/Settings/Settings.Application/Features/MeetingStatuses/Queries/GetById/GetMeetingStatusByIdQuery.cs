using MediatR;
using Settings.Application.DTOs.MeetingStatus;

namespace Settings.Application.Features.MeetingStatuses.Queries.GetById;

public sealed record GetMeetingStatusByIdQuery(Guid Id) : IRequest<MeetingStatusDto?>;
