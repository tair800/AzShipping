using MediatR;
using Settings.Application.DTOs.MeetingType;

namespace Settings.Application.Features.MeetingTypes.Commands.Create;

public sealed record CreateMeetingTypeCommand(CreateMeetingTypeDto Dto) : IRequest<MeetingTypeDto>;
