using Carrier.Application.DTOs.Terminal;
using Carrier.Application.Features.Terminals;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.TerminalAggregate;
using MediatR;

namespace Carrier.Application.Features.Terminals.Commands.Create;

public sealed class CreateTerminalCommandHandler(ITerminalRepository repository, IActionLogClient actionLogClient) : IRequestHandler<CreateTerminalCommand, TerminalDto>
{
    private static DateTime? ToUtc(DateTime? d) =>
        d == null ? null : d.Value.Kind == DateTimeKind.Utc ? d : DateTime.SpecifyKind(d.Value, DateTimeKind.Utc);

    public async Task<TerminalDto> Handle(CreateTerminalCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new Terminal
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            CountryId = dto.CountryId,
            CityId = dto.CityId,
            Address = dto.Address,
            PostCode = dto.PostCode,
            RailwayStation = dto.RailwayStation,
            TransportTypeIds = TerminalMapper.ToTransportTypeIdsString(dto.TransportTypeIds),
            Notes = dto.Notes,
            IsDeactive = dto.IsDeactive,
            DateOfCreation = ToUtc(dto.DateOfCreation),
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        await actionLogClient.LogAsync("Terminal created", $"terminal: {entity.Name} • id: {entity.Id}", null, null, cancellationToken);
        return TerminalMapper.MapToDto(created!);
    }
}
