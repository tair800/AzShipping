using General.Application.DTOs.MeetingHistory;
using General.Application.Features.MeetingHistories;
using General.Application.Services;
using General.Domain.AggregatesModel.MeetingHistoryAggregate;
using MediatR;

namespace General.Application.Features.MeetingHistories.Commands.Create;

public class CreateMeetingHistoryCommandHandler(IMeetingHistoryRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<CreateMeetingHistoryCommand, MeetingHistoryDto>
{
    public async Task<MeetingHistoryDto> Handle(CreateMeetingHistoryCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new MeetingHistory
        {
            Id = Guid.NewGuid(),
            MeetingId = dto.MeetingId,
            EventType = dto.EventType,
            Date = dto.Date,
            Time = dto.Time,
            EventResultId = dto.EventResultId,
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        var result = MeetingHistoryMapper.MapToDto(created!);
        await actionLogClient.LogAsync("Meeting history created", $"meeting history: meeting {entity.MeetingId} • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }
}
