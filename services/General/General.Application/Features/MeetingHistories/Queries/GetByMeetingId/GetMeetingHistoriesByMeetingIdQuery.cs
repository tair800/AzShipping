using General.Application.DTOs.MeetingHistory;
using MediatR;

namespace General.Application.Features.MeetingHistories.Queries.GetByMeetingId;

public record GetMeetingHistoriesByMeetingIdQuery(Guid MeetingId) : IRequest<IReadOnlyList<MeetingHistoryDto>>;
