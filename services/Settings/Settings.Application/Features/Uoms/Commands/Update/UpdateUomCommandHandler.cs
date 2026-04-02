using MediatR;
using Settings.Application.DTOs.Uom;
using Settings.Application.Features.Uoms;
using Settings.Domain.AggregatesModel.UomAggregate;

namespace Settings.Application.Features.Uoms.Commands.Update;

public sealed class UpdateUomCommandHandler(IUomRepository repository) : IRequestHandler<UpdateUomCommand, UomDto?>
{
    public async Task<UomDto?> Handle(UpdateUomCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        return UomMapper.MapToDto(entity);
    }
}
