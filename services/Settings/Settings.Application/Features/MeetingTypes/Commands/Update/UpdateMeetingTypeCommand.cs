using MediatR;
using Settings.Application.DTOs.MeetingType;

namespace Settings.Application.Features.MeetingTypes.Commands.Update;

public sealed record UpdateMeetingTypeCommand(Guid Id, UpdateMeetingTypeDto Dto) : IRequest<MeetingTypeDto?>;
