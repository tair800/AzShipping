using General.Application.DTOs.Meeting;
using General.Application.Features.Meetings;
using General.Domain.AggregatesModel.MeetingAggregate;
using General.Domain.AggregatesModel.MeetingHistoryAggregate;
using MediatR;

namespace General.Application.Features.Meetings.Commands.UpdateStatus;

public sealed class UpdateMeetingStatusCommandHandler(IMeetingRepository repository, IMeetingHistoryRepository historyRepo)
    : IRequestHandler<UpdateMeetingStatusCommand, MeetingDto?>
{
    public async Task<MeetingDto?> Handle(UpdateMeetingStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        var oldStatus = entity.MeetingStatusId?.ToString();
        var newStatus = request.MeetingStatusId?.ToString();
        if (!string.Equals(oldStatus ?? "", newStatus ?? "", StringComparison.Ordinal))
        {
            var h = new MeetingHistory
            {
                Id = Guid.NewGuid(),
                MeetingId = request.Id,
                EventType = "Edit",
                FieldName = "MeetingStatusId",
                OldValue = oldStatus,
                NewValue = newStatus,
                CreatedAt = DateTime.UtcNow
            };
            await historyRepo.AddAsync(h, cancellationToken);
        }

        entity.MeetingStatusId = request.MeetingStatusId;
        entity.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(entity, cancellationToken);
        var updated = await repository.GetByIdAsync(request.Id, cancellationToken);
        return MeetingMapper.MapToDto(updated!);
    }
}
