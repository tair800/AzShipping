using Carrier.Application.DTOs.Airline;
using Carrier.Application.Features.Airlines;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.AirlineAggregate;
using MediatR;

namespace Carrier.Application.Features.Airlines.Commands.Create;

public class CreateAirlineCommandHandler(IAirlineRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<CreateAirlineCommand, AirlineDto>
{
    public async Task<AirlineDto> Handle(CreateAirlineCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new Airline
        {
            Id = Guid.NewGuid(),
            Code = dto.Code,
            Icao = dto.Icao,
            Name = dto.Name,
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
        await actionLogClient.LogAsync("Airline created", $"airline: {entity.Name} • id: {entity.Id}", null, null, cancellationToken);
        return AirlineMapper.MapToDto(created!);
    }
}
