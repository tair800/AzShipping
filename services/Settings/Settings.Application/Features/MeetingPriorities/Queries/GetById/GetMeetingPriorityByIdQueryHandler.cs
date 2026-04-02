using MediatR;
using Settings.Application.DTOs.MeetingPriority;
using Settings.Application.Features.MeetingPriorities;
using Settings.Domain.AggregatesModel.MeetingPriorityAggregate;

namespace Settings.Application.Features.MeetingPriorities.Queries.GetById;

public sealed class GetMeetingPriorityByIdQueryHandler(IMeetingPriorityRepository repository) : IRequestHandler<GetMeetingPriorityByIdQuery, MeetingPriorityDto?>
{
    public async Task<MeetingPriorityDto?> Handle(GetMeetingPriorityByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : MeetingPriorityMapper.MapToDto(entity);
    }
}
