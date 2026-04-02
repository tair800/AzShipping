using MediatR;
using Settings.Application.DTOs.MeetingResult;

namespace Settings.Application.Features.MeetingResults.Queries.GetAll;

public sealed record GetAllMeetingResultsQuery : IRequest<IReadOnlyList<MeetingResultDto>>;
