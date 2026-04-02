using MediatR;
using Settings.Application.DTOs.MeetingType;
using Settings.Application.Features.MeetingTypes;
using Settings.Domain.AggregatesModel.MeetingTypeAggregate;

namespace Settings.Application.Features.MeetingTypes.Commands.Update;

public sealed class UpdateMeetingTypeCommandHandler(IMeetingTypeRepository repository) : IRequestHandler<UpdateMeetingTypeCommand, MeetingTypeDto?>
{
    public async Task<MeetingTypeDto?> Handle(UpdateMeetingTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        return MeetingTypeMapper.MapToDto(entity);
    }
}
