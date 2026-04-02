using MediatR;
using Settings.Application.DTOs.MeetingPriority;
using Settings.Application.Features.MeetingPriorities;
using Settings.Domain.AggregatesModel.MeetingPriorityAggregate;

namespace Settings.Application.Features.MeetingPriorities.Queries.GetAll;

public sealed class GetAllMeetingPrioritiesQueryHandler(IMeetingPriorityRepository repository) : IRequestHandler<GetAllMeetingPrioritiesQuery, IReadOnlyList<MeetingPriorityDto>>
{
    public async Task<IReadOnlyList<MeetingPriorityDto>> Handle(GetAllMeetingPrioritiesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(MeetingPriorityMapper.MapToDto).ToList();
    }
}
