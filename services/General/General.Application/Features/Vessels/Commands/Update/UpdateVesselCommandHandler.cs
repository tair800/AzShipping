using General.Application.DTOs.Vessel;
using General.Application.Features.Vessels;
using General.Application.Services;
using General.Domain.AggregatesModel.VesselAggregate;
using MediatR;

namespace General.Application.Features.Vessels.Commands.Update;

public class UpdateVesselCommandHandler(IVesselRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<UpdateVesselCommand, VesselDto?>
{
    public async Task<VesselDto?> Handle(UpdateVesselCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        var dto = request.Dto;
        entity.Code = dto.Code;
        entity.Name = dto.Name;
        entity.ImoCode = dto.ImoCode;
        entity.LocalName = dto.LocalName;
        entity.CountryId = dto.CountryId;
        entity.Notes = dto.Notes;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(entity, cancellationToken);
        var updated = await repository.GetByIdAsync(request.Id, cancellationToken);
        var result = VesselMapper.MapToDto(updated!);
        await actionLogClient.LogAsync("Vessel updated", $"vessel: {entity.Name} • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }
}
