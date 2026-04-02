using MediatR;
using Settings.Application.DTOs.MeetingStatus;

namespace Settings.Application.Features.MeetingStatuses.Queries.GetAll;

public sealed record GetAllMeetingStatusesQuery : IRequest<IReadOnlyList<MeetingStatusDto>>;
