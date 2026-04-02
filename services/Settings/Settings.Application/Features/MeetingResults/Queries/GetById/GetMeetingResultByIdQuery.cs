using MediatR;
using Settings.Application.DTOs.MeetingResult;

namespace Settings.Application.Features.MeetingResults.Queries.GetById;

public sealed record GetMeetingResultByIdQuery(Guid Id) : IRequest<MeetingResultDto?>;
