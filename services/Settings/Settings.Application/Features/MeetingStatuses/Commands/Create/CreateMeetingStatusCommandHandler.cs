using MediatR;
using Settings.Application.DTOs.MeetingStatus;
using Settings.Application.Features.MeetingStatuses;
using Settings.Domain.AggregatesModel.MeetingStatusAggregate;

namespace Settings.Application.Features.MeetingStatuses.Commands.Create;

public sealed class CreateMeetingStatusCommandHandler(IMeetingStatusRepository repository) : IRequestHandler<CreateMeetingStatusCommand, MeetingStatusDto>
{
    public async Task<MeetingStatusDto> Handle(CreateMeetingStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = new MeetingStatus
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            PrimaryColor = request.Dto.PrimaryColor,
            SecondaryColor = request.Dto.SecondaryColor,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        return MeetingStatusMapper.MapToDto(entity);
    }
}
