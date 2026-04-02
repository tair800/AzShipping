using MediatR;
using Settings.Application.DTOs.ClientSegment;
using Settings.Domain.AggregatesModel.ClientSegmentAggregate;

namespace Settings.Application.Features.ClientSegments.Queries.GetAll;

public sealed class GetAllClientSegmentsQueryHandler(IClientSegmentRepository repository) : IRequestHandler<GetAllClientSegmentsQuery, IReadOnlyList<ClientSegmentDto>>
{
    public async Task<IReadOnlyList<ClientSegmentDto>> Handle(GetAllClientSegmentsQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(e => new ClientSegmentDto { Id = e.Id, SegmentName = e.SegmentName, SegmentPriority = e.SegmentPriority, IsActive = e.IsActive, IsDefault = e.IsDefault, PrimaryColor = e.PrimaryColor, SecondaryColor = e.SecondaryColor, CreatedAt = e.CreatedAt, UpdatedAt = e.UpdatedAt }).ToList();
    }
}
