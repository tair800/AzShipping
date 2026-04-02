using MediatR;
using Operation.Application.DTOs.Operation;
using Operation.Domain.AggregatesModel.OperationAggregate;

namespace Operation.Application.Features.Operations;

public sealed record GetOperationTypesQuery(bool IncludeInactive) : IRequest<IReadOnlyList<OperationTypeDto>>;
public sealed class GetOperationTypesQueryHandler(IOperationTypeRepository types)
    : IRequestHandler<GetOperationTypesQuery, IReadOnlyList<OperationTypeDto>>
{
    public async Task<IReadOnlyList<OperationTypeDto>> Handle(GetOperationTypesQuery request, CancellationToken ct)
    {
        var list = await types.GetAllAsync(request.IncludeInactive, ct);
        return list.Select(OperationMapping.ToTypeDto).ToList();
    }
}

public sealed record GetOperationTypeByIdQuery(Guid Id) : IRequest<OperationTypeDto?>;
public sealed class GetOperationTypeByIdQueryHandler(IOperationTypeRepository types)
    : IRequestHandler<GetOperationTypeByIdQuery, OperationTypeDto?>
{
    public async Task<OperationTypeDto?> Handle(GetOperationTypeByIdQuery request, CancellationToken ct)
    {
        var t = await types.GetByIdAsync(request.Id, ct);
        return t == null ? null : OperationMapping.ToTypeDto(t);
    }
}

public sealed record GetAllOperationsQuery : IRequest<IReadOnlyList<OperationDto>>;
public sealed class GetAllOperationsQueryHandler(IOperationRepository ops, IOperationTypeRepository types)
    : IRequestHandler<GetAllOperationsQuery, IReadOnlyList<OperationDto>>
{
    public async Task<IReadOnlyList<OperationDto>> Handle(GetAllOperationsQuery request, CancellationToken ct)
    {
        var list = await ops.GetAllAsync(ct);
        var typeCache = new Dictionary<Guid, OperationType?>();
        var result = new List<OperationDto>();
        foreach (var e in list)
        {
            if (!typeCache.TryGetValue(e.OperationTypeId, out var t))
            {
                t = await types.GetByIdAsync(e.OperationTypeId, ct);
                typeCache[e.OperationTypeId] = t;
            }
            result.Add(OperationMapping.ToDto(e, t, e.Dimensions.ToList(), e.PackageLines.ToList(), e.VasItems.ToList()));
        }
        return result;
    }
}

public sealed record GetOperationsListQuery : IRequest<IReadOnlyList<OperationListItemDto>>;
public sealed class GetOperationsListQueryHandler(IOperationRepository ops, IOperationTypeRepository types)
    : IRequestHandler<GetOperationsListQuery, IReadOnlyList<OperationListItemDto>>
{
    public async Task<IReadOnlyList<OperationListItemDto>> Handle(GetOperationsListQuery request, CancellationToken ct)
    {
        var list = await ops.GetAllForListAsync(ct);
        var typeCache = new Dictionary<Guid, OperationType?>();
        var result = new List<OperationListItemDto>();
        foreach (var e in list)
        {
            if (!typeCache.TryGetValue(e.OperationTypeId, out var t))
            {
                t = await types.GetByIdAsync(e.OperationTypeId, ct);
                typeCache[e.OperationTypeId] = t;
            }
            result.Add(OperationMapping.ToListItemDto(e, t));
        }
        return result;
    }
}

public sealed record GetTripsListQuery : IRequest<IReadOnlyList<TripListItemDto>>;
public sealed class GetTripsListQueryHandler(IOperationRepository ops, IOperationTypeRepository types)
    : IRequestHandler<GetTripsListQuery, IReadOnlyList<TripListItemDto>>
{
    public async Task<IReadOnlyList<TripListItemDto>> Handle(GetTripsListQuery request, CancellationToken ct)
    {
        var list = await ops.GetAllScalarsAsync(ct);
        var typeCache = new Dictionary<Guid, OperationType?>();
        var result = new List<TripListItemDto>();
        foreach (var e in list)
        {
            if (!typeCache.TryGetValue(e.OperationTypeId, out var t))
            {
                t = await types.GetByIdAsync(e.OperationTypeId, ct);
                typeCache[e.OperationTypeId] = t;
            }
            result.Add(OperationMapping.ToTripListItemDto(e, t));
        }
        return result;
    }
}

public sealed record GetOperationByIdQuery(Guid Id) : IRequest<OperationDto?>;
public sealed class GetOperationByIdQueryHandler(IOperationRepository ops, IOperationTypeRepository types)
    : IRequestHandler<GetOperationByIdQuery, OperationDto?>
{
    public async Task<OperationDto?> Handle(GetOperationByIdQuery request, CancellationToken ct)
    {
        var e = await ops.GetByIdAsync(request.Id, ct);
        if (e == null) return null;
        var t = await types.GetByIdAsync(e.OperationTypeId, ct);
        return OperationMapping.ToDto(e, t, e.Dimensions.ToList(), e.PackageLines.ToList(), e.VasItems.ToList());
    }
}

