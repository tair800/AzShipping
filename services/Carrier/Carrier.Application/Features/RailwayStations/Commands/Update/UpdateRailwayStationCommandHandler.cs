using Carrier.Application.DTOs.RailwayStation;
using Carrier.Application.Features.RailwayStations;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.RailwayStationAggregate;
using MediatR;

namespace Carrier.Application.Features.RailwayStations.Commands.Update;

public class UpdateRailwayStationCommandHandler(IRailwayStationRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<UpdateRailwayStationCommand, RailwayStationDto?>
{
    public async Task<RailwayStationDto?> Handle(UpdateRailwayStationCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return null;

        var dto = request.Dto;
        existing.Code = dto.Code;
        existing.Name = dto.Name;
        existing.Railway = dto.Railway;
        existing.LocalName = dto.LocalName;
        existing.Prefix = dto.Prefix;
        existing.Website = dto.Website;
        existing.VatNo = dto.VatNo;
        existing.Notes = dto.Notes;
        existing.IsActive = dto.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(existing, cancellationToken);
        var updated = await repository.GetByIdAsync(existing.Id, cancellationToken);
        await actionLogClient.LogAsync("Railway station updated", $"railway station: {existing.Name} • id: {existing.Id}", null, null, cancellationToken);
        return RailwayStationMapper.MapToDto(updated!);
    }
}
