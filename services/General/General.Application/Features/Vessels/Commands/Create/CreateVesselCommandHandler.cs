using General.Application.DTOs.Vessel;
using General.Application.Features.Vessels;
using General.Application.Services;
using General.Domain.AggregatesModel.VesselAggregate;
using MediatR;

namespace General.Application.Features.Vessels.Commands.Create;

public class CreateVesselCommandHandler(IVesselRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<CreateVesselCommand, VesselDto>
{
    public async Task<VesselDto> Handle(CreateVesselCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new Vessel
        {
            Id = Guid.NewGuid(),
            Code = dto.Code,
            Name = dto.Name,
            ImoCode = dto.ImoCode,
            LocalName = dto.LocalName,
            CountryId = dto.CountryId,
            Notes = dto.Notes,
            IsActive = dto.IsActive,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        var result = VesselMapper.MapToDto(created!);
        await actionLogClient.LogAsync("Vessel created", $"vessel: {entity.Name} • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }
}
