using MediatR;
using Settings.Application.DTOs.MeetingResult;
using Settings.Application.Features.MeetingResults;
using Settings.Domain.AggregatesModel.MeetingResultAggregate;

namespace Settings.Application.Features.MeetingResults.Queries.GetAll;

public sealed class GetAllMeetingResultsQueryHandler(IMeetingResultRepository repository) : IRequestHandler<GetAllMeetingResultsQuery, IReadOnlyList<MeetingResultDto>>
{
    public async Task<IReadOnlyList<MeetingResultDto>> Handle(GetAllMeetingResultsQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(MeetingResultMapper.MapToDto).ToList();
    }
}
