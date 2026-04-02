using General.Application.DTOs.Meeting;
using General.Application.Features.Meetings;
using General.Application.Services;
using General.Domain.AggregatesModel.MeetingAggregate;
using General.Domain.AggregatesModel.MeetingHistoryAggregate;
using MediatR;

namespace General.Application.Features.Meetings.Commands.Update;

public class UpdateMeetingCommandHandler(IMeetingRepository repository, IMeetingHistoryRepository historyRepo, IActionLogClient actionLogClient)
    : IRequestHandler<UpdateMeetingCommand, MeetingDto?>
{
    public async Task<MeetingDto?> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        var dto = request.Dto;

        await AddHistoryIfChanged(entity.Id, "Name", entity.Name, dto.Name, cancellationToken);
        await AddHistoryIfChanged(entity.Id, "MeetingTypeId", entity.MeetingTypeId?.ToString(), dto.MeetingTypeId?.ToString(), cancellationToken);
        await AddHistoryIfChanged(entity.Id, "MeetingResultId", entity.MeetingResultId?.ToString(), dto.MeetingResultId?.ToString(), cancellationToken);
        await AddHistoryIfChanged(entity.Id, "MeetingStatusId", entity.MeetingStatusId?.ToString(), dto.MeetingStatusId?.ToString(), cancellationToken);
        await AddHistoryIfChanged(entity.Id, "ClientId", entity.ClientId?.ToString(), dto.ClientId?.ToString(), cancellationToken);
        await AddHistoryIfChanged(entity.Id, "TaskId", entity.TaskId?.ToString(), dto.TaskId?.ToString(), cancellationToken);
        await AddHistoryIfChanged(entity.Id, "OperationId", entity.OperationId?.ToString(), dto.OperationId?.ToString(), cancellationToken);
        await AddHistoryIfChanged(entity.Id, "MeetingPriorityId", entity.MeetingPriorityId?.ToString(), dto.MeetingPriorityId?.ToString(), cancellationToken);
        await AddHistoryIfChanged(entity.Id, "Date", entity.Date?.ToString("O"), dto.Date?.ToString("O"), cancellationToken);
        await AddHistoryIfChanged(entity.Id, "Time", entity.Time, dto.Time, cancellationToken);
        await AddHistoryIfChanged(entity.Id, "Address", entity.Address, dto.Address, cancellationToken);
        await AddHistoryIfChanged(entity.Id, "Comments", entity.Comments, dto.Comments, cancellationToken);
        await AddHistoryIfChanged(entity.Id, "HasClient", entity.HasClient.ToString(), dto.HasClient.ToString(), cancellationToken);

        entity.Name = dto.Name;
        entity.MeetingTypeId = dto.MeetingTypeId;
        entity.MeetingResultId = dto.MeetingResultId;
        entity.MeetingStatusId = dto.MeetingStatusId;
        entity.ClientId = dto.ClientId;
        entity.TaskId = dto.TaskId;
        entity.OperationId = dto.OperationId;
        entity.MeetingPriorityId = dto.MeetingPriorityId;
        entity.Date = dto.Date;
        entity.Time = dto.Time;
        entity.Address = dto.Address;
        entity.Comments = dto.Comments;
        entity.HasClient = dto.HasClient;
        entity.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(entity, cancellationToken);
        var updated = await repository.GetByIdAsync(request.Id, cancellationToken);
        var result = MeetingMapper.MapToDto(updated!);
        await actionLogClient.LogAsync("Meeting updated", $"meeting: {entity.Name} • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }

    private async Task AddHistoryIfChanged(Guid meetingId, string fieldName, string? oldVal, string? newVal, CancellationToken ct)
    {
        if (string.Equals(oldVal ?? "", newVal ?? "", StringComparison.Ordinal)) return;
        var h = new MeetingHistory
        {
            Id = Guid.NewGuid(),
            MeetingId = meetingId,
            EventType = "Edit",
            FieldName = fieldName,
            OldValue = Truncate(oldVal, 500),
            NewValue = Truncate(newVal, 500),
            CreatedAt = DateTime.UtcNow
        };
        await historyRepo.AddAsync(h, ct);
    }

    private static string? Truncate(string? s, int max) => s == null ? null : s.Length <= max ? s : s[..max];
}
