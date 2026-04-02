using MediatR;
using Settings.Application.DTOs.ClientSegment;

namespace Settings.Application.Features.ClientSegments.Queries.GetAll;

public sealed record GetAllClientSegmentsQuery : IRequest<IReadOnlyList<ClientSegmentDto>>;
