using General.Application.DTOs.Vas;
using General.Application.Features.Vas;
using General.Application.Services;
using General.Domain.AggregatesModel.VasAggregate;
using MediatR;

namespace General.Application.Features.Vas.Commands.Update;

public class UpdateVasCommandHandler(IVasRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<UpdateVasCommand, VasDto?>
{
    public async Task<VasDto?> Handle(UpdateVasCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        var dto = request.Dto;
        entity.Code = dto.Code;
        entity.Name = dto.Name;
        entity.OverWidth = dto.OverWidth;
        entity.OverHeight = dto.OverHeight;
        entity.OverWeight = dto.OverWeight;
        entity.IsMandatory = dto.IsMandatory;
        entity.ExecutionPlace = dto.ExecutionPlace;
        entity.Uom = dto.Uom;
        entity.IsAir = dto.IsAir;
        entity.IsSea = dto.IsSea;
        entity.IsRoad = dto.IsRoad;
        entity.IsRail = dto.IsRail;
        entity.Notes = dto.Notes;
        entity.IsActive = dto.IsActive;
        entity.Amount = dto.Amount;
        entity.CurrencyId = dto.CurrencyId;
        entity.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(entity, cancellationToken);
        var updated = await repository.GetByIdAsync(request.Id, cancellationToken);
        var result = VasMapper.MapToDto(updated!);
        await actionLogClient.LogAsync("Vas updated", $"vas: {entity.Name} • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }
}
