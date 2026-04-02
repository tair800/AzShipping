using MediatR;
using Settings.Application.DTOs.RequestPurpose;
using Settings.Domain.AggregatesModel.RequestPurposeAggregate;

namespace Settings.Application.Features.RequestPurposes;

public sealed record GetAllRequestPurposesQuery : IRequest<IReadOnlyList<RequestPurposeDto>>;
public sealed class GetAllRequestPurposesQueryHandler(IRequestPurposeRepository repository) : IRequestHandler<GetAllRequestPurposesQuery, IReadOnlyList<RequestPurposeDto>>
{
    public async Task<IReadOnlyList<RequestPurposeDto>> Handle(GetAllRequestPurposesQuery request, CancellationToken ct)
    {
        var list = await repository.GetAllAsync(ct);
        return list.Select(e => new RequestPurposeDto(e.Id, e.Name, e.IsActive, e.CreatedAt, e.UpdatedAt)).ToList();
    }
}

public sealed record GetRequestPurposeByIdQuery(Guid Id) : IRequest<RequestPurposeDto?>;
public sealed class GetRequestPurposeByIdQueryHandler(IRequestPurposeRepository repository) : IRequestHandler<GetRequestPurposeByIdQuery, RequestPurposeDto?>
{
    public async Task<RequestPurposeDto?> Handle(GetRequestPurposeByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        return e == null ? null : new RequestPurposeDto(e.Id, e.Name, e.IsActive, e.CreatedAt, e.UpdatedAt);
    }
}

public sealed record CreateRequestPurposeCommand(CreateRequestPurposeDto Dto) : IRequest<RequestPurposeDto>;
public sealed class CreateRequestPurposeCommandHandler(IRequestPurposeRepository repository) : IRequestHandler<CreateRequestPurposeCommand, RequestPurposeDto>
{
    public async Task<RequestPurposeDto> Handle(CreateRequestPurposeCommand request, CancellationToken ct)
    {
        var entity = new RequestPurpose { Id = Guid.NewGuid(), Name = request.Dto.Name, IsActive = request.Dto.IsActive, CreatedAt = DateTime.UtcNow };
        await repository.AddAsync(entity, ct);
        return new RequestPurposeDto(entity.Id, entity.Name, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record UpdateRequestPurposeCommand(Guid Id, UpdateRequestPurposeDto Dto) : IRequest<RequestPurposeDto?>;
public sealed class UpdateRequestPurposeCommandHandler(IRequestPurposeRepository repository) : IRequestHandler<UpdateRequestPurposeCommand, RequestPurposeDto?>
{
    public async Task<RequestPurposeDto?> Handle(UpdateRequestPurposeCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        entity.Name = request.Dto.Name; entity.IsActive = request.Dto.IsActive; entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, ct);
        return new RequestPurposeDto(entity.Id, entity.Name, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record DeleteRequestPurposeCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteRequestPurposeCommandHandler(IRequestPurposeRepository repository) : IRequestHandler<DeleteRequestPurposeCommand, bool>
{
    public async Task<bool> Handle(DeleteRequestPurposeCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, ct);
        return true;
    }
}
