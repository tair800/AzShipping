using General.Application.DTOs.Incoterm;
using General.Application.Features.Incoterms;
using General.Application.Services;
using General.Domain.AggregatesModel.IncotermAggregate;
using MediatR;

namespace General.Application.Features.Incoterms.Commands.Update;

public class UpdateIncotermCommandHandler(IIncotermRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<UpdateIncotermCommand, IncotermDto?>
{
    public async Task<IncotermDto?> Handle(UpdateIncotermCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;

        var dto = request.Dto;
        entity.Code = dto.Code;
        entity.Name = dto.Name;
        entity.LocalName = dto.LocalName;
        entity.Freight = dto.Freight;
        entity.OtherCharges = dto.OtherCharges;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(entity, cancellationToken);
        var updated = await repository.GetByIdAsync(request.Id, cancellationToken);
        var result = IncotermMapper.MapToDto(updated!);
        await actionLogClient.LogAsync("Incoterm updated", $"incoterm: {entity.Code} ({entity.Name}) • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }
}
