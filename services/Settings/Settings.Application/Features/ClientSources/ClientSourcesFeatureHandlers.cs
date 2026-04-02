using MediatR;
using Settings.Application.DTOs.ClientSource;
using Settings.Domain.AggregatesModel.ClientSourceAggregate;

namespace Settings.Application.Features.ClientSources;

public sealed record GetAllClientSourcesQuery : IRequest<IReadOnlyList<ClientSourceDto>>;
public sealed class GetAllClientSourcesQueryHandler(IClientSourceRepository repository) : IRequestHandler<GetAllClientSourcesQuery, IReadOnlyList<ClientSourceDto>>
{
    public async Task<IReadOnlyList<ClientSourceDto>> Handle(GetAllClientSourcesQuery request, CancellationToken ct)
    {
        var list = await repository.GetAllAsync(ct);
        return list.Select(e => new ClientSourceDto(e.Id, e.Name, e.IsActive, e.CreatedAt, e.UpdatedAt)).ToList();
    }
}

public sealed record GetClientSourceByIdQuery(Guid Id) : IRequest<ClientSourceDto?>;
public sealed class GetClientSourceByIdQueryHandler(IClientSourceRepository repository) : IRequestHandler<GetClientSourceByIdQuery, ClientSourceDto?>
{
    public async Task<ClientSourceDto?> Handle(GetClientSourceByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        if (e == null) return null;
        return new ClientSourceDto(e.Id, e.Name, e.IsActive, e.CreatedAt, e.UpdatedAt);
    }
}

public sealed record CreateClientSourceCommand(CreateClientSourceDto Dto) : IRequest<ClientSourceDto>;
public sealed class CreateClientSourceCommandHandler(IClientSourceRepository repository) : IRequestHandler<CreateClientSourceCommand, ClientSourceDto>
{
    public async Task<ClientSourceDto> Handle(CreateClientSourceCommand request, CancellationToken ct)
    {
        var entity = new ClientSource
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, ct);
        return new ClientSourceDto(entity.Id, entity.Name, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record UpdateClientSourceCommand(Guid Id, UpdateClientSourceDto Dto) : IRequest<ClientSourceDto?>;
public sealed class UpdateClientSourceCommandHandler(IClientSourceRepository repository) : IRequestHandler<UpdateClientSourceCommand, ClientSourceDto?>
{
    public async Task<ClientSourceDto?> Handle(UpdateClientSourceCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, ct);
        return new ClientSourceDto(entity.Id, entity.Name, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record DeleteClientSourceCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteClientSourceCommandHandler(IClientSourceRepository repository) : IRequestHandler<DeleteClientSourceCommand, bool>
{
    public async Task<bool> Handle(DeleteClientSourceCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, ct);
        return true;
    }
}
