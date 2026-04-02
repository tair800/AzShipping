using General.Application.DTOs.Incoterm;
using General.Application.Features.Incoterms;
using General.Application.Services;
using General.Domain.AggregatesModel.IncotermAggregate;
using MediatR;

namespace General.Application.Features.Incoterms.Commands.Create;

public class CreateIncotermCommandHandler(IIncotermRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<CreateIncotermCommand, IncotermDto>
{
    public async Task<IncotermDto> Handle(CreateIncotermCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new Incoterm
        {
            Id = Guid.NewGuid(),
            Code = dto.Code,
            Name = dto.Name,
            LocalName = dto.LocalName,
            Freight = dto.Freight,
            OtherCharges = dto.OtherCharges,
            IsActive = dto.IsActive,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow
        };

        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        var result = IncotermMapper.MapToDto(created!);
        await actionLogClient.LogAsync("Incoterm created", $"incoterm: {entity.Code} ({entity.Name}) • id: {entity.Id}", null, null, cancellationToken);
        return result;
    }
}
