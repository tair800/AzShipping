using MediatR;
using Settings.Application.DTOs.MeetingResult;
using Settings.Application.Features.MeetingResults;
using Settings.Domain.AggregatesModel.MeetingResultAggregate;

namespace Settings.Application.Features.MeetingResults.Commands.Update;

public sealed class UpdateMeetingResultCommandHandler(IMeetingResultRepository repository) : IRequestHandler<UpdateMeetingResultCommand, MeetingResultDto?>
{
    public async Task<MeetingResultDto?> Handle(UpdateMeetingResultCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.PrimaryColor = request.Dto.PrimaryColor;
        entity.SecondaryColor = request.Dto.SecondaryColor;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        return MeetingResultMapper.MapToDto(entity);
    }
}
