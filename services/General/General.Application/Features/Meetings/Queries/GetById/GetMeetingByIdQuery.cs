using General.Application.DTOs.Meeting;
using MediatR;

namespace General.Application.Features.Meetings.Queries.GetById;

public record GetMeetingByIdQuery(Guid Id) : IRequest<MeetingDto?>;
