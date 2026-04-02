using MediatR;
using Settings.Application.DTOs.City;
using Settings.Domain.AggregatesModel.CityAggregate;
using Settings.Domain.AggregatesModel.StateAggregate; // For EntityStatus enum

namespace Settings.Application.Features.Cities;

public sealed record GetAllCitiesQuery(EntityStatus? Status = null) : IRequest<IReadOnlyList<CityDto>>;
public sealed class GetAllCitiesQueryHandler(ICityRepository repository) : IRequestHandler<GetAllCitiesQuery, IReadOnlyList<CityDto>>
{
    public async Task<IReadOnlyList<CityDto>> Handle(GetAllCitiesQuery request, CancellationToken ct)
    {
        var list = request.Status.HasValue
            ? await repository.GetByStatusAsync(request.Status.Value, ct)
            : await repository.GetAllAsync(ct);
        return list.Select(e => new CityDto(
            e.Id, e.Code, e.Name, e.LocalName, e.StateId,
            e.State?.Name, e.State?.Country?.Name,
            e.ZipCode, e.Status.ToString(), e.Notes, e.CreatedAt, e.UpdatedAt)).ToList();
    }
}

public sealed record GetCityByIdQuery(Guid Id) : IRequest<CityDto?>;
public sealed class GetCityByIdQueryHandler(ICityRepository repository) : IRequestHandler<GetCityByIdQuery, CityDto?>
{
    public async Task<CityDto?> Handle(GetCityByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        if (e == null) return null;
        return new CityDto(
            e.Id, e.Code, e.Name, e.LocalName, e.StateId,
            e.State?.Name, e.State?.Country?.Name,
            e.ZipCode, e.Status.ToString(), e.Notes, e.CreatedAt, e.UpdatedAt);
    }
}

public sealed record CreateCityCommand(CreateCityDto Dto) : IRequest<CityDto>;
public sealed class CreateCityCommandHandler(ICityRepository repository) : IRequestHandler<CreateCityCommand, CityDto>
{
    public async Task<CityDto> Handle(CreateCityCommand request, CancellationToken ct)
    {
        var status = Enum.TryParse<EntityStatus>(request.Dto.Status, true, out var s) ? s : EntityStatus.Active;
        var entity = new City
        {
            Id = Guid.NewGuid(),
            Code = request.Dto.Code,
            Name = request.Dto.Name,
            LocalName = request.Dto.LocalName,
            StateId = request.Dto.StateId,
            ZipCode = request.Dto.ZipCode,
            Status = status,
            Notes = request.Dto.Notes,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, ct);
        var created = await repository.GetByIdAsync(entity.Id, ct);
        return new CityDto(
            created!.Id, created.Code, created.Name, created.LocalName, created.StateId,
            created.State?.Name, created.State?.Country?.Name,
            created.ZipCode, created.Status.ToString(), created.Notes, created.CreatedAt, created.UpdatedAt);
    }
}

public sealed record UpdateCityCommand(Guid Id, UpdateCityDto Dto) : IRequest<CityDto?>;
public sealed class UpdateCityCommandHandler(ICityRepository repository) : IRequestHandler<UpdateCityCommand, CityDto?>
{
    public async Task<CityDto?> Handle(UpdateCityCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        
        var status = Enum.TryParse<EntityStatus>(request.Dto.Status, true, out var s) ? s : entity.Status;
        entity.Code = request.Dto.Code;
        entity.Name = request.Dto.Name;
        entity.LocalName = request.Dto.LocalName;
        entity.StateId = request.Dto.StateId;
        entity.ZipCode = request.Dto.ZipCode;
        entity.Status = status;
        entity.Notes = request.Dto.Notes;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, ct);
        var updated = await repository.GetByIdAsync(entity.Id, ct);
        return new CityDto(
            updated!.Id, updated.Code, updated.Name, updated.LocalName, updated.StateId,
            updated.State?.Name, updated.State?.Country?.Name,
            updated.ZipCode, updated.Status.ToString(), updated.Notes, updated.CreatedAt, updated.UpdatedAt);
    }
}

public sealed record DeleteCityCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteCityCommandHandler(ICityRepository repository) : IRequestHandler<DeleteCityCommand, bool>
{
    public async Task<bool> Handle(DeleteCityCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        entity.Status = EntityStatus.Deleted;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, ct);
        return true;
    }
}
