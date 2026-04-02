using MediatR;
using Settings.Application.DTOs.WorkerPost;
using Settings.Domain.AggregatesModel.WorkerPostAggregate;

namespace Settings.Application.Features.WorkerPosts;

public sealed record GetAllWorkerPostsQuery : IRequest<IReadOnlyList<WorkerPostDto>>;
public sealed class GetAllWorkerPostsQueryHandler(IWorkerPostRepository repository) : IRequestHandler<GetAllWorkerPostsQuery, IReadOnlyList<WorkerPostDto>>
{
    public async Task<IReadOnlyList<WorkerPostDto>> Handle(GetAllWorkerPostsQuery request, CancellationToken ct)
    {
        var list = await repository.GetAllAsync(ct);
        return list.Select(e => new WorkerPostDto(e.Id, e.Name, e.IsActive, e.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), e.CreatedAt, e.UpdatedAt)).ToList();
    }
}

public sealed record GetWorkerPostByIdQuery(Guid Id) : IRequest<WorkerPostDto?>;
public sealed class GetWorkerPostByIdQueryHandler(IWorkerPostRepository repository) : IRequestHandler<GetWorkerPostByIdQuery, WorkerPostDto?>
{
    public async Task<WorkerPostDto?> Handle(GetWorkerPostByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        if (e == null) return null;
        return new WorkerPostDto(e.Id, e.Name, e.IsActive, e.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), e.CreatedAt, e.UpdatedAt);
    }
}

public sealed record CreateWorkerPostCommand(CreateWorkerPostDto Dto) : IRequest<WorkerPostDto>;
public sealed class CreateWorkerPostCommandHandler(IWorkerPostRepository repository) : IRequestHandler<CreateWorkerPostCommand, WorkerPostDto>
{
    public async Task<WorkerPostDto> Handle(CreateWorkerPostCommand request, CancellationToken ct)
    {
        var entity = new WorkerPost { Id = Guid.NewGuid(), Name = request.Dto.Name, IsActive = request.Dto.IsActive, CreatedAt = DateTime.UtcNow };
        if (request.Dto.Translations != null)
            foreach (var (code, name) in request.Dto.Translations)
                entity.Translations.Add(new WorkerPostTranslation { Id = Guid.NewGuid(), WorkerPostId = entity.Id, LanguageCode = code, Name = name });
        await repository.AddAsync(entity, ct);
        return new WorkerPostDto(entity.Id, entity.Name, entity.IsActive, entity.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record UpdateWorkerPostCommand(Guid Id, UpdateWorkerPostDto Dto) : IRequest<WorkerPostDto?>;
public sealed class UpdateWorkerPostCommandHandler(IWorkerPostRepository repository) : IRequestHandler<UpdateWorkerPostCommand, WorkerPostDto?>
{
    public async Task<WorkerPostDto?> Handle(UpdateWorkerPostCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        entity.Name = request.Dto.Name; entity.IsActive = request.Dto.IsActive; entity.UpdatedAt = DateTime.UtcNow;
        entity.Translations.Clear();
        if (request.Dto.Translations != null)
            foreach (var (code, name) in request.Dto.Translations)
                entity.Translations.Add(new WorkerPostTranslation { Id = Guid.NewGuid(), WorkerPostId = entity.Id, LanguageCode = code, Name = name });
        await repository.UpdateAsync(entity, ct);
        return new WorkerPostDto(entity.Id, entity.Name, entity.IsActive, entity.Translations.ToDictionary(t => t.LanguageCode, t => t.Name), entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record DeleteWorkerPostCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteWorkerPostCommandHandler(IWorkerPostRepository repository) : IRequestHandler<DeleteWorkerPostCommand, bool>
{
    public async Task<bool> Handle(DeleteWorkerPostCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, ct);
        return true;
    }
}
