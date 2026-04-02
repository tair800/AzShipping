using MediatR;
using Settings.Application.DTOs.WayOfNegotiation;
using Settings.Domain.AggregatesModel.WayOfNegotiationAggregate;

namespace Settings.Application.Features.WayOfNegotiations;

public sealed record GetAllWayOfNegotiationsQuery : IRequest<IReadOnlyList<WayOfNegotiationDto>>;
public sealed class GetAllWayOfNegotiationsQueryHandler(IWayOfNegotiationRepository repository) : IRequestHandler<GetAllWayOfNegotiationsQuery, IReadOnlyList<WayOfNegotiationDto>>
{
    public async Task<IReadOnlyList<WayOfNegotiationDto>> Handle(GetAllWayOfNegotiationsQuery request, CancellationToken ct)
    {
        var list = await repository.GetAllAsync(ct);
        return list.Select(e => new WayOfNegotiationDto(e.Id, e.Name, e.IsActive, e.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), e.CreatedAt, e.UpdatedAt)).ToList();
    }
}

public sealed record GetWayOfNegotiationByIdQuery(Guid Id) : IRequest<WayOfNegotiationDto?>;
public sealed class GetWayOfNegotiationByIdQueryHandler(IWayOfNegotiationRepository repository) : IRequestHandler<GetWayOfNegotiationByIdQuery, WayOfNegotiationDto?>
{
    public async Task<WayOfNegotiationDto?> Handle(GetWayOfNegotiationByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        if (e == null) return null;
        return new WayOfNegotiationDto(e.Id, e.Name, e.IsActive, e.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), e.CreatedAt, e.UpdatedAt);
    }
}

public sealed record CreateWayOfNegotiationCommand(CreateWayOfNegotiationDto Dto) : IRequest<WayOfNegotiationDto>;
public sealed class CreateWayOfNegotiationCommandHandler(IWayOfNegotiationRepository repository) : IRequestHandler<CreateWayOfNegotiationCommand, WayOfNegotiationDto>
{
    public async Task<WayOfNegotiationDto> Handle(CreateWayOfNegotiationCommand request, CancellationToken ct)
    {
        var entity = new WayOfNegotiation { Id = Guid.NewGuid(), Name = request.Dto.Name, IsActive = request.Dto.IsActive, CreatedAt = DateTime.UtcNow };
        if (request.Dto.Translations != null)
            foreach (var (code, name) in request.Dto.Translations)
                entity.Translations.Add(new WayOfNegotiationTranslation { Id = Guid.NewGuid(), WayOfNegotiationId = entity.Id, LanguageCode = code, Name = name });
        await repository.AddAsync(entity, ct);
        return new WayOfNegotiationDto(entity.Id, entity.Name, entity.IsActive, entity.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record UpdateWayOfNegotiationCommand(Guid Id, UpdateWayOfNegotiationDto Dto) : IRequest<WayOfNegotiationDto?>;
public sealed class UpdateWayOfNegotiationCommandHandler(IWayOfNegotiationRepository repository) : IRequestHandler<UpdateWayOfNegotiationCommand, WayOfNegotiationDto?>
{
    public async Task<WayOfNegotiationDto?> Handle(UpdateWayOfNegotiationCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        entity.Name = request.Dto.Name; entity.IsActive = request.Dto.IsActive; entity.UpdatedAt = DateTime.UtcNow;
        entity.Translations.Clear();
        if (request.Dto.Translations != null)
            foreach (var (code, name) in request.Dto.Translations)
                entity.Translations.Add(new WayOfNegotiationTranslation { Id = Guid.NewGuid(), WayOfNegotiationId = entity.Id, LanguageCode = code, Name = name });
        await repository.UpdateAsync(entity, ct);
        return new WayOfNegotiationDto(entity.Id, entity.Name, entity.IsActive, entity.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record DeleteWayOfNegotiationCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteWayOfNegotiationCommandHandler(IWayOfNegotiationRepository repository) : IRequestHandler<DeleteWayOfNegotiationCommand, bool>
{
    public async Task<bool> Handle(DeleteWayOfNegotiationCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, ct);
        return true;
    }
}
