using MediatR;
using Settings.Application.DTOs.DeferredPaymentCondition;
using Settings.Domain.AggregatesModel.DeferredPaymentConditionAggregate;

namespace Settings.Application.Features.DeferredPaymentConditions;

public sealed record GetAllDeferredPaymentConditionsQuery : IRequest<IReadOnlyList<DeferredPaymentConditionDto>>;
public sealed class GetAllDeferredPaymentConditionsQueryHandler(IDeferredPaymentConditionRepository repository) : IRequestHandler<GetAllDeferredPaymentConditionsQuery, IReadOnlyList<DeferredPaymentConditionDto>>
{
    public async Task<IReadOnlyList<DeferredPaymentConditionDto>> Handle(GetAllDeferredPaymentConditionsQuery request, CancellationToken ct)
    {
        var list = await repository.GetAllAsync(ct);
        return list.Select(MapToDto).ToList();
    }
    
    private static DeferredPaymentConditionDto MapToDto(DeferredPaymentCondition e) => new(
        e.Id, e.Name, e.ClientIncluded, e.ClientDaysOfDelay, e.CarrierIncluded, e.CarrierDaysOfDelay,
        e.FullText, e.IsActive, e.CreatedAt, e.UpdatedAt);
}

public sealed record GetDeferredPaymentConditionByIdQuery(Guid Id) : IRequest<DeferredPaymentConditionDto?>;
public sealed class GetDeferredPaymentConditionByIdQueryHandler(IDeferredPaymentConditionRepository repository) : IRequestHandler<GetDeferredPaymentConditionByIdQuery, DeferredPaymentConditionDto?>
{
    public async Task<DeferredPaymentConditionDto?> Handle(GetDeferredPaymentConditionByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        if (e == null) return null;
        return new DeferredPaymentConditionDto(
            e.Id, e.Name, e.ClientIncluded, e.ClientDaysOfDelay, e.CarrierIncluded, e.CarrierDaysOfDelay,
            e.FullText, e.IsActive, e.CreatedAt, e.UpdatedAt);
    }
}

public sealed record CreateDeferredPaymentConditionCommand(CreateDeferredPaymentConditionDto Dto) : IRequest<DeferredPaymentConditionDto>;
public sealed class CreateDeferredPaymentConditionCommandHandler(IDeferredPaymentConditionRepository repository) : IRequestHandler<CreateDeferredPaymentConditionCommand, DeferredPaymentConditionDto>
{
    public async Task<DeferredPaymentConditionDto> Handle(CreateDeferredPaymentConditionCommand request, CancellationToken ct)
    {
        var entity = new DeferredPaymentCondition
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            ClientIncluded = request.Dto.ClientIncluded,
            ClientDaysOfDelay = request.Dto.ClientDaysOfDelay,
            CarrierIncluded = request.Dto.CarrierIncluded,
            CarrierDaysOfDelay = request.Dto.CarrierDaysOfDelay,
            FullText = request.Dto.FullText,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, ct);
        return new DeferredPaymentConditionDto(
            entity.Id, entity.Name, entity.ClientIncluded, entity.ClientDaysOfDelay, entity.CarrierIncluded, entity.CarrierDaysOfDelay,
            entity.FullText, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record UpdateDeferredPaymentConditionCommand(Guid Id, UpdateDeferredPaymentConditionDto Dto) : IRequest<DeferredPaymentConditionDto?>;
public sealed class UpdateDeferredPaymentConditionCommandHandler(IDeferredPaymentConditionRepository repository) : IRequestHandler<UpdateDeferredPaymentConditionCommand, DeferredPaymentConditionDto?>
{
    public async Task<DeferredPaymentConditionDto?> Handle(UpdateDeferredPaymentConditionCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.ClientIncluded = request.Dto.ClientIncluded;
        entity.ClientDaysOfDelay = request.Dto.ClientDaysOfDelay;
        entity.CarrierIncluded = request.Dto.CarrierIncluded;
        entity.CarrierDaysOfDelay = request.Dto.CarrierDaysOfDelay;
        entity.FullText = request.Dto.FullText;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, ct);
        return new DeferredPaymentConditionDto(
            entity.Id, entity.Name, entity.ClientIncluded, entity.ClientDaysOfDelay, entity.CarrierIncluded, entity.CarrierDaysOfDelay,
            entity.FullText, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record DeleteDeferredPaymentConditionCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteDeferredPaymentConditionCommandHandler(IDeferredPaymentConditionRepository repository) : IRequestHandler<DeleteDeferredPaymentConditionCommand, bool>
{
    public async Task<bool> Handle(DeleteDeferredPaymentConditionCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, ct);
        return true;
    }
}
