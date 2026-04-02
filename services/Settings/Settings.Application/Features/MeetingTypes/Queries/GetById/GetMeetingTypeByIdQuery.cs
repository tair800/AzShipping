using MediatR;
using Settings.Application.DTOs.MeetingType;

namespace Settings.Application.Features.MeetingTypes.Queries.GetById;

public sealed record GetMeetingTypeByIdQuery(Guid Id) : IRequest<MeetingTypeDto?>;
