using Carrier.Application.DTOs.RailwayStation;
using Carrier.Application.Features.RailwayStations;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.RailwayStationAggregate;
using MediatR;

namespace Carrier.Application.Features.RailwayStations.Commands.Create;

public class CreateRailwayStationCommandHandler(IRailwayStationRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<CreateRailwayStationCommand, RailwayStationDto>
{
    public async Task<RailwayStationDto> Handle(CreateRailwayStationCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new RailwayStation
        {
            Id = Guid.NewGuid(),
            Code = dto.Code,
            Name = dto.Name,
            Railway = dto.Railway,
            LocalName = dto.LocalName,
            Prefix = dto.Prefix,
            Website = dto.Website,
            VatNo = dto.VatNo,
            Notes = dto.Notes,
            IsActive = dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        await actionLogClient.LogAsync("Railway station created", $"railway station: {entity.Name} • id: {entity.Id}", null, null, cancellationToken);
        return RailwayStationMapper.MapToDto(created!);
    }
}