public sealed record CreateOperationCommand(SaveOperationDto Dto) : IRequest<OperationDto>;
public sealed class CreateOperationCommandHandler(
    IOperationRepository ops,
    IOperationTypeRepository types,
    IOperationDimensionRepository dims,
    IOperationPackageLineRepository pkgLines,
    IOperationVasRepository vasItems) : IRequestHandler<CreateOperationCommand, OperationDto>
{
    public async Task<OperationDto> Handle(CreateOperationCommand request, CancellationToken ct)
    {
        var d = request.Dto;
        if (string.IsNullOrWhiteSpace(d.OperationNumber))
            throw new InvalidOperationException("OperationNumber is required.");
        if (d.OperationTypeId is null || d.OperationTypeId == Guid.Empty)
            throw new InvalidOperationException("OperationTypeId is required.");

        var type = await types.GetByIdAsync(d.OperationTypeId.Value, ct);
        if (type == null)
            throw new InvalidOperationException("Operation type not found.");

        var now = DateTime.UtcNow;
        var id = Guid.NewGuid();
        var entity = OperationMapping.CreateEntity(d, id, now, type.Id);
        OperationMapping.MergeScalars(entity, d);
        OperationMapping.ApplyAirCargoFromInputs(entity, type, d);
        OperationMapping.ApplySeaPackageTotals(entity, type, d);

        await ops.AddAsync(entity, ct);

        var dimEntities = OperationMapping.BuildDimensionEntities(id, d);
        if (dimEntities.Count > 0)
            await dims.AddRangeAsync(dimEntities, ct);

        var pkgEntities = OperationMapping.BuildPackageLineEntities(id, d);
        if (pkgEntities.Count > 0)
            await pkgLines.AddRangeAsync(pkgEntities, ct);

        var vasEntities = OperationMapping.BuildVasEntities(id, d, type);
        if (vasEntities.Count > 0)
            await vasItems.AddRangeAsync(vasEntities, ct);

        var loaded = await ops.GetByIdAsync(id, ct) ?? entity;
        var dloaded = await dims.GetByOperationIdAsync(id, ct);
        var ploaded = await pkgLines.GetByOperationIdAsync(id, ct);
        var vloaded = await vasItems.GetByOperationIdAsync(id, ct);
        return OperationMapping.ToDto(loaded, type, dloaded, ploaded, vloaded);
    }
}

public sealed record UpdateOperationCommand(Guid Id, SaveOperationDto Dto) : IRequest<OperationDto?>;
public sealed class UpdateOperationCommandHandler(
    IOperationRepository ops,
    IOperationTypeRepository types,
    IOperationDimensionRepository dims,
    IOperationPackageLineRepository pkgLines,
    IOperationVasRepository vasItems) : IRequestHandler<UpdateOperationCommand, OperationDto?>
{
    public async Task<OperationDto?> Handle(UpdateOperationCommand request, CancellationToken ct)
    {
        var e = await ops.GetByIdAsync(request.Id, ct);
        if (e == null) return null;

        var d = request.Dto;
        OperationMapping.MergeScalars(e, d);
        if (d.OperationTypeId is Guid tid && tid != Guid.Empty)
            e.OperationTypeId = tid;

        var type = await types.GetByIdAsync(e.OperationTypeId, ct);
        OperationMapping.ApplyAirCargoFromInputs(e, type, d);
        OperationMapping.ApplySeaPackageTotals(e, type, d);

        e.UpdatedAt = DateTime.UtcNow;
        await ops.UpdateAsync(e, ct);

        await dims.DeleteByOperationIdAsync(e.Id, ct);
        var dimEntities = OperationMapping.BuildDimensionEntities(e.Id, d);
        if (dimEntities.Count > 0)
            await dims.AddRangeAsync(dimEntities, ct);

        await pkgLines.DeleteByOperationIdAsync(e.Id, ct);
        var pkgEntities = OperationMapping.BuildPackageLineEntities(e.Id, d);
        if (pkgEntities.Count > 0)
            await pkgLines.AddRangeAsync(pkgEntities, ct);

        await vasItems.DeleteByOperationIdAsync(e.Id, ct);
        var vasEntities = OperationMapping.BuildVasEntities(e.Id, d, type);
        if (vasEntities.Count > 0)
            await vasItems.AddRangeAsync(vasEntities, ct);

        var dloaded = await dims.GetByOperationIdAsync(e.Id, ct);
        var ploaded = await pkgLines.GetByOperationIdAsync(e.Id, ct);
        var vloaded = await vasItems.GetByOperationIdAsync(e.Id, ct);
        return OperationMapping.ToDto(e, type, dloaded, ploaded, vloaded);
    }
}

public sealed record DeleteOperationCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteOperationCommandHandler(IOperationRepository ops) : IRequestHandler<DeleteOperationCommand, bool>
{
    public async Task<bool> Handle(DeleteOperationCommand request, CancellationToken ct)
    {
        var e = await ops.GetByIdAsync(request.Id, ct);
        if (e == null) return false;
        await ops.DeleteAsync(request.Id, ct);
        return true;
    }
}
