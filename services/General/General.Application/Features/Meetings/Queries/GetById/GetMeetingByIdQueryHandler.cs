using General.Application.DTOs.Meeting;
using General.Application.Features.Meetings;
using General.Domain.AggregatesModel.MeetingAggregate;
using MediatR;

namespace General.Application.Features.Meetings.Queries.GetById;

public class GetMeetingByIdQueryHandler(IMeetingRepository repository)
    : IRequestHandler<GetMeetingByIdQuery, MeetingDto?>
{
    public async Task<MeetingDto?> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return MeetingMapper.MapToDto(entity);
    }
}
