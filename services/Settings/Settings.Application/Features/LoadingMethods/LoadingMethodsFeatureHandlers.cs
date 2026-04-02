using MediatR;
using Settings.Application.DTOs.LoadingMethod;
using Settings.Domain.AggregatesModel.LoadingMethodAggregate;

namespace Settings.Application.Features.LoadingMethods;

public sealed record GetAllLoadingMethodsQuery : IRequest<IReadOnlyList<LoadingMethodDto>>;
public sealed class GetAllLoadingMethodsQueryHandler(ILoadingMethodRepository repository) : IRequestHandler<GetAllLoadingMethodsQuery, IReadOnlyList<LoadingMethodDto>>
{
    public async Task<IReadOnlyList<LoadingMethodDto>> Handle(GetAllLoadingMethodsQuery request, CancellationToken ct)
    {
        var list = await repository.GetAllAsync(ct);
        return list.Select(e => new LoadingMethodDto(e.Id, e.Name, e.IsActive, e.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), e.CreatedAt, e.UpdatedAt)).ToList();
    }
}

public sealed record GetLoadingMethodByIdQuery(Guid Id) : IRequest<LoadingMethodDto?>;
public sealed class GetLoadingMethodByIdQueryHandler(ILoadingMethodRepository repository) : IRequestHandler<GetLoadingMethodByIdQuery, LoadingMethodDto?>
{
    public async Task<LoadingMethodDto?> Handle(GetLoadingMethodByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        if (e == null) return null;
        return new LoadingMethodDto(e.Id, e.Name, e.IsActive, e.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), e.CreatedAt, e.UpdatedAt);
    }
}

public sealed record CreateLoadingMethodCommand(CreateLoadingMethodDto Dto) : IRequest<LoadingMethodDto>;
public sealed class CreateLoadingMethodCommandHandler(ILoadingMethodRepository repository) : IRequestHandler<CreateLoadingMethodCommand, LoadingMethodDto>
{
    public async Task<LoadingMethodDto> Handle(CreateLoadingMethodCommand request, CancellationToken ct)
    {
        var entity = new LoadingMethod { Id = Guid.NewGuid(), Name = request.Dto.Name, IsActive = request.Dto.IsActive, CreatedAt = DateTime.UtcNow };
        if (request.Dto.Translations != null)
            foreach (var (code, name) in request.Dto.Translations)
                entity.Translations.Add(new LoadingMethodTranslation { Id = Guid.NewGuid(), LoadingMethodId = entity.Id, LanguageCode = code, Name = name });
        await repository.AddAsync(entity, ct);
        return new LoadingMethodDto(entity.Id, entity.Name, entity.IsActive, entity.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record UpdateLoadingMethodCommand(Guid Id, UpdateLoadingMethodDto Dto) : IRequest<LoadingMethodDto?>;
public sealed class UpdateLoadingMethodCommandHandler(ILoadingMethodRepository repository) : IRequestHandler<UpdateLoadingMethodCommand, LoadingMethodDto?>
{
    public async Task<LoadingMethodDto?> Handle(UpdateLoadingMethodCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        entity.Name = request.Dto.Name; entity.IsActive = request.Dto.IsActive; entity.UpdatedAt = DateTime.UtcNow;
        entity.Translations.Clear();
        if (request.Dto.Translations != null)
            foreach (var (code, name) in request.Dto.Translations)
                entity.Translations.Add(new LoadingMethodTranslation { Id = Guid.NewGuid(), LoadingMethodId = entity.Id, LanguageCode = code, Name = name });
        await repository.UpdateAsync(entity, ct);
        return new LoadingMethodDto(entity.Id, entity.Name, entity.IsActive, entity.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record DeleteLoadingMethodCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteLoadingMethodCommandHandler(ILoadingMethodRepository repository) : IRequestHandler<DeleteLoadingMethodCommand, bool>
{
    public async Task<bool> Handle(DeleteLoadingMethodCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, ct);
        return true;
    }
}
