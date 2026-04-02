using MediatR;
using Settings.Application.DTOs.MeetingPriority;
using Settings.Application.Features.MeetingPriorities;
using Settings.Domain.AggregatesModel.MeetingPriorityAggregate;

namespace Settings.Application.Features.MeetingPriorities.Commands.Update;

public sealed class UpdateMeetingPriorityCommandHandler(IMeetingPriorityRepository repository) : IRequestHandler<UpdateMeetingPriorityCommand, MeetingPriorityDto?>
{
    public async Task<MeetingPriorityDto?> Handle(UpdateMeetingPriorityCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.PrimaryColor = request.Dto.PrimaryColor;
        entity.SecondaryColor = request.Dto.SecondaryColor;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        return MeetingPriorityMapper.MapToDto(entity);
    }
}
