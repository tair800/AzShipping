using MediatR;
using Settings.Application.DTOs.MeetingResult;
using Settings.Application.Features.MeetingResults;
using Settings.Domain.AggregatesModel.MeetingResultAggregate;

namespace Settings.Application.Features.MeetingResults.Commands.Create;

public sealed class CreateMeetingResultCommandHandler(IMeetingResultRepository repository) : IRequestHandler<CreateMeetingResultCommand, MeetingResultDto>
{
    public async Task<MeetingResultDto> Handle(CreateMeetingResultCommand request, CancellationToken cancellationToken)
    {
        var entity = new MeetingResult
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            PrimaryColor = request.Dto.PrimaryColor,
            SecondaryColor = request.Dto.SecondaryColor,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        return MeetingResultMapper.MapToDto(entity);
    }
}
