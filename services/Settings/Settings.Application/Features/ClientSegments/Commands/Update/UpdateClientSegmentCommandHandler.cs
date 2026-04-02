using MediatR;
using Settings.Application.DTOs.ClientSegment;
using Settings.Domain.AggregatesModel.ClientSegmentAggregate;

namespace Settings.Application.Features.ClientSegments.Commands.Update;

public sealed class UpdateClientSegmentCommandHandler(IClientSegmentRepository repository) : IRequestHandler<UpdateClientSegmentCommand, ClientSegmentDto?>
{
    public async Task<ClientSegmentDto?> Handle(UpdateClientSegmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.SegmentName = request.Dto.SegmentName;
        entity.SegmentPriority = request.Dto.SegmentPriority;
        entity.IsActive = request.Dto.IsActive;
        entity.IsDefault = request.Dto.IsDefault;
        entity.PrimaryColor = request.Dto.PrimaryColor;
        entity.SecondaryColor = request.Dto.SecondaryColor;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        return new ClientSegmentDto { Id = entity.Id, SegmentName = entity.SegmentName, SegmentPriority = entity.SegmentPriority, IsActive = entity.IsActive, IsDefault = entity.IsDefault, PrimaryColor = entity.PrimaryColor, SecondaryColor = entity.SecondaryColor, CreatedAt = entity.CreatedAt, UpdatedAt = entity.UpdatedAt };
    }
}
