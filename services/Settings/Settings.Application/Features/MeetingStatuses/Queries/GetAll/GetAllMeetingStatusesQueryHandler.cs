using MediatR;
using Settings.Application.DTOs.MeetingStatus;
using Settings.Application.Features.MeetingStatuses;
using Settings.Domain.AggregatesModel.MeetingStatusAggregate;

namespace Settings.Application.Features.MeetingStatuses.Queries.GetAll;

public sealed class GetAllMeetingStatusesQueryHandler(IMeetingStatusRepository repository) : IRequestHandler<GetAllMeetingStatusesQuery, IReadOnlyList<MeetingStatusDto>>
{
    public async Task<IReadOnlyList<MeetingStatusDto>> Handle(GetAllMeetingStatusesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(MeetingStatusMapper.MapToDto).ToList();
    }
}
