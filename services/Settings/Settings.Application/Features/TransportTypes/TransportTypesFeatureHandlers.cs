using MediatR;
using Settings.Application.DTOs.TransportType;
using Settings.Domain.AggregatesModel.TransportTypeAggregate;

namespace Settings.Application.Features.TransportTypes;

public sealed record GetAllTransportTypesQuery : IRequest<IReadOnlyList<TransportTypeDto>>;
public sealed class GetAllTransportTypesQueryHandler(ITransportTypeRepository repository) : IRequestHandler<GetAllTransportTypesQuery, IReadOnlyList<TransportTypeDto>>
{
    public async Task<IReadOnlyList<TransportTypeDto>> Handle(GetAllTransportTypesQuery request, CancellationToken ct)
    {
        var list = await repository.GetAllAsync(ct);
        return list.Select(e => new TransportTypeDto(e.Id, e.Name, e.IsAir, e.IsSea, e.IsRoad, e.IsRail, e.IsActive, e.CreatedAt, e.UpdatedAt)).ToList();
    }
}

public sealed record GetTransportTypeByIdQuery(Guid Id) : IRequest<TransportTypeDto?>;
public sealed class GetTransportTypeByIdQueryHandler(ITransportTypeRepository repository) : IRequestHandler<GetTransportTypeByIdQuery, TransportTypeDto?>
{
    public async Task<TransportTypeDto?> Handle(GetTransportTypeByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        return e == null ? null : new TransportTypeDto(e.Id, e.Name, e.IsAir, e.IsSea, e.IsRoad, e.IsRail, e.IsActive, e.CreatedAt, e.UpdatedAt);
    }
}

public sealed record CreateTransportTypeCommand(CreateTransportTypeDto Dto) : IRequest<TransportTypeDto>;
public sealed class CreateTransportTypeCommandHandler(ITransportTypeRepository repository) : IRequestHandler<CreateTransportTypeCommand, TransportTypeDto>
{
    public async Task<TransportTypeDto> Handle(CreateTransportTypeCommand request, CancellationToken ct)
    {
        var entity = new TransportType { Id = Guid.NewGuid(), Name = request.Dto.Name, IsAir = request.Dto.IsAir, IsSea = request.Dto.IsSea, IsRoad = request.Dto.IsRoad, IsRail = request.Dto.IsRail, IsActive = request.Dto.IsActive, CreatedAt = DateTime.UtcNow };
        await repository.AddAsync(entity, ct);
        return new TransportTypeDto(entity.Id, entity.Name, entity.IsAir, entity.IsSea, entity.IsRoad, entity.IsRail, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record UpdateTransportTypeCommand(Guid Id, UpdateTransportTypeDto Dto) : IRequest<TransportTypeDto?>;
public sealed class UpdateTransportTypeCommandHandler(ITransportTypeRepository repository) : IRequestHandler<UpdateTransportTypeCommand, TransportTypeDto?>
{
    public async Task<TransportTypeDto?> Handle(UpdateTransportTypeCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        entity.Name = request.Dto.Name; entity.IsAir = request.Dto.IsAir; entity.IsSea = request.Dto.IsSea; entity.IsRoad = request.Dto.IsRoad; entity.IsRail = request.Dto.IsRail; entity.IsActive = request.Dto.IsActive; entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, ct);
        return new TransportTypeDto(entity.Id, entity.Name, entity.IsAir, entity.IsSea, entity.IsRoad, entity.IsRail, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record DeleteTransportTypeCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteTransportTypeCommandHandler(ITransportTypeRepository repository) : IRequestHandler<DeleteTransportTypeCommand, bool>
{
    public async Task<bool> Handle(DeleteTransportTypeCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, ct);
        return true;
    }
}
