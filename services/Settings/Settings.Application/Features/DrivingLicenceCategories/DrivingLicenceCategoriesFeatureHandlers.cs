using MediatR;
using Settings.Application.DTOs.DrivingLicenceCategory;
using Settings.Domain.AggregatesModel.DrivingLicenceCategoryAggregate;

namespace Settings.Application.Features.DrivingLicenceCategories;

public sealed record GetAllDrivingLicenceCategoriesQuery : IRequest<IReadOnlyList<DrivingLicenceCategoryDto>>;
public sealed class GetAllDrivingLicenceCategoriesQueryHandler(IDrivingLicenceCategoryRepository repository) : IRequestHandler<GetAllDrivingLicenceCategoriesQuery, IReadOnlyList<DrivingLicenceCategoryDto>>
{
    public async Task<IReadOnlyList<DrivingLicenceCategoryDto>> Handle(GetAllDrivingLicenceCategoriesQuery request, CancellationToken ct)
    {
        var list = await repository.GetAllAsync(ct);
        return list.Select(e => new DrivingLicenceCategoryDto(e.Id, e.Name, e.Code, e.IsActive, e.CreatedAt, e.UpdatedAt)).ToList();
    }
}

public sealed record GetDrivingLicenceCategoryByIdQuery(Guid Id) : IRequest<DrivingLicenceCategoryDto?>;
public sealed class GetDrivingLicenceCategoryByIdQueryHandler(IDrivingLicenceCategoryRepository repository) : IRequestHandler<GetDrivingLicenceCategoryByIdQuery, DrivingLicenceCategoryDto?>
{
    public async Task<DrivingLicenceCategoryDto?> Handle(GetDrivingLicenceCategoryByIdQuery request, CancellationToken ct)
    {
        var e = await repository.GetByIdAsync(request.Id, ct);
        return e == null ? null : new DrivingLicenceCategoryDto(e.Id, e.Name, e.Code, e.IsActive, e.CreatedAt, e.UpdatedAt);
    }
}

public sealed record CreateDrivingLicenceCategoryCommand(CreateDrivingLicenceCategoryDto Dto) : IRequest<DrivingLicenceCategoryDto>;
public sealed class CreateDrivingLicenceCategoryCommandHandler(IDrivingLicenceCategoryRepository repository) : IRequestHandler<CreateDrivingLicenceCategoryCommand, DrivingLicenceCategoryDto>
{
    public async Task<DrivingLicenceCategoryDto> Handle(CreateDrivingLicenceCategoryCommand request, CancellationToken ct)
    {
        var entity = new DrivingLicenceCategory { Id = Guid.NewGuid(), Name = request.Dto.Name, Code = request.Dto.Code, IsActive = request.Dto.IsActive, CreatedAt = DateTime.UtcNow };
        await repository.AddAsync(entity, ct);
        return new DrivingLicenceCategoryDto(entity.Id, entity.Name, entity.Code, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record UpdateDrivingLicenceCategoryCommand(Guid Id, UpdateDrivingLicenceCategoryDto Dto) : IRequest<DrivingLicenceCategoryDto?>;
public sealed class UpdateDrivingLicenceCategoryCommandHandler(IDrivingLicenceCategoryRepository repository) : IRequestHandler<UpdateDrivingLicenceCategoryCommand, DrivingLicenceCategoryDto?>
{
    public async Task<DrivingLicenceCategoryDto?> Handle(UpdateDrivingLicenceCategoryCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return null;
        entity.Name = request.Dto.Name; entity.Code = request.Dto.Code; entity.IsActive = request.Dto.IsActive; entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, ct);
        return new DrivingLicenceCategoryDto(entity.Id, entity.Name, entity.Code, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}

public sealed record DeleteDrivingLicenceCategoryCommand(Guid Id) : IRequest<bool>;
public sealed class DeleteDrivingLicenceCategoryCommandHandler(IDrivingLicenceCategoryRepository repository) : IRequestHandler<DeleteDrivingLicenceCategoryCommand, bool>
{
    public async Task<bool> Handle(DeleteDrivingLicenceCategoryCommand request, CancellationToken ct)
    {
        var entity = await repository.GetByIdAsync(request.Id, ct);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, ct);
        return true;
    }
}
