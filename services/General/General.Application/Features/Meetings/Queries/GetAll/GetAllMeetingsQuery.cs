using General.Application.DTOs.Meeting;
using MediatR;

namespace General.Application.Features.Meetings.Queries.GetAll;

public record GetAllMeetingsQuery : IRequest<IReadOnlyList<MeetingDto>>;
