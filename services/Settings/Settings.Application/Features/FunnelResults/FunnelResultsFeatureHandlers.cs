using MediatR;
using Settings.Application.DTOs.FunnelResult;
using Settings.Domain.AggregatesModel.FunnelResultAggregate;
using Settings.Domain.AggregatesModel.ResultTypeAggregate;

namespace Settings.Application.Features.FunnelResults;

public sealed record GetAllFunnelResultsQuery : IRequest<IReadOnlyList<FunnelResultDto>>;
public sealed class GetAllFunnelResultsQueryHandler(IFunnelResultRepository repository) : IRequestHandler<GetAllFunnelResultsQuery, IReadOnlyList<FunnelResultDto>>
{
    public async Task<IReadOnlyList<FunnelResultDto>> Handle(GetAllFunnelResultsQuery request, CancellationToken ct)
    {
        var list = await repository.GetAllAsync(ct);
        return list.Select(e => new FunnelResultDto(e.Id, e.Name, e.ResultTypeId, e.ResultType.Name, e.ToNextStep, e.IsActive, e.CreatedAt, e.UpdatedAt)).ToList();
    }
}

public sealed record GetFunnelResultByIdQuery(Guid Id) : IRequest<FunnelResultDto?>;
public sealed class GetFunnelResultByIdQueryHandler(IFunnelResultRepository repository) : IRequestHandler<GetFunnelResultByIdQuery, FunnelResultDto?>
{
    public async Task<FunnelResultDto?> Handle(GetFunnelResultByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        if (e == null) return null;
        return new FunnelResultDto(e.Id, e.Name, e.ResultTypeId, e.ResultType.Name, e.ToNextStep, e.IsActive, e.CreatedAt, e.UpdatedAt);
    }
}

public sealed record CreateFunnelResultCommand(CreateFunnelResultDto Dto) : IRequest<FunnelResultDto>;
public sealed class CreateFunnelResultCommandHandler(IFunnelResultRepository repository, IResultTypeRepository resultTypeRepo) : IRequestHandler<CreateFunnelResultCommand, FunnelResultDto>
{
    public async Task<FunnelResultDto> Handle(CreateFunnelResultCommand request, CancellationToken ct)
    {
        var rt = await resultTypeRepo.GetByIdAsync(request.Dto.ResultTypeId, ct) ?? throw new InvalidOperationException("ResultType not found.");
        var entity = new FunnelResult
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            ResultTypeId = request.Dto.ResultTypeId,
            ToNextStep = request.Dto.ToNextStep,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, ct);
        return new FunnelResultDto(entity.Id, entity.Name, entity.ResultTypeId, rt.Name, entity.ToNextStep, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record UpdateFunnelResultCommand(Guid Id, UpdateFunnelResultDto Dto) : IRequest<FunnelResultDto?>;
public sealed class UpdateFunnelResultCommandHandler(IFunnelResultRepository repository, IResultTypeRepository resultTypeRepo) : IRequestHandler<UpdateFunnelResultCommand, FunnelResultDto?>
{
    public async Task<FunnelResultDto?> Handle(UpdateFunnelResultCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        var rt = await resultTypeRepo.GetByIdAsync(request.Dto.ResultTypeId, ct);
        if (rt == null) return null;
        entity.Name = request.Dto.Name;
        entity.ResultTypeId = request.Dto.ResultTypeId;
        entity.ToNextStep = request.Dto.ToNextStep;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, ct);
        return new FunnelResultDto(entity.Id, entity.Name, entity.ResultTypeId, rt.Name, entity.ToNextStep, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record DeleteFunnelResultCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteFunnelResultCommandHandler(IFunnelResultRepository repository) : IRequestHandler<DeleteFunnelResultCommand, bool>
{
    public async Task<bool> Handle(DeleteFunnelResultCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, ct);
        return true;
    }
}
