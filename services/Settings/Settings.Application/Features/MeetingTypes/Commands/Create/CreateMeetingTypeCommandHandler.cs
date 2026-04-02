using MediatR;
using Settings.Application.DTOs.MeetingType;
using Settings.Application.Features.MeetingTypes;
using Settings.Domain.AggregatesModel.MeetingTypeAggregate;

namespace Settings.Application.Features.MeetingTypes.Commands.Create;

public sealed class CreateMeetingTypeCommandHandler(IMeetingTypeRepository repository) : IRequestHandler<CreateMeetingTypeCommand, MeetingTypeDto>
{
    public async Task<MeetingTypeDto> Handle(CreateMeetingTypeCommand request, CancellationToken cancellationToken)
    {
        var entity = new MeetingType
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        return MeetingTypeMapper.MapToDto(entity);
    }
}
