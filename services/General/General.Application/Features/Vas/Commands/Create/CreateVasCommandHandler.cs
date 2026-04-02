using General.Application.DTOs.Vas;
using General.Application.Features.Vas;
using General.Application.Services;
using General.Domain.AggregatesModel.VasAggregate;
using MediatR;

namespace General.Application.Features.Vas.Commands.Create;

public class CreateVasCommandHandler(IVasRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<CreateVasCommand, VasDto>
{
    public async Task<VasDto> Handle(CreateVasCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new General.Domain.AggregatesModel.VasAggregate.Vas
        {
            Id = Guid.NewGuid(),
            Code = dto.Code,
            Name = dto.Name,
            OverWidth = dto.OverWidth,
            OverHeight = dto.OverHeight,
            OverWeight = dto.OverWeight,
            IsMandatory = dto.IsMandatory,
            ExecutionPlace = dto.ExecutionPlace,
            Uom = dto.Uom,
            IsAir = dto.IsAir,
            IsSea = dto.IsSea,
            IsRoad = dto.IsRoad,
            IsRail = dto.IsRail,
            Notes = dto.Notes,
            IsActive = dto.IsActive,
            IsDeleted = false,
            Amount = dto.Amount,
            CurrencyId = dto.CurrencyId,
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        var result = VasMapper.MapToDto(created!);
        await actionLogClient.LogAsync("Vas created", $"vas: {entity.Name} • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }
}
