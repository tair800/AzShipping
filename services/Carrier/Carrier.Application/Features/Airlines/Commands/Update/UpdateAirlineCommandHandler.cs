using Carrier.Application.DTOs.Airline;
using Carrier.Application.Features.Airlines;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.AirlineAggregate;
using MediatR;

namespace Carrier.Application.Features.Airlines.Commands.Update;

public class UpdateAirlineCommandHandler(IAirlineRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<UpdateAirlineCommand, AirlineDto?>
{
    public async Task<AirlineDto?> Handle(UpdateAirlineCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return null;

        var dto = request.Dto;
        existing.Code = dto.Code;
        existing.Icao = dto.Icao;
        existing.Name = dto.Name;
        existing.LocalName = dto.LocalName;
        existing.Prefix = dto.Prefix;
        existing.Website = dto.Website;
        existing.VatNo = dto.VatNo;
        existing.Notes = dto.Notes;
        existing.IsActive = dto.IsActive;
        existing.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(existing, cancellationToken);
        var updated = await repository.GetByIdAsync(existing.Id, cancellationToken);
        await actionLogClient.LogAsync("Airline updated", $"airline: {existing.Name} • id: {existing.Id}", null, null, cancellationToken);
        return AirlineMapper.MapToDto(updated!);
    }
}
