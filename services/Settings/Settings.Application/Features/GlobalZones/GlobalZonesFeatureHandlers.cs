using MediatR;
using Settings.Application.DTOs.GlobalZone;
using Settings.Domain.AggregatesModel.GlobalZoneAggregate;
using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.Application.Features.GlobalZones;

public sealed record GetAllGlobalZonesQuery(EntityStatus? Status = null) : IRequest<IReadOnlyList<GlobalZoneDto>>;
public sealed class GetAllGlobalZonesQueryHandler(IGlobalZoneRepository repository) : IRequestHandler<GetAllGlobalZonesQuery, IReadOnlyList<GlobalZoneDto>>
{
    public async Task<IReadOnlyList<GlobalZoneDto>> Handle(GetAllGlobalZonesQuery request, CancellationToken ct)
    {
        var list = request.Status.HasValue
            ? await repository.GetByStatusAsync(request.Status.Value, ct)
            : await repository.GetAllAsync(ct);
        return list.Select(e => new GlobalZoneDto(
            e.Id, e.Code, e.Name, e.LocalName, e.CountryId, null,
            e.Status.ToString(), e.CreatedAt, e.UpdatedAt)).ToList();
    }
}

public sealed record GetGlobalZoneByIdQuery(Guid Id) : IRequest<GlobalZoneDto?>;
public sealed class GetGlobalZoneByIdQueryHandler(IGlobalZoneRepository repository) : IRequestHandler<GetGlobalZoneByIdQuery, GlobalZoneDto?>
{
    public async Task<GlobalZoneDto?> Handle(GetGlobalZoneByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        if (e == null) return null;
        return new GlobalZoneDto(
            e.Id, e.Code, e.Name, e.LocalName, e.CountryId, null,
            e.Status.ToString(), e.CreatedAt, e.UpdatedAt);
    }
}

public sealed record CreateGlobalZoneCommand(CreateGlobalZoneDto Dto) : IRequest<GlobalZoneDto>;
public sealed class CreateGlobalZoneCommandHandler(IGlobalZoneRepository repository) : IRequestHandler<CreateGlobalZoneCommand, GlobalZoneDto>
{
    public async Task<GlobalZoneDto> Handle(CreateGlobalZoneCommand request, CancellationToken ct)
    {
        var status = Enum.TryParse<EntityStatus>(request.Dto.Status, true, out var s) ? s : EntityStatus.Active;
        var entity = new GlobalZone
        {
            Id = Guid.NewGuid(),
            Code = request.Dto.Code,
            Name = request.Dto.Name,
            LocalName = request.Dto.LocalName,
            CountryId = request.Dto.CountryId,
            Status = status,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, ct);
        return new GlobalZoneDto(
            entity.Id, entity.Code, entity.Name, entity.LocalName, entity.CountryId, null,
            entity.Status.ToString(), entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record UpdateGlobalZoneCommand(Guid Id, UpdateGlobalZoneDto Dto) : IRequest<GlobalZoneDto?>;
public sealed class UpdateGlobalZoneCommandHandler(IGlobalZoneRepository repository) : IRequestHandler<UpdateGlobalZoneCommand, GlobalZoneDto?>
{
    public async Task<GlobalZoneDto?> Handle(UpdateGlobalZoneCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        
        var status = Enum.TryParse<EntityStatus>(request.Dto.Status, true, out var s) ? s : entity.Status;
        entity.Code = request.Dto.Code;
        entity.Name = request.Dto.Name;
        entity.LocalName = request.Dto.LocalName;
        entity.CountryId = request.Dto.CountryId;
        entity.Status = status;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, ct);
        
        return new GlobalZoneDto(
            entity.Id, entity.Code, entity.Name, entity.LocalName, entity.CountryId, null,
            entity.Status.ToString(), entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record DeleteGlobalZoneCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteGlobalZoneCommandHandler(IGlobalZoneRepository repository) : IRequestHandler<DeleteGlobalZoneCommand, bool>
{
    public async Task<bool> Handle(DeleteGlobalZoneCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        entity.Status = EntityStatus.Deleted;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, ct);
        return true;
    }
}
