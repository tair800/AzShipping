using MediatR;
using Settings.Application.DTOs.Packaging;
using Settings.Domain.AggregatesModel.PackagingAggregate;

namespace Settings.Application.Features.Packagings;

public sealed record GetAllPackagingsQuery : IRequest<IReadOnlyList<PackagingDto>>;
public sealed class GetAllPackagingsQueryHandler(IPackagingRepository repository) : IRequestHandler<GetAllPackagingsQuery, IReadOnlyList<PackagingDto>>
{
    public async Task<IReadOnlyList<PackagingDto>> Handle(GetAllPackagingsQuery request, CancellationToken ct)
    {
        var list = await repository.GetAllAsync(ct);
        return list.Select(e => new PackagingDto(e.Id, e.Name, e.IsActive, e.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), e.CreatedAt, e.UpdatedAt)).ToList();
    }
}

public sealed record GetPackagingByIdQuery(Guid Id) : IRequest<PackagingDto?>;
public sealed class GetPackagingByIdQueryHandler(IPackagingRepository repository) : IRequestHandler<GetPackagingByIdQuery, PackagingDto?>
{
    public async Task<PackagingDto?> Handle(GetPackagingByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        if (e == null) return null;
        return new PackagingDto(e.Id, e.Name, e.IsActive, e.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), e.CreatedAt, e.UpdatedAt);
    }
}

public sealed record CreatePackagingCommand(CreatePackagingDto Dto) : IRequest<PackagingDto>;
public sealed class CreatePackagingCommandHandler(IPackagingRepository repository) : IRequestHandler<CreatePackagingCommand, PackagingDto>
{
    public async Task<PackagingDto> Handle(CreatePackagingCommand request, CancellationToken ct)
    {
        var entity = new Packaging { Id = Guid.NewGuid(), Name = request.Dto.Name, IsActive = request.Dto.IsActive, CreatedAt = DateTime.UtcNow };
        if (request.Dto.Translations != null)
            foreach (var (code, name) in request.Dto.Translations)
                entity.Translations.Add(new PackagingTranslation { Id = Guid.NewGuid(), PackagingId = entity.Id, LanguageCode = code, Name = name });
        await repository.AddAsync(entity, ct);
        return new PackagingDto(entity.Id, entity.Name, entity.IsActive, entity.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record UpdatePackagingCommand(Guid Id, UpdatePackagingDto Dto) : IRequest<PackagingDto?>;
public sealed class UpdatePackagingCommandHandler(IPackagingRepository repository) : IRequestHandler<UpdatePackagingCommand, PackagingDto?>
{
    public async Task<PackagingDto?> Handle(UpdatePackagingCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        entity.Name = request.Dto.Name; entity.IsActive = request.Dto.IsActive; entity.UpdatedAt = DateTime.UtcNow;
        entity.Translations.Clear();
        if (request.Dto.Translations != null)
            foreach (var (code, name) in request.Dto.Translations)
                entity.Translations.Add(new PackagingTranslation { Id = Guid.NewGuid(), PackagingId = entity.Id, LanguageCode = code, Name = name });
        await repository.UpdateAsync(entity, ct);
        return new PackagingDto(entity.Id, entity.Name, entity.IsActive, entity.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record DeletePackagingCommand(Guid Id) : IRequest<bool>;
public sealed class DeletePackagingCommandHandler(IPackagingRepository repository) : IRequestHandler<DeletePackagingCommand, bool>
{
    public async Task<bool> Handle(DeletePackagingCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, ct);
        return true;
    }
}
