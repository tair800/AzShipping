using MediatR;
using Settings.Application.DTOs.Template;
using Settings.Domain.AggregatesModel.TemplateAggregate;

namespace Settings.Application.Features.Templates;

public sealed record GetAllTemplatesQuery : IRequest<IReadOnlyList<TemplateDto>>;
public sealed class GetAllTemplatesQueryHandler(ITemplateRepository repository) : IRequestHandler<GetAllTemplatesQuery, IReadOnlyList<TemplateDto>>
{
    public async Task<IReadOnlyList<TemplateDto>> Handle(GetAllTemplatesQuery request, CancellationToken ct)
    {
        var list = await repository.GetAllAsync(ct);
        return list.Select(e => new TemplateDto(e.Id, e.Name, e.IsActive, e.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), e.CreatedAt, e.UpdatedAt)).ToList();
    }
}

public sealed record GetTemplateByIdQuery(Guid Id) : IRequest<TemplateDto?>;
public sealed class GetTemplateByIdQueryHandler(ITemplateRepository repository) : IRequestHandler<GetTemplateByIdQuery, TemplateDto?>
{
    public async Task<TemplateDto?> Handle(GetTemplateByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        if (e == null) return null;
        return new TemplateDto(e.Id, e.Name, e.IsActive, e.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), e.CreatedAt, e.UpdatedAt);
    }
}

public sealed record CreateTemplateCommand(CreateTemplateDto Dto) : IRequest<TemplateDto>;
public sealed class CreateTemplateCommandHandler(ITemplateRepository repository) : IRequestHandler<CreateTemplateCommand, TemplateDto>
{
    public async Task<TemplateDto> Handle(CreateTemplateCommand request, CancellationToken ct)
    {
        var entity = new Template { Id = Guid.NewGuid(), Name = request.Dto.Name, IsActive = request.Dto.IsActive, CreatedAt = DateTime.UtcNow };
        if (request.Dto.Translations != null)
            foreach (var (code, name) in request.Dto.Translations)
                entity.Translations.Add(new TemplateTranslation { Id = Guid.NewGuid(), TemplateId = entity.Id, LanguageCode = code, Name = name });
        await repository.AddAsync(entity, ct);
        return new TemplateDto(entity.Id, entity.Name, entity.IsActive, entity.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record UpdateTemplateCommand(Guid Id, UpdateTemplateDto Dto) : IRequest<TemplateDto?>;
public sealed class UpdateTemplateCommandHandler(ITemplateRepository repository) : IRequestHandler<UpdateTemplateCommand, TemplateDto?>
{
    public async Task<TemplateDto?> Handle(UpdateTemplateCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        entity.Translations.Clear();
        if (request.Dto.Translations != null)
            foreach (var (code, name) in request.Dto.Translations)
                entity.Translations.Add(new TemplateTranslation { Id = Guid.NewGuid(), TemplateId = entity.Id, LanguageCode = code, Name = name });
        await repository.UpdateAsync(entity, ct);
        return new TemplateDto(entity.Id, entity.Name, entity.IsActive, entity.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record DeleteTemplateCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteTemplateCommandHandler(ITemplateRepository repository) : IRequestHandler<DeleteTemplateCommand, bool>
{
    public async Task<bool> Handle(DeleteTemplateCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, ct);
        return true;
    }
}
