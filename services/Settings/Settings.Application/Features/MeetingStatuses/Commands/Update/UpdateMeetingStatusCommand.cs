using MediatR;
using Settings.Application.DTOs.MeetingStatus;

namespace Settings.Application.Features.MeetingStatuses.Commands.Update;

public sealed record UpdateMeetingStatusCommand(Guid Id, UpdateMeetingStatusDto Dto) : IRequest<MeetingStatusDto?>;
