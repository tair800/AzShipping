using MediatR;
using Settings.Application.DTOs.Uom;
using Settings.Application.Features.Uoms;
using Settings.Domain.AggregatesModel.UomAggregate;

namespace Settings.Application.Features.Uoms.Commands.Create;

public sealed class CreateUomCommandHandler(IUomRepository repository) : IRequestHandler<CreateUomCommand, UomDto>
{
    public async Task<UomDto> Handle(CreateUomCommand request, CancellationToken cancellationToken)
    {
        var entity = new Uom
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        return UomMapper.MapToDto(entity);
    }
}
