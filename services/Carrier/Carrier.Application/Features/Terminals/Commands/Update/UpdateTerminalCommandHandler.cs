using Carrier.Application.DTOs.Terminal;
using Carrier.Application.Features.Terminals;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.TerminalAggregate;
using MediatR;

namespace Carrier.Application.Features.Terminals.Commands.Update;

public sealed class UpdateTerminalCommandHandler(ITerminalRepository repository, IActionLogClient actionLogClient) : IRequestHandler<UpdateTerminalCommand, TerminalDto?>
{
    private static DateTime? ToUtc(DateTime? d) =>
        d == null ? null : d.Value.Kind == DateTimeKind.Utc ? d : DateTime.SpecifyKind(d.Value, DateTimeKind.Utc);

    public async Task<TerminalDto?> Handle(UpdateTerminalCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return null;

        var dto = request.Dto;
        existing.Name = dto.Name;
        existing.CountryId = dto.CountryId;
        existing.CityId = dto.CityId;
        existing.Address = dto.Address;
        existing.PostCode = dto.PostCode;
        existing.RailwayStation = dto.RailwayStation;
        existing.TransportTypeIds = TerminalMapper.ToTransportTypeIdsString(dto.TransportTypeIds);
        existing.Notes = dto.Notes;
        existing.IsDeactive = dto.IsDeactive;
        existing.DateOfCreation = ToUtc(dto.DateOfCreation);
        existing.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(existing, cancellationToken);
        var updated = await repository.GetByIdAsync(existing.Id, cancellationToken);
        await actionLogClient.LogAsync("Terminal updated", $"terminal: {existing.Name} • id: {existing.Id}", null, null, cancellationToken);
        return TerminalMapper.MapToDto(updated!);
    }
}
