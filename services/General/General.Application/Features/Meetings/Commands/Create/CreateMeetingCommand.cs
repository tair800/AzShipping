using General.Application.DTOs.Meeting;
using MediatR;

namespace General.Application.Features.Meetings.Commands.Create;

public record CreateMeetingCommand(CreateMeetingDto Dto) : IRequest<MeetingDto>;
