using MediatR;
using Settings.Application.DTOs.MeetingType;
using Settings.Application.Features.MeetingTypes;
using Settings.Domain.AggregatesModel.MeetingTypeAggregate;

namespace Settings.Application.Features.MeetingTypes.Queries.GetAll;

public sealed class GetAllMeetingTypesQueryHandler(IMeetingTypeRepository repository) : IRequestHandler<GetAllMeetingTypesQuery, IReadOnlyList<MeetingTypeDto>>
{
    public async Task<IReadOnlyList<MeetingTypeDto>> Handle(GetAllMeetingTypesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(MeetingTypeMapper.MapToDto).ToList();
    }
}
