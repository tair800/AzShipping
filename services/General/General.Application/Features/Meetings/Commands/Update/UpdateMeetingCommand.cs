using General.Application.DTOs.Meeting;
using MediatR;

namespace General.Application.Features.Meetings.Commands.Update;

public record UpdateMeetingCommand(Guid Id, UpdateMeetingDto Dto) : IRequest<MeetingDto?>;
