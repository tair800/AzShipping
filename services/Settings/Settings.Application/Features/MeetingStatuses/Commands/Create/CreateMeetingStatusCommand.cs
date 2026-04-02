using MediatR;
using Settings.Application.DTOs.MeetingStatus;

namespace Settings.Application.Features.MeetingStatuses.Commands.Create;

public sealed record CreateMeetingStatusCommand(CreateMeetingStatusDto Dto) : IRequest<MeetingStatusDto>;
