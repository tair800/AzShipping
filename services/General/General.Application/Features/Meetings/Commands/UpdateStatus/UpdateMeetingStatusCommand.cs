using General.Application.DTOs.Meeting;
using MediatR;

namespace General.Application.Features.Meetings.Commands.UpdateStatus;

public sealed record UpdateMeetingStatusCommand(Guid Id, Guid? MeetingStatusId) : IRequest<MeetingDto?>;
