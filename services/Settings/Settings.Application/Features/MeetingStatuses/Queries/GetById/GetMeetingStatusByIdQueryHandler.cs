using MediatR;
using Settings.Application.DTOs.MeetingStatus;
using Settings.Application.Features.MeetingStatuses;
using Settings.Domain.AggregatesModel.MeetingStatusAggregate;

namespace Settings.Application.Features.MeetingStatuses.Queries.GetById;

public sealed class GetMeetingStatusByIdQueryHandler(IMeetingStatusRepository repository) : IRequestHandler<GetMeetingStatusByIdQuery, MeetingStatusDto?>
{
    public async Task<MeetingStatusDto?> Handle(GetMeetingStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : MeetingStatusMapper.MapToDto(entity);
    }
}
