using MediatR;
using Settings.Application.DTOs.State;
using Settings.Domain.AggregatesModel.StateAggregate;

namespace Settings.Application.Features.States;

public sealed record GetAllStatesQuery(EntityStatus? Status = null) : IRequest<IReadOnlyList<StateDto>>;
public sealed class GetAllStatesQueryHandler(IStateRepository repository) : IRequestHandler<GetAllStatesQuery, IReadOnlyList<StateDto>>
{
    public async Task<IReadOnlyList<StateDto>> Handle(GetAllStatesQuery request, CancellationToken ct)
    {
        var list = request.Status.HasValue
            ? await repository.GetByStatusAsync(request.Status.Value, ct)
            : await repository.GetAllAsync(ct);
        return list.Select(e => new StateDto(e.Id, e.Code, e.Name, e.LocalName, e.CountryId, e.Country?.Name, e.Status.ToString(), e.Notes, e.CreatedAt, e.UpdatedAt)).ToList();
    }
}

public sealed record GetStateByIdQuery(Guid Id) : IRequest<StateDto?>;
public sealed class GetStateByIdQueryHandler(IStateRepository repository) : IRequestHandler<GetStateByIdQuery, StateDto?>
{
    public async Task<StateDto?> Handle(GetStateByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        if (e == null) return null;
        return new StateDto(e.Id, e.Code, e.Name, e.LocalName, e.CountryId, e.Country?.Name, e.Status.ToString(), e.Notes, e.CreatedAt, e.UpdatedAt);
    }
}

public sealed record CreateStateCommand(CreateStateDto Dto) : IRequest<StateDto>;
public sealed class CreateStateCommandHandler(IStateRepository repository) : IRequestHandler<CreateStateCommand, StateDto>
{
    public async Task<StateDto> Handle(CreateStateCommand request, CancellationToken ct)
    {
        var status = Enum.TryParse<EntityStatus>(request.Dto.Status, true, out var s) ? s : EntityStatus.Active;
        var entity = new State
        {
            Id = Guid.NewGuid(),
            Code = request.Dto.Code,
            Name = request.Dto.Name,
            LocalName = request.Dto.LocalName,
            CountryId = request.Dto.CountryId,
            Status = status,
            Notes = request.Dto.Notes,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, ct);
        var created = await repository.GetByIdAsync(entity.Id, ct);
        return new StateDto(created!.Id, created.Code, created.Name, created.LocalName, created.CountryId, created.Country?.Name, created.Status.ToString(), created.Notes, created.CreatedAt, created.UpdatedAt);
    }
}

public sealed record UpdateStateCommand(Guid Id, UpdateStateDto Dto) : IRequest<StateDto?>;
public sealed class UpdateStateCommandHandler(IStateRepository repository) : IRequestHandler<UpdateStateCommand, StateDto?>
{
    public async Task<StateDto?> Handle(UpdateStateCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        var status = Enum.TryParse<EntityStatus>(request.Dto.Status, true, out var s) ? s : entity.Status;
        entity.Code = request.Dto.Code;
        entity.Name = request.Dto.Name;
        entity.LocalName = request.Dto.LocalName;
        entity.CountryId = request.Dto.CountryId;
        entity.Status = status;
        entity.Notes = request.Dto.Notes;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, ct);
        var updated = await repository.GetByIdAsync(entity.Id, ct);
        return new StateDto(updated!.Id, updated.Code, updated.Name, updated.LocalName, updated.CountryId, updated.Country?.Name, updated.Status.ToString(), updated.Notes, updated.CreatedAt, updated.UpdatedAt);
    }
}

public sealed record DeleteStateCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteStateCommandHandler(IStateRepository repository) : IRequestHandler<DeleteStateCommand, bool>
{
    public async Task<bool> Handle(DeleteStateCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        entity.Status = EntityStatus.Deleted;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, ct);
        return true;
    }
}
