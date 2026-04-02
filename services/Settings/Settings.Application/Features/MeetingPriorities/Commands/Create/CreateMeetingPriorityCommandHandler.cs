using MediatR;
using Settings.Application.DTOs.MeetingPriority;
using Settings.Application.Features.MeetingPriorities;
using Settings.Domain.AggregatesModel.MeetingPriorityAggregate;

namespace Settings.Application.Features.MeetingPriorities.Commands.Create;

public sealed class CreateMeetingPriorityCommandHandler(IMeetingPriorityRepository repository) : IRequestHandler<CreateMeetingPriorityCommand, MeetingPriorityDto>
{
    public async Task<MeetingPriorityDto> Handle(CreateMeetingPriorityCommand request, CancellationToken cancellationToken)
    {
        var entity = new MeetingPriority
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            PrimaryColor = request.Dto.PrimaryColor,
            SecondaryColor = request.Dto.SecondaryColor,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        return MeetingPriorityMapper.MapToDto(entity);
    }
}
