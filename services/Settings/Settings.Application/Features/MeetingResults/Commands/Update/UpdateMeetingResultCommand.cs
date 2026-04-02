using MediatR;
using Settings.Application.DTOs.MeetingResult;

namespace Settings.Application.Features.MeetingResults.Commands.Update;

public sealed record UpdateMeetingResultCommand(Guid Id, UpdateMeetingResultDto Dto) : IRequest<MeetingResultDto?>;
