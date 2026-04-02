using MediatR;
using Settings.Application.DTOs.ClientSegment;
using Settings.Domain.AggregatesModel.ClientSegmentAggregate;

namespace Settings.Application.Features.ClientSegments.Commands.Create;

public sealed class CreateClientSegmentCommandHandler(IClientSegmentRepository repository) : IRequestHandler<CreateClientSegmentCommand, ClientSegmentDto>
{
    public async Task<ClientSegmentDto> Handle(CreateClientSegmentCommand request, CancellationToken cancellationToken)
    {
        var entity = new ClientSegment
        {
            Id = Guid.NewGuid(),
            SegmentName = request.Dto.SegmentName,
            SegmentPriority = request.Dto.SegmentPriority,
            PrimaryColor = request.Dto.PrimaryColor,
            SecondaryColor = request.Dto.SecondaryColor,
            IsActive = request.Dto.IsActive,
            IsDefault = request.Dto.IsDefault,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        return new ClientSegmentDto { Id = entity.Id, SegmentName = entity.SegmentName, SegmentPriority = entity.SegmentPriority, IsActive = entity.IsActive, IsDefault = entity.IsDefault, PrimaryColor = entity.PrimaryColor, SecondaryColor = entity.SecondaryColor, CreatedAt = entity.CreatedAt, UpdatedAt = null };
    }
}
