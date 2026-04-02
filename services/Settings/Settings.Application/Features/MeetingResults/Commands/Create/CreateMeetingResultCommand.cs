using MediatR;
using Settings.Application.DTOs.MeetingResult;

namespace Settings.Application.Features.MeetingResults.Commands.Create;

public sealed record CreateMeetingResultCommand(CreateMeetingResultDto Dto) : IRequest<MeetingResultDto>;
