using General.Application.DTOs.Meeting;
using General.Application.Features.Meetings;
using General.Domain.AggregatesModel.MeetingAggregate;
using MediatR;

namespace General.Application.Features.Meetings.Queries.GetAll;

public class GetAllMeetingsQueryHandler(IMeetingRepository repository)
    : IRequestHandler<GetAllMeetingsQuery, IReadOnlyList<MeetingDto>>
{
    public async Task<IReadOnlyList<MeetingDto>> Handle(GetAllMeetingsQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetAllAsync(cancellationToken);
        return items.Select(MeetingMapper.MapToDto).ToList();
    }
}
