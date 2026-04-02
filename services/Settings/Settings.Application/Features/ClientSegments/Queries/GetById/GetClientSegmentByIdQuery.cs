using MediatR;
using Settings.Application.DTOs.ClientSegment;

namespace Settings.Application.Features.ClientSegments.Queries.GetById;

public sealed record GetClientSegmentByIdQuery(Guid Id) : IRequest<ClientSegmentDto?>;
