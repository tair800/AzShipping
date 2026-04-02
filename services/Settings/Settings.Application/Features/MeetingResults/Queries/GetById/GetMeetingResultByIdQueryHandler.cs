using MediatR;
using Settings.Application.DTOs.MeetingResult;
using Settings.Application.Features.MeetingResults;
using Settings.Domain.AggregatesModel.MeetingResultAggregate;

namespace Settings.Application.Features.MeetingResults.Queries.GetById;

public sealed class GetMeetingResultByIdQueryHandler(IMeetingResultRepository repository) : IRequestHandler<GetMeetingResultByIdQuery, MeetingResultDto?>
{
    public async Task<MeetingResultDto?> Handle(GetMeetingResultByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : MeetingResultMapper.MapToDto(entity);
    }
}
