using MediatR;
using Settings.Application.DTOs.MeetingType;
using Settings.Application.Features.MeetingTypes;
using Settings.Domain.AggregatesModel.MeetingTypeAggregate;

namespace Settings.Application.Features.MeetingTypes.Queries.GetById;

public sealed class GetMeetingTypeByIdQueryHandler(IMeetingTypeRepository repository) : IRequestHandler<GetMeetingTypeByIdQuery, MeetingTypeDto?>
{
    public async Task<MeetingTypeDto?> Handle(GetMeetingTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : MeetingTypeMapper.MapToDto(entity);
    }
}
