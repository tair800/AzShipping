using General.Application.DTOs.MeetingHistory;
using MediatR;

namespace General.Application.Features.MeetingHistories.Commands.Create;

public record CreateMeetingHistoryCommand(CreateMeetingHistoryDto Dto) : IRequest<MeetingHistoryDto>;
