using MediatR;
using Settings.Application.DTOs.MeetingStatus;
using Settings.Application.Features.MeetingStatuses;
using Settings.Domain.AggregatesModel.MeetingStatusAggregate;

namespace Settings.Application.Features.MeetingStatuses.Commands.Update;

public sealed class UpdateMeetingStatusCommandHandler(IMeetingStatusRepository repository) : IRequestHandler<UpdateMeetingStatusCommand, MeetingStatusDto?>
{
    public async Task<MeetingStatusDto?> Handle(UpdateMeetingStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.PrimaryColor = request.Dto.PrimaryColor;
        entity.SecondaryColor = request.Dto.SecondaryColor;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        return MeetingStatusMapper.MapToDto(entity);
    }
}
