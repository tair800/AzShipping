using General.Application.DTOs.Meeting;
using General.Application.Features.Meetings;
using General.Application.Services;
using General.Domain.AggregatesModel.MeetingAggregate;
using MediatR;

namespace General.Application.Features.Meetings.Commands.Create;

public class CreateMeetingCommandHandler(IMeetingRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<CreateMeetingCommand, MeetingDto>
{
    public async Task<MeetingDto> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new Meeting
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            MeetingTypeId = dto.MeetingTypeId,
            MeetingResultId = dto.MeetingResultId,
            MeetingStatusId = dto.MeetingStatusId,
            ClientId = dto.ClientId,
            TaskId = dto.TaskId,
            OperationId = dto.OperationId,
            MeetingPriorityId = dto.MeetingPriorityId,
            Date = dto.Date,
            Time = dto.Time,
            Address = dto.Address,
            Comments = dto.Comments,
            HasClient = dto.HasClient,
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        var result = MeetingMapper.MapToDto(created!);
        await actionLogClient.LogAsync("Meeting created", $"meeting: {entity.Name} • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }
}
