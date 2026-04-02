using MediatR;
using Settings.Application.DTOs.ClientSegment;
using Settings.Domain.AggregatesModel.ClientSegmentAggregate;

namespace Settings.Application.Features.ClientSegments.Queries.GetById;

public sealed class GetClientSegmentByIdQueryHandler(IClientSegmentRepository repository) : IRequestHandler<GetClientSegmentByIdQuery, ClientSegmentDto?>
{
    public async Task<ClientSegmentDto?> Handle(GetClientSegmentByIdQuery request, CancellationToken cancellationToken)
    {
        var e = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (e == null) return null;
        return new ClientSegmentDto { Id = e.Id, SegmentName = e.SegmentName, SegmentPriority = e.SegmentPriority, IsActive = e.IsActive, IsDefault = e.IsDefault, PrimaryColor = e.PrimaryColor, SecondaryColor = e.SecondaryColor, CreatedAt = e.CreatedAt, UpdatedAt = e.UpdatedAt };
    }
}
