using MediatR;
using Settings.Application.DTOs.MeetingType;

namespace Settings.Application.Features.MeetingTypes.Queries.GetAll;

public sealed record GetAllMeetingTypesQuery : IRequest<IReadOnlyList<MeetingTypeDto>>;
