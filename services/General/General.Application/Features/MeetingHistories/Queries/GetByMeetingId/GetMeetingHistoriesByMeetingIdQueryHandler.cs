using General.Application.DTOs.MeetingHistory;
using General.Application.Features.MeetingHistories;
using General.Domain.AggregatesModel.MeetingHistoryAggregate;
using MediatR;

namespace General.Application.Features.MeetingHistories.Queries.GetByMeetingId;

public class GetMeetingHistoriesByMeetingIdQueryHandler(IMeetingHistoryRepository repository)
    : IRequestHandler<GetMeetingHistoriesByMeetingIdQuery, IReadOnlyList<MeetingHistoryDto>>
{
    public async Task<IReadOnlyList<MeetingHistoryDto>> Handle(GetMeetingHistoriesByMeetingIdQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetByMeetingIdAsync(request.MeetingId, cancellationToken);
        return items.Select(MeetingHistoryMapper.MapToDto).ToList();
    }
}
