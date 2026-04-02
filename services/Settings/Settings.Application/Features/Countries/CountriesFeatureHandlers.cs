using MediatR;
using Settings.Application.DTOs.Country;
using Settings.Domain.AggregatesModel.CountryAggregate;
using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.Application.Features.Countries;

public sealed record GetAllCountriesQuery(EntityStatus? Status = null) : IRequest<IReadOnlyList<CountryDto>>;
public sealed class GetAllCountriesQueryHandler(ICountryRepository repository) : IRequestHandler<GetAllCountriesQuery, IReadOnlyList<CountryDto>>
{
    public async Task<IReadOnlyList<CountryDto>> Handle(GetAllCountriesQuery request, CancellationToken ct)
    {
        var list = request.Status.HasValue
            ? await repository.GetByStatusAsync(request.Status.Value, ct)
            : await repository.GetAllAsync(ct);
        return list.Select(MapToDto).ToList();
    }
    
    private static CountryDto MapToDto(Country e) => new(
        e.Id, e.IsoCode, e.Name, e.LocalName, e.IsStateRequired, e.HasCities,
        e.Status.ToString(), e.Notes,
        e.CountryGlobalZones.Select(cg => new GlobalZoneRef(cg.GlobalZone.Id, cg.GlobalZone.Code, cg.GlobalZone.Name)).ToList(),
        e.CreatedAt, e.UpdatedAt);
}

public sealed record GetCountryByIdQuery(Guid Id) : IRequest<CountryDto?>;
public sealed class GetCountryByIdQueryHandler(ICountryRepository repository) : IRequestHandler<GetCountryByIdQuery, CountryDto?>
{
    public async Task<CountryDto?> Handle(GetCountryByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        if (e == null) return null;
        return new CountryDto(
            e.Id, e.IsoCode, e.Name, e.LocalName, e.IsStateRequired, e.HasCities,
            e.Status.ToString(), e.Notes,
            e.CountryGlobalZones.Select(cg => new GlobalZoneRef(cg.GlobalZone.Id, cg.GlobalZone.Code, cg.GlobalZone.Name)).ToList(),
            e.CreatedAt, e.UpdatedAt);
    }
}

public sealed record CreateCountryCommand(CreateCountryDto Dto) : IRequest<CountryDto>;
public sealed class CreateCountryCommandHandler(ICountryRepository repository) : IRequestHandler<CreateCountryCommand, CountryDto>
{
    public async Task<CountryDto> Handle(CreateCountryCommand request, CancellationToken ct)
    {
        var status = Enum.TryParse<EntityStatus>(request.Dto.Status, true, out var s) ? s : EntityStatus.Active;
        var entity = new Country
        {
            Id = Guid.NewGuid(),
            IsoCode = request.Dto.IsoCode,
            Name = request.Dto.Name,
            LocalName = request.Dto.LocalName,
            IsStateRequired = request.Dto.IsStateRequired,
            HasCities = request.Dto.HasCities,
            Status = status,
            Notes = request.Dto.Notes,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, ct);
        
        if (request.Dto.GlobalZoneIds?.Any() == true)
            await repository.SetGlobalZonesAsync(entity.Id, request.Dto.GlobalZoneIds, ct);
        
        var created = await repository.GetByIdAsync(entity.Id, ct);
        return new CountryDto(
            created!.Id, created.IsoCode, created.Name, created.LocalName, created.IsStateRequired, created.HasCities,
            created.Status.ToString(), created.Notes,
            created.CountryGlobalZones.Select(cg => new GlobalZoneRef(cg.GlobalZone.Id, cg.GlobalZone.Code, cg.GlobalZone.Name)).ToList(),
            created.CreatedAt, created.UpdatedAt);
    }
}

public sealed record UpdateCountryCommand(Guid Id, UpdateCountryDto Dto) : IRequest<CountryDto?>;
public sealed class UpdateCountryCommandHandler(ICountryRepository repository) : IRequestHandler<UpdateCountryCommand, CountryDto?>
{
    public async Task<CountryDto?> Handle(UpdateCountryCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        
        var status = Enum.TryParse<EntityStatus>(request.Dto.Status, true, out var s) ? s : entity.Status;
        entity.IsoCode = request.Dto.IsoCode;
        entity.Name = request.Dto.Name;
        entity.LocalName = request.Dto.LocalName;
        entity.IsStateRequired = request.Dto.IsStateRequired;
        entity.HasCities = request.Dto.HasCities;
        entity.Status = status;
        entity.Notes = request.Dto.Notes;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, ct);
        
        if (request.Dto.GlobalZoneIds != null)
            await repository.SetGlobalZonesAsync(entity.Id, request.Dto.GlobalZoneIds, ct);
        
        var updated = await repository.GetByIdAsync(entity.Id, ct);
        return new CountryDto(
            updated!.Id, updated.IsoCode, updated.Name, updated.LocalName, updated.IsStateRequired, updated.HasCities,
            updated.Status.ToString(), updated.Notes,
            updated.CountryGlobalZones.Select(cg => new GlobalZoneRef(cg.GlobalZone.Id, cg.GlobalZone.Code, cg.GlobalZone.Name)).ToList(),
            updated.CreatedAt, updated.UpdatedAt);
    }
}

public sealed record DeleteCountryCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteCountryCommandHandler(ICountryRepository repository) : IRequestHandler<DeleteCountryCommand, bool>
{
    public async Task<bool> Handle(DeleteCountryCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        entity.Status = EntityStatus.Deleted;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, ct);
        return true;
    }
}
